# Confirmation of Payee

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/tools/confirmation-of-payee`

## Purpose

This endpoint enables verification of payee account details prior to adding them to the system or initiating a payment transaction. It allows you to verify your payee's account details before adding them to our system or initiating a payment.

## Authentication

The endpoint supports two credential types:
- Bearer token authentication
- JWT authentication

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Request Structure

The API accepts a JSON request body containing payee verification details. The request includes account holder information and bank account details to validate against the financial institution's records.

### Expected Parameters

- **account_name** (string): Name of the account holder
- **sort_code** (string): Bank sort code (UK)
- **account_number** (string): Bank account number
- **account_type** (string): Type of account (personal/business)

## Response Codes

| Status | Description |
|--------|-------------|
| 201 Created | Successful verification |
| 400 Bad Request | Validation errors in request parameters |
| 401 Unauthorized | Authentication failure |
| 404 Not Found | Resource not found |

## Response Structure

### Success Response (201 Created)

Returns confirmation status indicating whether the payee details match the account holder information on file.

### Error Responses

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error details",
  "instance": "/v1/tools/confirmation-of-payee",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Additional Context

This tool operates within the broader Acquired API framework, which provides payment processing, customer management, and transaction handling capabilities. The confirmation of payee function serves as a validation utility within the payment ecosystem, helping to prevent misdirected payments and fraud.

## Code Examples

Available in: Shell, Node, Ruby, PHP, Python
