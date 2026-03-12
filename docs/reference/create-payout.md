# Process a Payout - API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/pay-out`

This endpoint enables execution of payments to customers from an Acquired-managed account.

## Authentication

- **Type**: Bearer Token (JWT)
- **Header**: Authorization with access token from login endpoint

## Request Parameters

### Headers
- `Company-Id` (uuid): Unique identifier for your company
- `Mid` (uuid, optional): Acquiring bank connection identifier

### Request Body Schema

The request accepts a JSON payload with the following structure:

```json
{
  "payee": {
    "payee_id": "uuid",
    "account_number": "string",
    "sort_code": "string"
  },
  "transaction": {
    "order_id": "string (2-50 chars)",
    "amount": "number (float)",
    "currency": "enum (gbp, eur, usd, etc.)",
    "custom_data": "string (base64 encoded)"
  },
  "reference": "string (max 18 chars)"
}
```

> **Note:** You can provide either `payee_id` OR `account_number` + `sort_code` in the payee object.

### Core Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `order_id` | string | Unique reference for payout request |
| `amount` | number | Total amount to transfer |
| `currency` | enum | ISO 4217 currency code (lowercase) |
| `payee_id` | uuid | Existing payee identifier |
| `account_number` | string | Beneficiary bank account |
| `sort_code` | string | UK sort code for routing |
| `reference` | string | Statement reference (max 18 chars) |

## Response Codes

### Success Response (201 Created)

```json
{
  "transaction_id": "string (uuid)",
  "status": "pending|processed|failed",
  "payout": {
    "payee_id": "uuid",
    "amount": "number",
    "currency": "string"
  }
}
```

### Error Responses

**400 Bad Request**: "Your request parameters did not pass our validation."
- Returns `invalid_parameters` array with detailed field-level errors

**401 Unauthorized**: "Authentication with the API failed, please check your details and try again."

**404 Not Found**: Payee or resource not located

## Supported Currencies

AED, AUD, CAD, CHF, CNY, DKK, EUR, GBP, HKD, JPY, MXN, SEK, USD, ZAR

## Related Endpoints

- **Create a Payee** (POST `/payees`): Register beneficiary before payout
- **Retrieve Transaction** (GET `/transactions/{id}`): Check payout status
- **List Payees** (GET `/payees`): View existing beneficiaries

## Notes

- Payee must exist before processing payout (or provide account_number + sort_code directly)
- Reference field appears on beneficiary's bank statement where supported
