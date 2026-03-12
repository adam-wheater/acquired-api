# Update a session_id - API Documentation

## Endpoint Overview

**HTTP Method:** PUT
**URL:** `https://test-api.acquired.com/v1/payment-sessions/{session_id}`

## Description

"This request allows you to update any details of the previously created `session_id.`" When payment completion occurs within Components, the authorization will use the most recent parameters configured against the `session_id`.

## Authentication

**Type:** Bearer Token (JWT)

## Base URL

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Request Parameters

The endpoint accepts a PUT request to modify an existing payment session. You submit only the parameters requiring updates; any unmodified parameters remain unchanged.

### Path Parameter

| Parameter | Type | Description |
|-----------|------|-------------|
| `session_id` | String (UUID) | The unique identifier of the payment session to update |

## Language/SDK Support

Documentation indicates support for:
- Shell
- Node
- Ruby
- PHP
- Python

## Related Endpoints

Within the Components section, you can also:
- **Generate a session_id** (POST `/payment-sessions`)

## Navigation Context

This endpoint belongs to the **Components** category under the Acquired API reference, alongside session generation functionality.

---

**Note:** Complete request/response schema details and code examples are referenced in the original API specification. The request body accepts the same structure as the Generate session_id endpoint (transaction, payment, customer, and tds objects) - only the fields being updated need to be included in the request.
