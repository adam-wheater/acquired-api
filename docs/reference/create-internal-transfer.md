# Internal Transfer API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/internal-transfer`
**Description:** "Process an internal transfer between accounts."

## Authentication

The endpoint requires Bearer token authentication via JWT credentials.

## Request Structure

### Headers
- `Company-Id` (UUID): Required header identifying your company
- `Mid` (UUID): Optional header for specific acquiring bank connection

### Request Body

The request accepts a JSON payload with the following structure:

```json
{
  "transaction": {
    "order_id": "string",
    "amount": "number",
    "currency": "string",
    "custom_data": "string",
    "custom1": "string",
    "custom2": "string"
  },
  "payment": {
    "reference": "string"
  }
}
```

### Transaction Parameters

| Parameter | Type | Requirements | Details |
|-----------|------|--------------|---------|
| `order_id` | String | Required | Unique reference; 2-50 characters, pattern: `[\w\-]*` |
| `amount` | Number | Required | Total charge amount (float format) |
| `currency` | String | Required | ISO 4217 code (lowercase): aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar |
| `custom_data` | String | Optional | Base64 encoded custom data |
| `custom1` | String | Optional | Additional reference data; max 50 characters |
| `custom2` | String | Optional | Additional reference data; max 50 characters |

### Payment Parameters

| Parameter | Type | Details |
|-----------|------|---------|
| `reference` | String | Bank statement reference (1-18 characters, pattern: `[\w\- ]*`) |

## Response Format

### Success Response (201 Created)

```json
{
  "transaction_id": "string",
  "status": "string",
  "amount": "number",
  "currency": "string"
}
```

### Error Responses

**400 Bad Request** - Validation failure
**401 Unauthorized** - Authentication failure
**404 Not Found** - Resource not found

Error responses include:
- `status`: "error"
- `error_type`: validation, unauthorized, or conflict
- `title`: Human-readable error message
- `instance`: Where error occurred
- `invalid_parameters`: Array of failed parameters with reasons

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Key Notes

The documentation mentions "expand responses" and "pagination" are available features for related API operations. This endpoint is part of the Faster Payments section within the Acquired API suite.
