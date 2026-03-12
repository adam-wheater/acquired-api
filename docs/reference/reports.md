# Reports - Overview

## Section Overview

The Reports section of the Acquired.com API provides endpoints for accessing reconciliation reports and transaction data.

## Available Endpoints

### List all Reconciliation Reports
**GET** `/reports/reconciliations`
Retrieve a paginated list of available reconciliation reports, querying by Company-Id, Mid, date range, and specific fields.

### Retrieve a Reconciliation Report
**GET** `/reports/reconciliations/{reconciliation_id}`
Fetch a specific reconciliation report by its unique identifier.

## Authentication

All endpoints in this section require Bearer Token (JWT) authentication.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Common Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

## Common Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter which parameters appear in the response |
| `date_from` | date | No | - | Start of date range for report filtering |
| `date_to` | date | No | - | End of date range for report filtering |

## Pagination Response Format

```json
{
  "meta": {
    "count": 0,
    "offset": 0,
    "limit": 0,
    "total": 0,
    "links": [
      {
        "rel": "first|last|prev|next|self",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

## Error Response Format

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|forbidden",
  "title": "Human-readable error message",
  "instance": "/v1/reports/reconciliations",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Usage Notes

- Reports are paginated; use offset/limit for navigation
- Filter parameter enables selective field returns
- Date range parameters enable time-based filtering
- Timestamps return in ISO 8601 format

## Code Examples

Available in: Shell, Node, Ruby, PHP, Python
