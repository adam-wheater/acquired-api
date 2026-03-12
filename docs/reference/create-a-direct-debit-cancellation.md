# Cancel a Direct Debit

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/cancel`

## Description

This endpoint enables cancellation of Direct Debit transactions prior to submission to banking institutions. The operation stores any custom data against the cancellation event and will fail if the transaction is not in pending status.

## Authentication

- **Type:** Bearer Token (JWT)
- **Requirement:** Required for this endpoint

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction to cancel |

## Request Body

The endpoint accepts custom data to be stored with the cancellation event:

```json
{
  "custom_data": "Base64 encoded string detailing custom data"
}
```

## Response Schemas

### Success Response (200 OK)

```json
{
  "status": "success",
  "transaction_id": "UUID",
  "cancelled_at": "ISO 8601 datetime"
}
```

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/cancel",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/transactions/{transaction_id}/cancel"
}
```

**404 Not Found:**

Transaction ID does not exist or is invalid.

## Key Constraints

- Transaction must be in **pending status** to cancel successfully
- Operation cannot proceed if transaction has already been submitted to banks
- Custom data must be Base64 encoded if provided

## Related Operations

This function operates within the Direct Debit section alongside:
- Create a mandate
- Create a collection
- Cancel a mandate
- Process a retry
- Retrieve mandate details
