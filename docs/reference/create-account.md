# Create an Account - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/accounts`

This endpoint enables account creation for payment processing. Submit account information to receive an `account_id` in the response.

## Authentication

- **Type:** Bearer Token
- **Alternative:** JWT

## Request Structure

The endpoint accepts account details in the request body. Based on the OpenAPI specification provided, the request should include account-related information.

## Headers

- **Company-Id** (optional): UUID identifying your company
- **Mid** (optional): UUID connecting to acquiring bank

## Response

**Success Status:** 201 Created

The successful response returns an account identifier that can be used for subsequent payment operations.

## Error Responses

The API returns structured error responses including:

- **400 Bad Request:** "Validation failed" - includes `invalid_parameters` array detailing which fields failed
- **401 Unauthorized:** "Authentication failed, verify credentials and retry"
- **404 Not Found:** Resource unavailable

Each error response contains:
- `status`: Current request status
- `error_type`: Classification of error
- `title`: Human-readable explanation
- `instance`: Location where error occurred

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Related Operations

This endpoint is part of the Faster Payments section, alongside:
- Create a payee
- Process a payout
- List all accounts
- Retrieve account details
- Internal transfer
- List all payees
