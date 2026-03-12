# Create a Mandate (Open Banking / VRP)

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates`

This endpoint creates a mandate and returns the details needed to direct the customer to their bank for authorization.

## Authentication

**Type:** Bearer JWT Token

Required header: Authorization with Bearer token obtained from the login endpoint.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

### Body Schema

The request body accepts a JSON object with the following structure:

#### Core Transaction Details
- **mandate_reference** (string): Unique reference for the mandate
- **customer_id** (string, UUID): Unique identifier for the customer
- **frequency** (string): Payment frequency (e.g., monthly, weekly, one-time)
- **start_date** (string, date): When the mandate becomes active
- **end_date** (string, date): Optional end date for the mandate

#### Customer Information
- **first_name** (string): Customer's given name
- **last_name** (string): Customer's family name
- **email** (string): Customer email address
- **reference** (string): Your internal customer reference

#### Banking Details
- **iban** (string): International Bank Account Number
- **account_holder_name** (string): Name on the bank account
- **country_code** (string): ISO 3166 2-character code

### Example Request Body

```json
{
  "mandate_reference": "MANDATE-001",
  "customer_id": "uuid",
  "frequency": "monthly",
  "start_date": "2026-04-01",
  "end_date": "2027-04-01",
  "first_name": "John",
  "last_name": "Doe",
  "email": "john.doe@example.com",
  "reference": "CUST-001",
  "iban": "GB82WEST12345698765432",
  "account_holder_name": "John Doe",
  "country_code": "GB"
}
```

## Response Format

### Success Response (201 Created)

The successful response includes:
- **mandate_id** (string, UUID): Unique mandate identifier assigned by system
- **status** (string): Current mandate status
- **redirect_url** (string): URL to send customer for bank authorization
- **created** (string, date-time): Timestamp of creation
- **expires_at** (string, date-time): Mandate expiration time

```json
{
  "mandate_id": "uuid",
  "status": "pending_authorization",
  "redirect_url": "https://bank.example.com/authorize/...",
  "created": "2026-04-01T12:00:00Z",
  "expires_at": "2026-04-01T12:30:00Z"
}
```

### Error Responses

**400 Bad Request** - Validation failures including:
- Invalid parameter format
- Missing required fields
- Out-of-range values

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/mandates",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Authentication failures

**404 Not Found** - Resource doesn't exist

## Key Notes

The mandate creation process initiates open banking authorization. Customers must complete verification through their bank portal using the provided redirect URL. The system supports multiple payment frequencies and includes optional expiration dates for time-limited arrangements.
