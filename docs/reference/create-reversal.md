# Process a Reversal

## Overview

This endpoint allows you to automatically determine whether a payment should be voided or refunded based on settlement status and acquiring bank rules. The system supports both full and partial reversals.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/reversal`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/reversal`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/reversal`

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
| `transaction_id` | string (UUID) | Yes | Unique identifier for the transaction being reversed |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

### Request Body Schema

The API accepts a JSON payload with the following structure:

```json
{
  "amount": 15.02,
  "reason": "customer_request",
  "custom_data": "base64encodedstring",
  "custom1": "reference_value_1",
  "custom2": "reference_value_2"
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | No | The reversal amount. If not provided, processes a **full reversal** |
| `reason` | string | No | Reason code for the reversal |
| `custom_data` | string | No | Base64 encoded string for custom metadata |
| `custom1` | string | No | Additional reference data (max 50 characters, alphanumeric and special characters) |
| `custom2` | string | No | Additional reference data (max 50 characters, alphanumeric and special characters) |

## Key Features

- **Automatic Determination:** The system intelligently decides between void and refund operations based on settlement status
- **Full & Partial Support:** Process complete or partial transaction reversals
- **Settlement Aware:** Takes into account current settlement status of transactions
- **Bank Rule Compliance:** Adheres to acquiring bank-specific requirements

## Usage Notes

- Reversals can typically only be processed on recent transactions
- The specific reversal type (void vs. refund) depends on settlement timing
- Partial reversals require an explicit `amount` parameter
- Full reversals process when no `amount` is specified

## Response Schemas

### 201 Created (Success)

Successful reversal processing returns:

```json
{
  "transaction_id": "string (uuid)",
  "status": "string",
  "reversal_type": "string",
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
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/reversal",
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
  "instance": "/v1/transactions/{transaction_id}/reversal"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/reversal"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)
