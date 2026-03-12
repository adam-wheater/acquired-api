# Create a Collection

## Overview

This endpoint enables merchants to establish a Direct Debit collection against a `mandate_id` to collect payments directly from a customer's bank account.

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/collections`

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization with access token

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Headers

| Header | Type | Description |
|--------|------|-------------|
| Authorization | string | Bearer token for authentication |
| Company-Id | string (uuid) | Your assigned company identifier |

### Body Schema

The request accepts a JSON payload with the following structure:

```json
{
  "mandate_id": "string (uuid)",
  "amount": "number",
  "currency": "string",
  "reference": "string",
  "custom_data": "string (base64 encoded)"
}
```

### Required Fields
- `mandate_id` (string, UUID): Unique identifier for the mandate
- `amount` (number): Transaction amount
- `currency` (string): ISO 4217 currency code (lowercase)

### Optional Fields
- `reference` (string): Custom reference for tracking
- `custom_data` (string): Base64-encoded custom information

## Response

### Success Response (201 Created)

The endpoint returns a collection object containing:
- `collection_id`: Unique identifier for the collection
- `mandate_id`: Associated mandate reference
- `status`: Current collection status
- `amount`: Transaction amount processed
- `currency`: Currency used
- `created`: Timestamp of creation

### Error Responses

**400 Bad Request** - Validation errors with invalid parameters

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/payments/collections",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Authentication failure

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/payments/collections"
}
```

**404 Not Found** - Mandate or resource not found

## Related Operations

This endpoint is part of the Direct Debit payment methods section, which includes:
- Create a mandate
- Cancel a mandate
- Cancel Direct Debit
- Process a retry
- Retrieve mandate details

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python
