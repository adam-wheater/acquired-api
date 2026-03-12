# Retrieve a Mandate (Direct Debit)

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/mandates/{mandate_id}`

**Description:** This endpoint allows you to fetch information associated with a specific mandate by providing the necessary `mandate_id` parameter in your request.

## Authentication

**Type:** Bearer Token (JWT)

Required for all requests to this endpoint.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the mandate |

## Request Example

```
GET https://test-api.acquired.com/v1/mandates/{mandate_id}
Authorization: Bearer <access_token>
Company-Id: <company_uuid>
```

## Response Format

The endpoint returns mandate-related data in JSON format upon successful retrieval.

### Success Response (200 OK)

Returns the full mandate object including:
- `mandate_id`: Unique identifier
- `customer_id`: Associated customer
- `status`: Current mandate status
- `mandate_reference`: Your reference
- `bank_account`: Account details
- `scheme`: Payment scheme
- `created`: Creation timestamp
- `updated`: Last update timestamp

## HTTP Status Codes

- **200 OK** - Successful retrieval of mandate information
- **400 Bad Request** - Invalid request parameters
- **401 Unauthorized** - Authentication failed or invalid credentials
- **404 Not Found** - The specified mandate does not exist

## Error Response Structure

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error message",
  "instance": "/v1/mandates/{mandate_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "error description"
    }
  ]
}
```

## Related Operations

This endpoint is part of the Direct Debit section and works alongside:
- Create a mandate: POST `/mandates`
- Create a collection: POST `/payments/collections`
- Cancel a mandate: POST `/mandates/{mandate_id}/cancel`
- Cancel a Direct Debit: POST `/transactions/{transaction_id}/cancel`
- Process a retry: POST `/transactions/{transaction_id}/retry`
