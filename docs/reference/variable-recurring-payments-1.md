# Variable Recurring Payments (VRP) - Overview

## Section Overview

The Variable Recurring Payments section of the Acquired.com API provides endpoints for managing Open Banking VRP mandates and payments. VRP enables merchants to collect variable amounts from customers through Open Banking connections with their banks.

## Available Endpoints

### List Supported Banks
**GET** `/open-banking/supported-banks`
Returns a list of banks supported for Open Banking and variable recurring payment journeys.

### Create a Mandate
**POST** `/open-banking/mandates`
Creates a mandate and returns the details needed to direct the customer to their bank for authorization.

### List Mandates
**GET** `/open-banking/mandates`
Returns a paginated list of mandates.

### Retrieve an Open Banking Mandate
**GET** `/open-banking/mandates/{mandate_id}`
Retrieves an open banking mandate using a unique identifier.

### Initiate a Variable Recurring Payment
**POST** `/open-banking/vrps`
Initiates a variable recurring payment against an established mandate.

### List Variable Recurring Payments
**GET** `/open-banking/vrps`
Returns a paginated list of variable recurring payments.

### Confirm Funds Check Against a Mandate
**POST** `/open-banking/mandates/{mandate_id}/confirm-funds`
Initiates a confirmation of funds check against an established mandate.

## Authentication

All endpoints in this section require Bearer Token (JWT) authentication.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Pagination

List endpoints support pagination through:
- `offset` (integer): The record to start the response on
- `limit` (integer): Maximum number of records returned (1-100)
- `filter` (string): Filter parameters to return within response

### Pagination Response Structure

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
        "href": "/v1/path?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

## Error Response Format

All endpoints return errors in the standard format:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error message",
  "instance": "/v1/endpoint-path",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```
