# List all Reconciliation Reports

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/reports/reconciliations`

**Description:** Retrieve a paginated list of available reconciliation reports, querying by Company-Id, Mid, date range, and specific fields.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Header Parameters

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum records returned per response (min: 1, max: 100) |
| `filter` | string | No | - | Filter which parameters appear in the response |
| `mid` | string | No | - | Filter results by merchant ID |
| `date_from` | string (date) | No | - | Start date for report range |
| `date_to` | string (date) | No | - | End date for report range |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays:

```json
{
  "meta": {
    "count": 10,
    "offset": 0,
    "limit": 25,
    "total": 50,
    "links": [
      {
        "rel": "self",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/reports/reconciliations?offset=40",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/reports/reconciliations?offset=10",
        "title": "Next page"
      }
    ]
  },
  "data": [
    {
      "report_id": "uuid",
      "company_id": "uuid",
      "mid": "uuid",
      "report_date": "date-time",
      "period_start": "date-time",
      "period_end": "date-time",
      "total_transactions": 0,
      "total_amount": 0.00,
      "currency": "string",
      "status": "string",
      "created": "date-time",
      "last_updated": "date-time"
    }
  ]
}
```

### Metadata Object

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records in current response |
| `offset` | integer | Starting record number |
| `limit` | integer | Maximum records per page |
| `total` | integer | Total records matching query |
| `links` | array | Pagination navigation links (self, first, last, prev, next) |

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation.",
  "instance": "/v1/reports/reconciliations",
  "invalid_parameters": [
    {
      "parameter": "string",
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
  "title": "Authentication with the API failed, please verify credentials.",
  "instance": "/v1/reports/reconciliations"
}
```

**403 Forbidden:** Insufficient permissions for requested resource.

## Related Endpoints

- Retrieve a Reconciliation Report: GET `/reports/reconciliations/{reconciliation_id}`
