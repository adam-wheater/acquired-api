# Initiate a Variable Recurring Payment

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/vrps`

This endpoint enables the initiation of a variable recurring payment against an established mandate.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Structure

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

### Body Schema

The API accepts JSON payloads with the following components:

#### Core Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | The unique identifier of the mandate authorizing the recurring payment |
| `amount` | number | Yes | The payment amount to be processed |
| `currency` | string | Yes | ISO 4217 currency code (lowercase) |
| `description` | string | No | Payment description for record-keeping |
| `reference` | string | No | Your unique reference identifier for this transaction |

### Example Request Body

```json
{
  "mandate_id": "550e8400-e29b-41d4-a716-446655440000",
  "amount": 25.99,
  "currency": "gbp",
  "description": "Monthly subscription payment",
  "reference": "PAY-2026-001"
}
```

## Response Codes

### Success Response (201 Created)

Returns transaction details including:
- `transaction_id`: Unique identifier for the payment
- `mandate_id`: Associated mandate
- `status`: Current payment status
- `amount`: Amount processed
- `currency`: Currency used
- `created`: Timestamp of creation

### Error Responses

**400 Bad Request** - Invalid parameters or missing required fields

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/vrps",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Invalid or expired access token

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/open-banking/vrps"
}
```

**404 Not Found** - Mandate ID does not exist or is invalid

## Related Endpoints

Within the Variable Recurring Payments section:
- List supported banks: GET `/open-banking/supported-banks`
- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- List VRPs: GET `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python

## Additional Resources

Refer to the Fundamentals section for guidance on handling errors, pagination, and expanding response data.
