# Process a Refund

## Overview

Process a refund for a specific payment. Append the required `transaction_id` to the URL and enter the amount to be refunded in the body of the request.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/refund`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/refund`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/refund`

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
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction being refunded |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Authorization` | string | Bearer token for API authentication (required) |
| `Company-Id` | string (UUID) | Unique ID assigned by Acquired for your company (optional) |

### Request Body Schema

The endpoint accepts JSON with the following structure:

```json
{
  "amount": 15.02,
  "reason": "customer_request",
  "metadata": {}
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | Yes | The refund amount in the transaction's original currency |
| `reason` | string | No | Reason code for the refund |
| `metadata` | object | No | Additional custom data for the refund |

## Important Notes

- Refunds can typically only be processed against captured transactions
- Partial refunds are generally supported if the remaining balance permits
- The refund amount must not exceed the original transaction amount

## Response Schemas

### 201 Created (Success)

Successful refund processing returns:

```json
{
  "status": "success",
  "refund_id": "string (uuid)",
  "transaction_id": "string (uuid)",
  "amount": 15.02,
  "currency": "gbp",
  "created": "2024-01-15T10:30:00Z",
  "reason": "customer_request"
}
```

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/refund",
  "invalid_parameters": [
    {
      "parameter": "amount",
      "reason": "Amount exceeds the original transaction amount"
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
  "instance": "/v1/transactions/{transaction_id}/refund"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/refund"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)
