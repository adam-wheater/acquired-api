# Generate a link_id - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-links`

This endpoint creates a payment link containing order or payment details. The returned `link_id` can be appended to your environment URL for sharing with customers.

**Format:** `https://test-pay.acquired.com/v1/{link_id}` (test) or `https://pay.acquired.com/v1/{link_id}` (production)

---

## Authentication

- **Type:** Bearer Token
- **Alternative:** JWT
- **Header:** Authorization: Bearer {access_token}

---

## Base URL

- **Test:** `https://test-api.acquired.com/v1/payment-links`
- **Production:** `https://api.acquired.com/v1/payment-links`

---

## Request Body Schema

The request accepts a JSON object with the following structure:

### Transaction Object (Required)

| Parameter | Type | Description | Example |
|-----------|------|-------------|---------|
| `order_id` | string | Unique reference for the payment request | `1f1f2a61-5b68-4725-a0ce-9560514ec00b` |
| `amount` | number | Total amount to charge | `15.02` |
| `currency` | string | ISO 4217 currency code (lowercase) | `gbp` |
| `moto` | boolean | Set true for phone orders | `false` |
| `capture` | boolean | Set false to authorize only | `true` |
| `custom_data` | string | Base64 encoded custom data | `L3BheW1lbnQtbGlua3MgcGF5IGN1c3RvbV9kYXRh` |
| `custom1` | string | Additional customer/reference data | Max 50 chars |
| `custom2` | string | Additional customer/reference data | Max 50 chars |

**Supported Currencies:** aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

### Payment Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `card` | object | Card payment details |
| `create_card` | boolean | Create card for future use (default: false) |
| `reference` | string | Bank statement reference (max 18 chars) |

**Card Details:**
- `holder_name` (required): Cardholder name
- `number` (required): Card number
- `expiry_month` (required): Month (1-12)
- `expiry_year` (required): Year (22-32)
- `cvv` (required): 3-4 digit security code

### Customer Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `customer_id` | string (UUID) | Existing customer ID |
| `reference` | string | Your customer reference |
| `first_name` | string | Customer first name |
| `last_name` | string | Customer last name |
| `dob` | string (date) | Date of birth (YYYY-MM-DD) |
| `custom_data` | string | Base64 encoded custom data |
| `billing` | object | Billing address details |
| `shipping` | object | Shipping address details |

**Address Fields:**
- `line_1`: First address line
- `line_2`: Second address line
- `city`: City
- `state`: State code (US only, 3 chars)
- `postcode`: Postal code
- `country_code`: ISO 3166 2-character code
- `email`: Email address
- `phone`: Phone object with `country_code` and `number`

### 3D Secure Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `is_active` | boolean | Enable EMV 3-D Secure authentication |
| `challenge_preference` | string | challenge_mandated, challenge_preferred, no_challenge_requested, no_preference |
| `challenge_window_size` | string | full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400 |
| `contact_url` | string (URI) | Support page URL |
| `redirect_url` | string (URI) | Post-authentication redirect URL |
| `webhook_url` | string (URI) | Webhook notification endpoint |

---

## Response Codes

### 201 Created

Successful payment link generation. Response includes Company-Id and Mid headers.

### 400 Bad Request

Validation failed. Response includes:
- `status`: "error"
- `error_type`: "validation"
- `title`: Human-readable error message
- `instance`: Location of error
- `invalid_parameters`: Array of parameter errors with `parameter` and `reason`

### 401 Unauthorized

Authentication failed. Response includes:
- `status`: "error"
- `error_type`: "unauthorized"
- `title`: "Authentication with the API failed, please check your details and try again."
- `instance`: Endpoint path

### 403 Forbidden

Access denied to requested resource. Response includes error details with `error_type`: "forbidden"

---

## Request Headers

| Header | Type | Description |
|--------|------|-------------|
| `Company-Id` | UUID | Unique ID for your company (optional) |
| `Mid` | UUID | Acquiring bank merchant ID (optional) |
| `Authorization` | string | Bearer token for authentication |

---

## Important Notes

- **MCC 6012 Requirements:** Merchants in this category must provide `last_name` and `dob` for customers
- **3DS Recommendation:** Customer billing address and email are recommended for EMV 3DS transactions
- **AVS Checks:** Full address and postcode recommended
- **Custom Data:** All custom_data fields must be Base64 encoded
- **Pattern Restrictions:** Fields have specific character patterns; special characters must match defined regex patterns
- **Webhook vs Redirect:** When 3DS is active with webhook_url inside tds object, use redirect_url for post-authentication navigation

---

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python
