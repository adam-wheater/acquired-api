# List Mandates (Open Banking / VRP)

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates`

Returns a paginated list of mandates from the Acquired API's Variable Recurring Payments section.

## Authentication

**Type:** Bearer Token (JWT)

This endpoint requires Bearer token authentication via JWT credentials.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum number of records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter the parameters that you want to return within the response |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |

## Response Format

### Success Response (200 OK)

The response follows a paginated structure:

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
        "href": "/v1/open-banking/mandates?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/open-banking/mandates?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/open-banking/mandates?offset=40",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/open-banking/mandates?offset=10",
        "title": "Next page"
      }
    ]
  },
  "data": []
}
```

**Metadata Object:**
- `count` - Number of records returned
- `offset` - Starting record number
- `limit` - Maximum records that can be returned
- `total` - Total records in query response
- `links` - Array of navigation links (self, first, last, prev, next)

**Data Array:** Contains mandate objects with complete details

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/mandates",
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
  "instance": "/v1/open-banking/mandates"
}
```

**404 Not Found:** Resource not found

## Related Endpoints

- Create a mandate: POST `/open-banking/mandates`
- Retrieve a mandate: GET `/open-banking/mandates/{mandate_id}`
- List supported banks: GET `/open-banking/supported-banks`
