# Retrieve an Open Banking Mandate

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates/{mandate_id}`

This endpoint retrieves an open banking mandate using a unique identifier. The `mandate_id` can be obtained through the `mandate_active` webhook or after a customer authenticates via Hosted Checkout.

## Authentication

**Type:** Bearer Token (JWT)

Requests require valid bearer token authentication passed in the authorization header.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier for the open banking mandate |

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Response

### Success Response (200 OK)

The successful response returns the complete mandate details in JSON format. The response structure includes:

- Mandate identification and status information
- Customer authentication details
- Bank account information
- Creation and update timestamps
- Mandate-specific configuration (frequency, start/end dates)

### Error Responses

| Status | Error Type | Description |
|--------|-----------|-------------|
| 400 | Validation Error | Request parameters failed validation |
| 401 | Unauthorized | Authentication credentials invalid or missing |
| 404 | Not Found | Mandate identifier does not exist |

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error explanation",
  "instance": "/v1/open-banking/mandates/{mandate_id}"
}
```

## Usage Context

This endpoint is part of the Variable Recurring Payments section within the Acquired API. It functions as a retrieval mechanism for mandates created through open banking connections, enabling merchants to verify mandate status and details without reinitiating the authentication flow.

## Related Endpoints

- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- Initiate a VRP: POST `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`
