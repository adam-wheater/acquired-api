# Retrieve a Transaction by ID

## Overview

The "Retrieve a transaction" endpoint is a GET request that allows you to fetch transaction details using a transaction ID returned from payment submissions. Every time you submit a payment request, including card, recurring payments, Google Pay, Apple Pay, you are returned a `transaction_id`. Append the `transaction_id` to the URL to retrieve the transaction details.

## Endpoint Details

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

Available credential types:
- Bearer Token
- JWT

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the transaction |

## Request Example

```
GET https://test-api.acquired.com/v1/transactions/[transaction_id]
```

No request body is required.

## Response Structure

The response returns complete transaction details in JSON format including:

- Transaction status and type
- Amount and currency information
- Payment method details
- Customer information
- Timestamp data
- Authorization codes and references

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Transaction successfully retrieved |
| 400 | Bad Request - Validation error in request parameters |
| 401 | Unauthorized - Authentication credentials invalid or missing |
| 404 | Not Found - Transaction ID does not exist |

## Error Response Format

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error description",
  "instance": "/v1/transactions/{transaction_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Explanation of validation failure"
    }
  ]
}
```

## Supported Languages

Code examples available in:
- Shell
- Node
- Ruby
- PHP
- Python

## Related Endpoints

- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)
