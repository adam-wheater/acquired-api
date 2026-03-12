# Retrieve a Reconciliation Report

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/reports/reconciliations/{reconciliation_id}`

This endpoint allows you to fetch a specific reconciliation report by providing the `reconciliation_id` in the URL path.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `reconciliation_id` | string (UUID) | Yes | Unique identifier for the reconciliation report |

## Request Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID for your company |

## Response Format

### Success Response (200 OK)

The response returns a specific reconciliation report object containing:
- `report_id`: Unique identifier
- `company_id`: Associated company
- `mid`: Merchant ID
- `report_date`: Date of the report
- `period_start`: Start of reporting period
- `period_end`: End of reporting period
- `total_transactions`: Number of transactions
- `total_amount`: Total settlement amount
- `currency`: Currency code
- `status`: Report status
- `created`: Creation timestamp
- `last_updated`: Last update timestamp

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/reports/reconciliations/{reconciliation_id}",
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
  "instance": "/v1/reports/reconciliations/{reconciliation_id}"
}
```

**404 Not Found:** The specified reconciliation report does not exist.

## Implementation Notes

- The reconciliation report retrieval is a read-only operation
- The `reconciliation_id` parameter must be a valid UUID format
- Must correspond to an existing reconciliation report in your account

## Related Endpoints

- List all Reconciliation Reports: GET `/reports/reconciliations`
