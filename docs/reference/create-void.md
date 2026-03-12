# Process a Void

## Overview

Process a cancellation of a transaction before it settles through a consumer's account. This operation differs from refunds, which occur post-settlement.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/void`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/void`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/void`

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
| `transaction_id` | string (UUID) | Yes | Unique identifier for the transaction to be voided |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to specific acquiring bank |

## Key Notes

- Voids cancel transactions **before settlement**
- This operation differs from refunds, which occur **post-settlement**
- The transaction must be eligible for voiding based on its current status
- Once a transaction has been settled, it cannot be voided (use refund instead)

## Response Structure

Upon successful processing, the API returns transaction void details including:

- Transaction status updates
- Void confirmation details
- Timestamps for the operation

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Void processed successfully |
| 400 | Bad Request - Validation errors in request |
| 401 | Unauthorized - Authentication failed |
| 404 | Not Found - Transaction not found |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/void",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Explanation of validation failure"
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
  "instance": "/v1/transactions/{transaction_id}/void"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/void"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)
