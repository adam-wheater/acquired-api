# Process a Capture

## Overview

Process an authorisation of the transaction and then capture the funds later. Append the required `transaction_id` to the URL and include the capture amount in the request body. This operation converts an authorization hold into an actual charge on the cardholder's account.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/capture`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/capture`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/capture`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction to be captured |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique company identifier assigned by Acquired.com |
| `Mid` | string (UUID) | No | Merchant ID assigned by Acquired.com connecting to specific acquiring bank |

### Request Body Schema

The capture request accepts an amount specification to allow partial captures:

```json
{
  "amount": 15.02
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | Yes | The monetary value to capture from the authorized transaction |

## Key Concepts

- A capture operation requires a preceding authorization. The merchant must first process an authorization (by setting `capture: false` during payment creation), then later submit the capture request to complete the transaction lifecycle.
- The capture amount can be **less than** the originally authorized amount, enabling **partial capture** scenarios.
- The remaining authorized balance typically becomes available for release back to the cardholder.
- If no amount is specified, the full authorized amount may be captured.

## Response Format

### Success Response (200 OK)

```json
{
  "transaction_id": "string (UUID)",
  "status": "string",
  "amount": 15.02,
  "currency": "gbp",
  "transaction_type": "string",
  "created": "2024-01-15T10:30:00Z",
  "reference": "string"
}
```

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Capture processed successfully |
| 400 | Bad Request - Your request parameters did not pass validation |
| 401 | Unauthorized - Authentication failed |
| 404 | Not Found - Resource does not exist |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/capture",
  "invalid_parameters": [
    {
      "parameter": "amount",
      "reason": "Amount exceeds the authorized amount"
    }
  ]
}
```

### 401 Unauthorized

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/transactions/{transaction_id}/capture"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/capture"
}
```

## Related Endpoints

- [Create Payment](transactions.md) - Initial authorization and payment processing
- [Process a refund](create-refund.md) - Reversing previously captured amounts
- [Process a void](create-void.md) - Canceling an authorization before capture occurs
- [Process a reversal](create-reversal.md) - Automatic void/refund determination
- [List all transactions](fetch-all-transactions.md) (GET)
