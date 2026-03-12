# Direct Debit - API Documentation

## Overview

**Section:** Direct Debit Payment Methods

The Direct Debit section of the Acquired.com API provides endpoints for managing Direct Debit mandates, collections, cancellations, and retries.

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates`

The first step in the Direct Debit process involves creating a mandate. This must be established against a customer's bank account before Direct Debit collections can proceed.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required Header:** Authorization with access token

## Request Parameters

The request body should contain:

- **customer_id** (string, UUID): Unique identifier for the customer
- **bank_account** (object): Bank account details including:
  - Account holder name
  - Account number
  - Sort code (for UK accounts)
  - IBAN (for international accounts)
- **mandate_reference** (string): Your unique reference for the mandate
- **scheme** (string): Payment scheme (typically "sepa" or "bacs")

## Response Structure

**Success Response (201 Created):**

```json
{
  "mandate_id": "uuid",
  "customer_id": "uuid",
  "status": "pending_approval",
  "mandate_reference": "string",
  "created": "date-time"
}
```

## HTTP Status Codes

- **201 Created:** Mandate successfully created
- **400 Bad Request:** Invalid parameters or validation failure
- **401 Unauthorized:** Authentication failed
- **404 Not Found:** Customer or resource not found

## Error Response Format

Failed requests return error details with:
- `status`: "error"
- `error_type`: Type of error (validation, unauthorized, etc.)
- `title`: Human-readable error message
- `instance`: Where the error occurred
- `invalid_parameters`: Array of validation issues

## Related Endpoints

- **Create a collection:** POST `/collections`
- **Cancel a mandate:** POST `/mandates/{mandate_id}/cancellations`
- **Cancel a Direct Debit:** POST `/transactions/{transaction_id}/cancel`
- **Process a retry:** POST `/transactions/{transaction_id}/retry`
- **Retrieve mandate:** GET `/mandates/{mandate_id}`

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`
