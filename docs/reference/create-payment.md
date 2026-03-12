# Process a Payment - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments`

**Description:** "Submit a payment request into the API and receive a response. If you want to create a card for future use, submit the full card details within the card object and set the `create_card` parameter to `true`"

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization header required with Bearer token

## Required Headers

| Header | Type | Description |
|--------|------|-------------|
| Company-Id | UUID | Unique identifier assigned by Acquired.com for your company |
| Mid | UUID | Unique ID connecting to a specific acquiring bank |

## Request Body Schema

### Transaction Object (Required)

| Parameter | Type | Description | Required |
|-----------|------|-------------|----------|
| order_id | String | Unique reference for payment request (2-50 chars, alphanumeric with hyphens) | Yes |
| amount | Float | Total amount to charge | Yes |
| currency | String | ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar) | Yes |
| moto | Boolean | Set true for phone orders (default: false) | No |
| capture | Boolean | Set false to authorize only; true = sale (default: true) | No |
| custom_data | String | Base64 encoded custom data | No |
| custom1 | String | Additional internal reference (0-50 chars) | No |
| custom2 | String | Additional internal reference (0-50 chars) | No |

### Payment Object

**Card Object:**
- `holder_name` (String, 2-45 chars): Cardholder name (required)
- `number` (String): Full card number (required)
- `expiry_month` (Integer, 1-12): Expiration month (required)
- `expiry_year` (Integer, 22-32): Expiration year (required)
- `cvv` (String, 3-4 chars): Security code (required)

**Additional Parameters:**
- `create_card` (Boolean): Set true to save card for future use (default: false)
- `reference` (String, 1-18 chars): Card statement reference where supported

### Customer Object

The customer data structure includes:
- `customer_id` (UUID): Existing customer reference
- `reference` (String): Your unique customer identifier
- `first_name` (String, 0-22 chars)
- `last_name` (String, 0-22 chars) - *Required for MCC 6012 merchants*
- `dob` (Date): Date of birth - *Required for MCC 6012 merchants*
- `custom_data` (String): Base64 encoded data

**Billing Address:**
- `line_1` (0-50 chars): *Recommended for 3DS and AVS*
- `line_2` (0-50 chars)
- `city` (1-40 chars)
- `state` (0-3 chars): US state code only
- `postcode` (1-40 chars): *Recommended for MCC 6012; Required for 3DS/AVS*
- `country_code` (2 chars): ISO 3166 code - *Recommended for 3DS*

**Shipping Address:**
- `address_match` (Boolean): Auto-populate if same as billing
- Same structure as billing address

**Contact Information:**
- `email` (String): Email format - *Recommended for 3DS*
- `phone.country_code` (String, 1-3 digits)
- `phone.number` (String, 0-15 digits)

### 3D Secure (TDS) Object

| Parameter | Type | Values | Description |
|-----------|------|--------|-------------|
| is_active | Boolean | true/false | Enable EMV 3DS authentication |
| challenge_preference | String | challenge_mandated, challenge_preferred, no_challenge_requested, no_preference | Cardholder challenge preference |
| challenge_window_size | String | full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400 | Preferred window size |
| contact_url | URI | Max 256 chars | Link to support page |
| redirect_url | URI | Max 256 chars | Post-authentication redirect location |
| webhook_url | URI | Max 256 chars | Notification endpoint for auth process |

## Response Codes

**201 Created:** Successful payment submission
- Headers include Company-Id and Mid

**400 Bad Request:** Validation errors
- Returns error_type: "validation"
- Includes invalid_parameters array with parameter names and reasons

**401 Unauthorized:** Authentication failed
- error_type: "unauthorized"
- Message: "Authentication with the API failed, please check your details and try again"

## Response Schema Structure

Successful responses include transaction details, card information, customer data, and authorization results with relevant timestamps and status indicators.

## Key Notes

- Full card details required for processing
- "Recommended when processing EMV 3DS transactions" - address and email fields enhance security
- Base64 encoding used for custom_data fields
- Phone numbers follow international dialing code format
- Card expiry years range from 22-32 (representing 2022-2032)
