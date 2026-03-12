# Retrieve Account Details - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/accounts/{mid}`

This endpoint retrieves account details using a unique merchant identifier (`mid`), which can be located in the Hub under Settings > Developers tab.

## Authentication

**Type:** Bearer Token (JWT)

Requests require an `access_token` obtained through the login endpoint.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `mid` | string | Unique identifier assigned by Acquired.com connecting to a specific acquiring bank |

## Request Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| Authorization | string | Yes | Bearer token for API authentication |

## Response Format

**Success Response (200 OK):**

Returns account details as JSON object containing merchant account information associated with the provided `mid`.

## Error Responses

**400 Bad Request**
- Status: "error"
- Error Type: "validation"
- Returns invalid parameters array with parameter names and reasons

**401 Unauthorized**
- Status: "error"
- Error Type: "unauthorized"
- Message: "Authentication with the API failed, please check your details and try again"

**404 Not Found**
- Status: "error"
- Error Type: Returned when account/mid not found

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Related Endpoints

This endpoint is part of the Faster Payments section, alongside:
- List all accounts (GET)
- Create an account (POST)
- Internal transfer (POST)
- List all payees (GET)
