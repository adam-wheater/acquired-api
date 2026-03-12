# List All Transactions

## Overview

Retrieve all transactions with optional filtering capabilities. If no filters are provided, only transactions from the current day will be returned.

## Endpoint Details

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/transactions`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions`
- **Production Environment:** `https://api.acquired.com/v1/transactions`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Query Parameters

The endpoint supports filtering through the following query parameters:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `start_date` | string | No | Filter transactions after this date |
| `end_date` | string | No | Filter transactions before this date |
| `order_id` | string | No | Filter by specific order identifier |
| `status` | string | No | Filter by transaction status |
| `currency` | string | No | Filter by currency code (ISO 4217, e.g. `gbp`, `usd`, `eur`) |
| `reason` | string | No | Filter by transaction reason |
| `payment_method` | string | No | Filter by payment method used |
| `transaction_type` | string | No | Filter by type of transaction |
| `recurring_type` | string | No | Filter by recurring payment type |
| `charge_date` | string | No | Filter by charge date |
| `offset` | integer | No | Starting record number (default: 0) |
| `limit` | integer | No | Maximum records returned (1-100) |
| `filter` | string | No | Specify which response parameters to return |

## Default Behavior

**Important:** If no filters are provided, only transactions from the current day will be returned.

## Request Example

```
GET https://test-api.acquired.com/v1/transactions?start_date=2024-01-01&end_date=2024-01-31&status=success&limit=50
```

## Response Structure

Responses follow a paginated format including metadata and transaction data arrays, with navigation links for pagination control.

The response includes:
- Pagination metadata (total count, offset, limit)
- Array of transaction objects
- Navigation links for paginating through results

## Key Features

- Filtering flexibility across multiple transaction attributes
- Default daily transaction retrieval without filters
- Pagination support for managing large result sets via `offset` and `limit`
- Bearer token authentication required
- Response field filtering via the `filter` parameter

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Transactions successfully retrieved |
| 400 | Bad Request - Validation error in request parameters |
| 401 | Unauthorized - Authentication credentials invalid or missing |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions",
  "invalid_parameters": [
    {
      "parameter": "start_date",
      "reason": "Invalid date format"
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
  "instance": "/v1/transactions"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
