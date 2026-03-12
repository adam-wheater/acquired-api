# Create an access_token (Login) - API Documentation

## Overview

The authentication endpoint generates bearer tokens required for API access. Access to the API is by Bearer Tokens, this request generates an `access_token` that can be used in the authorization header of each request.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/login`

### Request Body

The login endpoint requires application credentials in JSON format:

| Parameter | Type | Length | Required | Description |
|-----------|------|--------|----------|-------------|
| `app_id` | string | 8 chars | Yes | Unique ID assigned by Acquired.com to your application |
| `app_key` | string | 32 chars | Yes | Secret key used as part of the authentication process |

**Example Request:**
```json
{
  "app_id": "72693514",
  "app_key": "de0eef006b4c30440aeca8ca5ac58789"
}
```

### Successful Response (200 OK)

The endpoint returns authentication details:

| Field | Type | Description |
|-------|------|-------------|
| `token_type` | string | Details the token type returned (typically "Bearer") |
| `expires_in` | integer | Time in seconds until the `access_token` expires (example: 3600) |
| `access_token` | string | Unique token that can be used to authenticate and log into the API |

**Example Response:**
```json
{
  "token_type": "Bearer",
  "expires_in": 3600,
  "access_token": "eyJ0eXAiOiJKV1QiLC....."
}
```

### Error Responses

**400 Bad Request:** Invalid credentials or missing required parameters. Response includes validation details with parameter-specific error reasons.

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

**401 Unauthorized:** Authentication failed.

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
- Must be included in authorization headers for authenticated endpoints throughout the platform
- Token format: `Authorization: Bearer {access_token}`
