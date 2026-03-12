# Send Funds to a Card - API Documentation

## Overview

This endpoint enables direct fund transfers to customer cards using Visa Direct and Mastercard Send technologies. The API accepts transaction details and card information to initiate transfers.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/credit`

## Authentication

The endpoint requires Bearer token authentication via JWT.

## Request Structure

### Headers
- `Company-Id` (UUID): Your company identifier assigned by Acquired
- `Mid` (UUID): Merchant ID connecting to your acquiring bank

### Request Body

The request follows this general structure:

```json
{
  "transaction": {
    "order_id": "string",
    "amount": "number",
    "currency": "string",
    "moto": "boolean",
    "capture": "boolean",
    "custom_data": "string",
    "custom1": "string",
    "custom2": "string"
  },
  "payment": {
    "card": {
      "holder_name": "string",
      "number": "string",
      "expiry_month": "integer",
      "expiry_year": "integer",
      "cvv": "string"
    },
    "reference": "string"
  },
  "customer": {
    "customer_id": "UUID",
    "reference": "string",
    "first_name": "string",
    "last_name": "string",
    "dob": "date",
    "custom_data": "string",
    "billing": {},
    "shipping": {}
  }
}
```

### Transaction Parameters

| Parameter | Type | Description | Required |
|-----------|------|-------------|----------|
| `order_id` | string | Your unique reference for the payment | Yes |
| `amount` | number | Transfer amount (decimal format) | Yes |
| `currency` | string | ISO 4217 code (lowercase) | Yes |
| `moto` | boolean | Phone-based payment indicator | No |
| `capture` | boolean | Auto-capture funds (default: true) | No |
| `custom_data` | string | Base64-encoded custom metadata | No |
| `custom1` | string | Additional reference data | No |
| `custom2` | string | Additional reference data | No |

### Supported Currencies

aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

### Payment Card Parameters

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, letters/spaces | Card holder's name |
| `number` | string | Valid card number | Full card number |
| `expiry_month` | integer | 1-12 | Expiration month |
| `expiry_year` | integer | 22-32 | Two-digit year |
| `cvv` | string | 3-4 digits | Security code |

### Customer Information

Customer data can include:
- Basic details (first/last name, DOB)
- Billing address with postal/country codes
- Shipping address (with option to match billing)
- Contact information (email, phone with country code)

**Note:** Last name and DOB are required for MCC 6012 merchants.

## Response Format

A successful transfer returns a 201 status with transaction confirmation details including:
- Transaction ID
- Status confirmation
- Settlement information

## Error Responses

The API returns standardized error responses:

**400 Bad Request** - Validation failures with specific parameter issues
**401 Unauthorized** - Authentication failures
**404 Not Found** - Resource doesn't exist

Error responses include:
- `status`: Error indicator
- `error_type`: Category (validation, unauthorized, forbidden)
- `title`: Human-readable explanation
- `instance`: Endpoint where error occurred
- `invalid_parameters`: Array of specific validation failures

## Key Considerations

- Billing address information improves AVS checks
- Email addresses support EMV 3DS processing
- Phone numbers require international dialing codes
- Custom data must be Base64-encoded
- The `reference` parameter appears on cardholders' bank statements when supported
