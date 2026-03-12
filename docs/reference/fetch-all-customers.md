# List All Customers - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/customers`

### Description

Returns a list of all customers. Customers are returned by creation date, with the most recently created showing first.

By default, the endpoint returns 25 records unless modified via the `limit` query parameter. You can filter returned parameters using the `filter` query parameter.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization header required with access token

## Query Parameters

| Parameter | Type | Required | Default | Min | Max | Description |
|-----------|------|----------|---------|-----|-----|-------------|
| `offset` | integer | No | 0 | - | - | The record number to start the response on |
| `limit` | integer | No | 25 | 1 | 100 | Maximum records returned per response |
| `filter` | string | No | - | - | - | Specify which parameters to return in the response |
| `reference` | string | No | - | - | - | Filter results by customer reference value |

### Header Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `Company-Id` | UUID | No | Your unique company identifier |

## Response Schema (200 OK)

The response includes pagination metadata and an array of customer objects.

### Metadata Structure

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      {
        "rel": "self",
        "href": "/v1/customers?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/customers?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/customers?offset=125",
        "title": "Last page"
      },
      {
        "rel": "prev",
        "href": "/v1/customers?offset=0",
        "title": "Previous page"
      },
      {
        "rel": "next",
        "href": "/v1/customers?offset=25",
        "title": "Next page"
      }
    ]
  }
}
```

### Customer Object Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `company_id` | UUID | Yes | Unique company identifier assigned by Acquired |
| `reference` | string | No | Your unique customer reference (max 50 chars) |
| `first_name` | string | No | Customer's first name (max 22 chars) |
| `last_name` | string | No | Customer's last name (max 22 chars) |
| `dob` | date | No | Date of birth in YYYY-MM-DD format |
| `custom_data` | string | No | Base64-encoded custom data |
| `billing` | object | No | Billing address and contact details |
| `shipping` | object | No | Shipping address and contact details |
| `created` | date-time | Yes | Record creation timestamp |
| `last_updated` | date-time | Yes | Last modification timestamp |

### Address Object (Billing/Shipping)

```json
{
  "address": {
    "line_1": "string (max 50 chars)",
    "line_2": "string (max 50 chars)",
    "city": "string (max 40 chars)",
    "state": "string (max 3 chars - US only)",
    "postcode": "string (max 40 chars)",
    "country_code": "string (2-char ISO 3166 code)"
  },
  "email": "string (email format)",
  "phone": {
    "country_code": "string (1-3 digits)",
    "number": "string (max 15 digits)"
  }
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

### 401 Unauthorized

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, check your details and try again.",
  "instance": "/v1/customers"
}
```

## Additional Notes

- Results are ordered by creation date, newest first
- The `filter` parameter allows selective field returns to optimize responses
- Pagination links include "self", "first", "last", "prev", and "next" relations
- All date fields use ISO 8601 format
