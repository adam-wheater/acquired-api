# Process a Payment (card_id) - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/reuse`
**Description:** "Process a payment using a stored card. Simply pass the `card_id` and the cvv value in your request instead of passing the full card details."

---

## Authentication

**Type:** Bearer Token (JWT)
**Header:** Authorization with Bearer token from `/login` endpoint

---

## Request Parameters

### Header Parameters
- **Company-Id** (optional): Unique identifier assigned by Acquired.com for your company (UUID format)
- **Mid** (optional): Unique identifier connecting to a specific acquiring bank (UUID format)

### Request Body Schema

The request accepts a JSON object with the following structure:

#### Transaction Object (Required)
- **order_id** (string, 2-50 chars): Unique reference for the payment request
- **amount** (number): Total amount to charge
- **currency** (string): ISO 4217 currency code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean, default: false): Set true for phone payments
- **capture** (boolean, default: true): Set false to authorize only
- **custom_data** (string): Base64 encoded custom data
- **custom1** (string, 0-50 chars): Additional customer/reference data
- **custom2** (string, 0-50 chars): Additional customer/reference data

#### Payment Object (Required)
- **card_id** (string, UUID): The unique identifier of the stored card
- **cvv** (string, 3-4 digits): Security code for the stored card
- **reference** (string, 1-18 chars): Optional text to appear on cardholder statement

#### Customer Object (Optional)
- **customer_id** (string, UUID): Unique customer identifier
- **reference** (string, 0-50 chars): Merchant's customer reference
- **first_name** (string, 0-22 chars): Customer first name
- **last_name** (string, 0-22 chars): Customer last name
- **dob** (string, date format YYYY-MM-DD): Date of birth (required for MCC 6012)
- **custom_data** (string): Base64 encoded custom data
- **billing** (object): Billing address details
- **shipping** (object): Shipping address details

#### Address Object (Billing/Shipping)
- **line_1** (string, 0-50 chars): First address line
- **line_2** (string, 0-50 chars): Second address line
- **city** (string, 1-40 chars): City name
- **state** (string, 0-3 chars): ISO 3166-2 state code (US only)
- **postcode** (string, 1-40 chars): Postal code
- **country_code** (string, 2 chars): ISO 3166 country code

#### Phone Object
- **country_code** (string, 1-3 digits): International dialing code
- **number** (string, 0-15 digits): Phone number

#### 3D Secure Object (Optional)
- **is_active** (boolean, default: false): Enable EMV 3DS authentication
- **challenge_preference** (string): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size** (string): full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- **contact_url** (string, max 256 chars): Support page URI
- **redirect_url** (string, max 256 chars): Post-authentication redirect URI
- **webhook_url** (string, max 256 chars): Webhook notification endpoint

---

## Response Codes

### 201 Created
Successful payment processed. Response includes transaction details, authorization codes, and card information.

### 400 Bad Request
Validation error. Response includes error type and invalid parameter details.

### 401 Unauthorized
Authentication failed. Check credentials.

### 404 Not Found
Resource not found (card_id may be invalid).

---

## Language/SDK Support

- Shell/cURL
- Node.js
- Ruby
- PHP
- Python

---

## Key Notes

- This endpoint reuses stored card data, reducing PCI compliance burden
- CVV must be provided for each transaction (not stored)
- Supports EMV 3D Secure authentication when enabled
- Custom data fields accept base64-encoded information
- Address information recommended for fraud prevention and EMV 3DS processing
