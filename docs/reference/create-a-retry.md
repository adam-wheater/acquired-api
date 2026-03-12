# Process a Retry

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/retry`

## Description

This endpoint enables retrying Direct Debit transactions that have been declined, specifically in cases where the failure occurred due to insufficient funds.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Identifier for the transaction to retry |

## Request Body

The request body may include custom data to associate with the retry event:

```json
{
  "custom_data": "Base64 encoded string"
}
```

## Response Codes

| Status | Description |
|--------|-------------|
| 201 | Created/Success - Retry initiated |
| 400 | Bad Request - Validation errors |
| 401 | Unauthorized - Authentication failures |
| 404 | Not Found - Transaction not found |

### Error Response Format

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/retry",
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
  "instance": "/v1/transactions/{transaction_id}/retry"
}
```

## Implementation Languages

Code examples available in: Shell, Node, Ruby, PHP, Python

## Related Endpoints

This retry functionality is part of the Direct Debit section, which also includes:
- Create a mandate
- Create a collection
- Cancel a mandate
- Cancel a Direct Debit
- Retrieve a mandate

## Security Notes

Requests require valid bearer token authentication obtained through the `/login` endpoint using your `app_id` and `app_key`.
