# Recurring Payments API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments/recurring`

This endpoint enables "charging customers on a regular basis, i.e. for subscriptions or credit-based services."

## Authentication

- **Type**: Bearer Token (JWT)
- **Method**: Include access token in Authorization header
- **Token Generation**: Use `/login` endpoint with `app_id` and `app_key`

## Required Headers

| Header | Description | Type |
|--------|-------------|------|
| `Company-Id` | Unique identifier for your company | UUID |
| `Mid` | Unique ID connecting to acquiring bank | UUID (optional) |

## Request Body Structure

### Transaction Object
- **order_id** (string, 2-50 chars): Your unique reference for payment
- **amount** (float): Total charge amount
- **currency** (enum): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean): Set true for phone orders
- **capture** (boolean): Default true; set false for auth-only
- **custom_data** (string): Base64 encoded custom metadata
- **custom1, custom2** (string): Additional reference fields

### Payment Object
- **card**: Card details (holder_name, number, expiry_month, expiry_year, cvv)
- **create_card** (boolean): Save card for future use
- **reference** (string, 1-18 chars): Bank statement descriptor

### Customer Object
Complete customer information including:
- **customer_id** or create inline with first_name, last_name, dob
- **Billing address**: line_1, line_2, city, state, postcode, country_code
- **Shipping address**: Optional; can mirror billing
- **Email and phone**: Recommended for 3DS transactions

### TDS (3-D Secure) Object
- **is_active** (boolean): Enable EMV 3DS authentication
- **challenge_preference**: challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size**: full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- **contact_url**: Support page link
- **redirect_url**: Post-authentication redirect destination
- **webhook_url**: Notification endpoint for auth process updates

## Response Format

**Success (201 Created)**: Returns transaction object with:
- Transaction ID
- Status (pending, success, failed)
- Authorization details
- Card information (masked)

**Error Responses**:
- **400**: Validation failure - returns invalid_parameters array
- **401**: Authentication failed
- **403**: Forbidden/insufficient permissions
- **409**: Conflict (e.g., duplicate reference)

## Language Support

Supported: Shell, Node, Ruby, PHP, Python

## Key Notes

- Recurring payments link transactions to stored customer records
- All transactions associate with a `customer_id`
- Address data recommended for fraud prevention and 3DS
- Phone number format: country_code + number (max 15 digits)
- Date of birth required for MCC 6012 merchants
- Custom data must be Base64 encoded
