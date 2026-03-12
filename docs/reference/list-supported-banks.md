# List Supported Banks

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/supported-banks`

**Description:** Returns a list of banks supported for Open Banking and variable recurring payment journeys.

## Authentication

**Type:** Bearer Token (JWT)

The endpoint requires authentication via Bearer token included in the authorization header of each request.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Details

### Parameters

No query parameters are required for this endpoint.

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |

## Response Structure

### Success Response (200 OK)

The response returns a paginated list of supported banks for open banking operations and variable recurring payment journeys.

**Meta Object:**
- `count` (integer): Number of records in response
- `offset` (integer): Starting record position
- `limit` (integer): Maximum records returnable
- `total` (integer): Complete record count in query
- `links` (array): Pagination navigation links

**Data Array:**
Contains bank objects with institution-specific information for open banking integrations.

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
        "href": "/v1/open-banking/supported-banks?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Request parameters failed validation",
  "instance": "/v1/open-banking/supported-banks",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "error description"
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
  "instance": "/v1/open-banking/supported-banks"
}
```

## HTTP Response Codes

| Status | Description |
|--------|-------------|
| 200 | Successful retrieval of supported banks |
| 400 | Invalid request parameters |
| 401 | Authentication failure - invalid credentials |

## Implementation Notes

This endpoint is part of the "Pay by Bank" / Variable Recurring Payments API section and facilitates variable recurring payment mandates by enabling selection from an approved financial institution roster.
