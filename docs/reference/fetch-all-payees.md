# List All Payees - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/payees`

**Description:** "Returns a paginated list of Payee records created against the authenticated company."

## Authentication

- **Type:** Bearer Token (JWT)
- **Credentials:** Required

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `offset` | integer | No | "The record to start the response on." (default: 0) |
| `limit` | integer | No | "A limit on the scope of values returned in the response." (min: 1, max: 100) |
| `filter` | string | No | "Filter the parameters that you want to return within the response." |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Company-Id` | UUID | "Unique ID assigned by Acquired.com for your company." |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays:

**Meta Object:**
- `count` (integer): "The number of records returned as part of the response."
- `offset` (integer): "The starting record number."
- `limit` (integer): "The maximum number of records that can be returned."
- `total` (integer): "The total number of records contained in the query response."
- `links` (array): Navigation links with rel types: self, first, last, prev, next

**Data Array:** Contains payee objects with pagination support (default: 25 most recent)

## Key Features

- **Scope:** "Payees are scoped to the Company (not MID)."
- **Default Behavior:** "By default, the 25 most recently created Payees are returned."
- **Sorting:** Results returned by creation date (most recent first)

## Error Responses

### 400 Bad Request
Returns validation error with invalid_parameters array containing parameter names and reasons.

### 401 Unauthorized
Authentication failed - check API credentials.

## Related Endpoints

- Create a payee: POST `/payees`
- Process a payout: POST `/payouts`
- Create an account: POST `/accounts`
- List all accounts: GET `/accounts`
- Internal transfer: POST `/internal-transfer`
