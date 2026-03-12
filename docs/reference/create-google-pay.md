# Google Pay API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments/google-pay`

This endpoint processes Google Pay payments. When users select Google Pay as their payment method, the system creates a payment request and returns an encrypted payload to your application.

## Authentication

The endpoint requires Bearer token authentication via JWT. Include your access token in the Authorization header.

## Request Parameters

### Headers
- **Company-Id** (optional): UUID identifying your company
- **Mid** (optional): UUID connecting to a specific acquiring bank

### Request Body

The request accepts a JSON object with the following primary sections:

**Transaction Object** (required):
- `order_id` (string, 2-50 chars): Your unique payment reference
- `amount` (float): Total charge amount
- `currency` (enum): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- `moto` (boolean, default: false): Phone payment flag
- `capture` (boolean, default: true): Authorization-only mode when false
- `custom_data` (string): Base64-encoded custom information
- `custom1`, `custom2` (string, max 50 chars): Additional reference fields

**Payment Object**:
- `google_pay_token`: Encrypted Google Pay payload
- `reference` (string, max 18 chars): Bank statement descriptor
- `create_card` (boolean, default: false): Save for future use

**Customer Object** (optional):
- `customer_id` (UUID): Existing customer identifier
- `reference` (string, max 50 chars): Your customer reference
- `first_name` (string, max 22 chars): Customer's given name
- `last_name` (string, max 22 chars): Customer's surname
- `dob` (date format YYYY-MM-DD): Date of birth
- `custom_data` (string): Base64-encoded custom data
- Billing and shipping address objects with: line_1, line_2, city, state, postcode, country_code
- Email and phone contact information

**3DS Object** (optional):
- `is_active` (boolean): Enable EMV 3-D Secure authentication
- `challenge_preference` (enum): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- `challenge_window_size` (enum): full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- `contact_url` (URI): Support page link
- `redirect_url` (URI): Post-authentication redirect
- `webhook_url` (URI): Webhook notification endpoint

## Response

**Success (201 Created)**:
Returns a JSON object containing:
- `transaction_id` (string): Unique transaction identifier
- Payment status and details
- Card information (if created)
- Authorization codes and timestamps

**Error Responses**:

- **400 Bad Request**: Validation failures with invalid_parameters array detailing specific field errors
- **401 Unauthorized**: Authentication failure
- **404 Not Found**: Resource not found

## Key Features

- Supports both authorization and capture modes
- Optional customer creation during payment
- 3-D Secure authentication available
- Card tokenization for recurring payments
- Custom data transmission for reconciliation
- Webhook support for asynchronous updates
