# Cancel a Mandate

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates/{mandate_id}/cancel`

## Description

This endpoint enables immediate cancellation of an active mandate. The process will reject requests if the mandate isn't in an active status. Once cancelled, a mandate cannot revert to active -- you must create a fresh mandate instead. Custom data submitted with this request associates with the resulting cancellation event.

## Authentication

**Type:** Bearer Token (JWT)

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | The unique identifier for the mandate to cancel |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Authorization` | string | Bearer token for authentication |
| `Company-Id` | string (UUID) | Your assigned company identifier |

### Request Body

```json
{
  "custom_data": "string (base64 encoded)"
}
```

## Response Schemas

### Success Response (200 OK)

The API returns confirmation of the cancellation action.

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Request parameters failed validation",
  "instance": "/v1/mandates/{mandate_id}/cancel",
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
  "instance": "/v1/mandates/{mandate_id}/cancel"
}
```

**404 Not Found:**

Mandate not found or doesn't exist.

## Important Notes

- The mandate must maintain active status for cancellation to succeed
- Cancelled mandates are permanent -- reactivation is impossible
- Any custom data included becomes linked to the cancellation event
- You must create a new mandate if you need to resume collections

## Related Endpoints

- Create a mandate: POST `/mandates`
- Create a collection: POST `/payments/collections`
- Retrieve mandate: GET `/mandates/{mandate_id}`
