# Create a Payee - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}/payees`

The endpoint enables creation of new payees stored against customer profiles, returning a unique `payee_id` for subsequent payout operations.

## Authentication

- **Type:** Bearer Token or JWT
- **Scheme:** Bearer authentication required in request headers

## Request Parameters

### Path Parameters
- `customer_id` (required): UUID format identifier assigned by Acquired.com to the customer

### Request Body Schema

The request accepts a JSON payload with payee information. The API specification indicates support for payee creation but the specific field requirements are contained within the OpenAPI schema definition provided in the document.

## Response Format

### Success Response (201 Created)

The successful response includes:
- HTTP Status: 201
- Response headers including `Company-Id` and `Mid` (both UUID format)
- JSON body containing the created payee details including a unique `payee_id`

**Key field:**
- `payee_id`: "a unique identifier assigned by Acquired.com to the payee when created" - which can then be referenced in payout operations.

### Error Responses

**400 Bad Request:**
- `status`: "error"
- `error_type`: "validation"
- `title`: Validation failure message
- `invalid_parameters`: Array of parameter-specific errors

**401 Unauthorized:**
- `status`: "error"
- `error_type`: "unauthorized"
- `title`: Authentication failure notification
- `instance`: Error endpoint reference

**403 Forbidden:**
- Permission/access restrictions

**404 Not Found:**
- Returns when specified customer_id doesn't exist

**409 Conflict:**
- State conflicts during creation

## Headers

- `Company-Id`: UUID assigned by Acquired.com (recommended in header)
- `Authorization`: Bearer token required

## Related Operations

This endpoint is part of the Faster Payments suite, which includes:
- Process a payout (uses the `payee_id`)
- Create account
- List all payees
- Internal transfers

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python
