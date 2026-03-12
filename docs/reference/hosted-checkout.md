# Generate a link_id - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-links`

### Description
This endpoint creates a payment link containing order or payment details. The generated `link_id` can be appended to the appropriate environment URL and shared with customers. For example: `https://test-pay.acquired.com/v1/{link_id}` (remove "test" prefix in production).

## Authentication

**Type:** Bearer Token or JWT
**Required Header:** Authorization with valid access token

## Request Parameters

The request body accepts a JSON object with transaction and payment details:

### Transaction Object (Required)
- **order_id** (string, 2-50 chars): Unique reference for the payment request
- **amount** (number): Total charge amount
- **currency** (string): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean, default: false): Set true for phone orders
- **capture** (boolean, default: true): Set false to authorize only
- **custom_data** (string): Base64-encoded custom data
- **custom1, custom2** (string): Additional reference fields (0-50 chars)

### Payment Object
- **card** (object): Card details (holder_name, number, expiry_month, expiry_year, cvv)
- **create_card** (boolean): Set true to store card for future use
- **reference** (string): Bank statement descriptor (1-18 chars)

### Customer Object
- **customer_id** (string, UUID): Unique customer identifier
- **reference** (string): Your customer reference
- **first_name, last_name** (string): Customer names
- **dob** (string, format: YYYY-MM-DD): Date of birth
- **billing/shipping** (object): Address and contact details

### TDS (3D Secure) Object
- **is_active** (boolean): Enable EMV 3DS authentication
- **challenge_preference** (string): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size** (string): Window dimensions for challenge
- **contact_url** (string, URI): Support page link
- **redirect_url** (string, URI): Post-authentication redirect location
- **webhook_url** (string, URI): Notification endpoint

## Response Codes

**201 Created:** Successful link generation
**400 Bad Request:** Validation errors in parameters
**401 Unauthorized:** Authentication failure
**403 Forbidden:** Access denied to resource
**404 Not Found:** Resource not found
**409 Conflict:** Conflict with existing data

## Headers

- **Company-Id** (string, UUID): Required company identifier
- **Mid** (string, UUID): Merchant ID for specific acquiring bank

## Usage Notes

This endpoint generates shareable payment links for hosted checkout flows. The link includes all transaction and customer details, enabling secure payment processing through the Acquired platform. For production environments, replace "test-api" and "test-pay" with production URLs.
