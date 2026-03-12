# Create an access_token - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/login`

## Description

Access to the API is by Bearer Tokens, this request generates an `access_token` that authenticates subsequent API calls. Client credentials are passed through the login endpoint to obtain the token.

## Request Body

The endpoint accepts `application/json` with the following required fields:

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `app_id` | string | 8 characters exactly | Unique ID assigned by Acquired.com to your application |
| `app_key` | string | 32 characters exactly | Secret key used as part of the authentication process |

**Example Request:**
```json
{
  "app_id": "72693514",
  "app_key": "de0eef006b4c30440aeca8ca5ac58789"
}
```

## Response Schemas

### 200 OK Response

Success returns token authentication details:

| Field | Type | Description |
|-------|------|-------------|
| `token_type` | string | Details the token type returned (e.g., "Bearer") |
| `expires_in` | integer | Time in seconds until the `access_token` expires |
| `access_token` | string | Unique token that can be used to authenticate and log into the API |

**Example:**
```json
{
  "token_type": "Bearer",
  "expires_in": 3600,
  "access_token": "eyJ0eXAiOiJKV1QiLC....."
}
```

### 400 Bad Request

Validation failures return error details including `invalid_parameters` array with parameter names and reasons.

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/login",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

### 401 Unauthorized

Authentication with the API failed, please check your details and try again.

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Usage Notes

- Generated tokens function as bearer tokens for subsequent API requests
- Must be included in the Authorization header for all authenticated endpoints
- Token format: `Authorization: Bearer {access_token}`
- Tokens expire after the duration specified in `expires_in` (typically 3600 seconds / 1 hour)
