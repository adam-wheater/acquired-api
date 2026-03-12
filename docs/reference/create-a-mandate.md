# Create a Mandate (Direct Debit)

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates`

The first step in the Direct Debit process is to create a mandate. A mandate must be logged against a customer's bank account before Direct Debit collections can be made.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization with Bearer token from login endpoint

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Key Information

### Purpose
Setting up a mandate enables merchants to collect payments via Direct Debit from a customer's bank account. This must be established before any collections can occur.

### Request Structure
The API expects authentication credentials and mandate details to be submitted. The system validates all parameters before processing.

### Request Parameters

- **customer_id** (string, UUID): Unique identifier for the customer
- **bank_account** (object): Bank account details including:
  - Account holder name
  - Account number
  - Sort code (for UK accounts)
  - IBAN (for international accounts)
- **mandate_reference** (string): Your unique reference for the mandate
- **scheme** (string): Payment scheme (typically "sepa" or "bacs")

### Response

Upon successful creation, the endpoint returns a mandate confirmation with relevant identifiers and status information.

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

## Related Operations

The Direct Debit section includes complementary endpoints for:
- Creating collections against established mandates
- Canceling mandates when needed
- Processing retry attempts
- Retrieving mandate details

## Integration Notes

This endpoint requires a valid company context and appropriate access credentials. The mandate creation is a prerequisite step in the broader Direct Debit workflow, allowing subsequent collection operations against customer bank accounts.
