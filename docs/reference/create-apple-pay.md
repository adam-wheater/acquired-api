# Apple Pay API Documentation

## Overview

The Apple Pay endpoint enables processing of encrypted Apple Pay payment requests through the Acquired API.

**Endpoint:** `POST https://test-api.acquired.com/v1/payments/apple-pay`

**Authentication:** Bearer Token or JWT

## Request Structure

When users select the Apple Pay button, "a payment request will be created and an encrypted payload will be returned" to your system.

### Core Request Components

The request body includes:

**Transaction Object:**
- `order_id` (string, 2-50 chars): Unique reference for payment request
- `amount` (number): Total charge amount
- `currency` (string): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- `moto` (boolean): Set true for phone transactions
- `capture` (boolean): false for authorization only; true for immediate capture
- `custom_data` (string): Base64 encoded custom data
- `custom1`, `custom2` (strings): Additional reference fields

**Payment Object:**
- Apple Pay encrypted payload data

**Customer Object:**
- `customer_id` (UUID): Link to existing customer
- Billing/shipping address information
- Contact details

**3DS Configuration:**
- `is_active` (boolean): Enable EMV 3-D Secure authentication
- `challenge_preference`: Challenge handling preference
- `redirect_url`: Post-authentication redirect destination
- `webhook_url`: Notification endpoint for auth status updates

## Response

**Success (201 Created):**
Returns transaction details including:
- `transaction_id`: Unique transaction identifier
- Payment status and authorization details
- Card information (masked)

**Error Responses:**
- 400: Validation failures with specific parameter details
- 401: Authentication failure
- 403: Access denied to requested resource

## Required Headers

- `Company-Id`: UUID for your company
- `Mid`: UUID linking to acquiring bank (optional)
