# Initiate a Confirmation of Funds Check Against a Mandate

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates/{mandate_id}/confirm-funds`

## Description

This endpoint enables merchants to initiate a verification process that checks whether sufficient funds are available against an established mandate. This is part of the Variable Recurring Payments workflow.

## Authentication

**Type:** Bearer Token (JWT)
**Required Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier for the mandate |

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Request Body

The endpoint accepts JSON request payloads with mandate-related details for funds confirmation:

```json
{
  "amount": "number",
  "currency": "string"
}
```

## Response Schema

### Success Response (201 Created)

Returns confirmation details of the funds check initiation, including:
- Funds availability status
- Amount checked
- Mandate reference
- Timestamp

### Error Responses

| Status Code | Scenario |
|-------------|----------|
| 400 | Validation failures or invalid parameters |
| 401 | Authentication credentials missing or invalid |
| 404 | Specified mandate not found |

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error description",
  "instance": "/v1/open-banking/mandates/{mandate_id}/confirm-funds",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Context

This endpoint operates within the Variable Recurring Payments section, which includes operations for:
- List supported banks
- Create mandates
- List mandates
- Retrieve open banking mandates
- Initiate variable recurring payments
- List variable recurring payments
