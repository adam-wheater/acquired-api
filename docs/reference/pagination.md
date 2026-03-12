# Pagination Documentation

## Overview

The Acquired API implements pagination to manage large result sets efficiently. "When making calls to the Acquired.com API, a lot of the time, there will be a number of results to return."

**Default behavior:** 25 results returned
**Maximum limit:** 100 results per request
**Exceeding limits:** Returns either a 400 Bad Request or automatically caps results at 100

## Query Parameters

| Field | Type | Description | Default |
|-------|------|-------------|---------|
| `filter` | string | Filter parameters to return in the response | N/A |
| `limit` | integer | Number of values returned (max 100) | 25 |
| `offset` | integer | Record to start the response on | 0 |

### Example Request
```
{{url}}/customers?filter=customer_id,dob,last_name&limit=30&offset=2
```

### Example Response
```json
{
  "meta": {
    "count": 30,
    "offset": 2,
    "limit": 30,
    "total": 88
  },
  "data": [
    {
      "customer_id": "5b18a0c0-05de-dfe3-6309-cc703723b0bf",
      "dob": "1994-01-20",
      "last_name": "Barrow"
    }
  ]
}
```

## Metadata Responses

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records returned in response |
| `offset` | integer | Starting object position (matches request offset) |
| `limit` | integer | Maximum records per response |
| `total` | integer | Total records available in query |

### Sample Metadata Examples

**GET /customers** (77 total records)
```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 77
  }
}
```

**GET /customers?limit=15**
```json
{
  "meta": {
    "count": 15,
    "offset": 0,
    "limit": 15,
    "total": 77
  }
}
```

**GET /customers?offset=5**
```json
{
  "meta": {
    "count": 25,
    "offset": 5,
    "limit": 25,
    "total": 77
  }
}
```

**GET /customers?offset=5&limit=5**
```json
{
  "meta": {
    "count": 5,
    "offset": 5,
    "limit": 5,
    "total": 77
  }
}
```

## HATEOAS

The API implements HATEOAS (Hypermedia As The Engine Of Application State) through `links` parameters, allowing clients to discover actions dynamically from resources themselves.

**Endpoints returning links:**
- /payments
- /payments/apple-pay
- /payments/google-pay
- /payments/recurring
- /transactions/{transaction_id}/refund
- /transactions/{transaction_id}/void
- /transactions/{transaction_id}/capture

### Payment Response Example
```json
{
  "transaction_id": "29c05255-8905-f38f-1446-03f83de52d7c",
  "status": "success",
  "issuer_response_code": "00",
  "links": [
    {
      "rel": "self",
      "href": "/v1/transactions/29c05255-8905-f38f-1446-03f83de52d7c"
    }
  ]
}
```

Submit a subsequent GET request to the `href` value to retrieve transaction details.
