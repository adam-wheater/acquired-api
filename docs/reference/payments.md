# Process a Payment - API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments`

"Submit a payment request into the API and receive a response. If you want to create a card for future use, submit the full card details within the card object and set the `create_card` parameter to `true`"

## Authentication

- **Type**: Bearer Token (JWT)
- **Required Header**: Authorization with valid access token

## Request Headers

| Header | Type | Description |
|--------|------|-------------|
| Company-Id | UUID | Unique ID assigned by Acquired.com for your company |
| Mid | UUID | Unique ID connecting to a specific acquiring bank |

## Request Body Structure

### Transaction Object (Required)

| Field | Type | Description | Required |
|-------|------|-------------|----------|
| order_id | String | Unique reference for payment (2-50 chars, alphanumeric with hyphens) | Yes |
| amount | Float | Total amount to charge | Yes |
| currency | String | ISO 4217 code in lowercase | Yes |
| moto | Boolean | Set true for phone payments (default: false) | No |
| capture | Boolean | Set false to authorize only; true triggers sale (default: true) | No |
| custom_data | String | Base64 encoded custom data | No |
| custom1 | String | Additional customer/reference data (0-50 chars) | No |
| custom2 | String | Additional customer/reference data (0-50 chars) | No |

### Payment Object

**Card Details:**
- holder_name: String (2-45 chars, alphabetic)
- number: String (full card number)
- expiry_month: Integer (1-12)
- expiry_year: Integer (22-32)
- cvv: String (3-4 digits)
- create_card: Boolean (set true to save for future use)
- reference: String (1-18 chars, appears on bank statement when supported)

### Customer Object (Optional)

Includes customer_id or full customer details:
- first_name / last_name
- date_of_birth (format: YYYY-MM-DD)
- Billing address with line_1, line_2, city, state, postcode, country_code
- Shipping address (with address_match option)
- Email and phone contact information

### 3D Secure (TDS) Object (Optional)

- is_active: Boolean
- challenge_preference: Enum (challenge_mandated, challenge_preferred, no_challenge_requested, no_preference)
- challenge_window_size: Enum (full_screen, windowed sizes from 250x400 to 600x400)
- contact_url: URI to support page
- redirect_url: URI for post-authentication redirect
- webhook_url: Endpoint for notifications

## Supported Currencies

aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

## Response Codes

| Code | Status | Description |
|------|--------|-------------|
| 201 | Created | Payment processed successfully |
| 400 | Bad Request | Validation errors in request parameters |
| 401 | Unauthorized | Authentication failed |

## Key Notes

- If `capture` is false, transaction_type will be authorization only
- Full card details required unless using stored card_id
- "Required for MCC 6012 merchants" applies to last_name and dob fields
- Recommended fields include billing address and email for 3DS transactions
- Base URL for test environment: `https://test-api.acquired.com/v1`
- Base URL for production: `https://api.acquired.com/v1`
