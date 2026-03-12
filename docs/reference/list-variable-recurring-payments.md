# List Variable Recurring Payments

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/vrps`
**Description:** Returns a paginated list of variable recurring payments.

## Authentication

**Type:** Bearer Token (JWT)
**Required Header:** Authorization with valid access token

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum number of records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter specific parameters to return in response |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays.

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
        "href": "/v1/open-banking/vrps?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

### Metadata Object

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records in current response |
| `offset` | integer | Starting position of records |
| `limit` | integer | Maximum records per request |
| `total` | integer | Total records available |
| `links` | array | Navigation links for pagination |

### Link Relations

- `self` - Current resource link
- `first` - First page link
- `last` - Last page link
- `prev` - Previous page link
- `next` - Next page link

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/vrps",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Detailed error description"
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
  "instance": "/v1/open-banking/vrps"
}
```

## Pagination Details

The API implements cursor-based pagination through `offset` and `limit` parameters. The response metadata includes:

- **count:** Number of records in current response
- **offset:** Starting position of records
- **limit:** Maximum records per request
- **total:** Total records available
- **links:** Navigation links for pagination

## Implementation Notes

- Responses are ordered by creation date with most recently created items appearing first
- Default behavior returns 25 most recent objects unless the `limit` parameter specifies otherwise
- Use the `filter` parameter to control which fields appear in responses

## Related Endpoints

This endpoint is part of the Variable Recurring Payments section:
- List supported banks: GET `/open-banking/supported-banks`
- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- Retrieve a mandate: GET `/open-banking/mandates/{mandate_id}`
- Initiate a VRP: POST `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`
