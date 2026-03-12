# Create a Customer - API Documentation

## Overview

The Create a Customer endpoint allows you to establish a customer record that serves as the central reference point for all associated transactions, beneficiaries, and payment instruments. Everything can be linked back to a `customer_id`, making this the foundational step in the payment workflow.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/customers`
**Authentication:** Bearer Token (JWT)

## Request Parameters

### Header Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `Company-Id` | UUID | Yes | Unique identifier assigned by Acquired for your company |

### Request Body Schema

#### Customer Fields

| Parameter | Type | Length | Required | Description |
|-----------|------|--------|----------|-------------|
| `reference` | string | 0-50 | No | Your unique customer reference (alphanumeric, hyphens, periods, commas) |
| `first_name` | string | 0-22 | No | Customer's first name (alphabetic only) |
| `last_name` | string | 0-22 | No | Customer's last name (required for MCC 6012 merchants) |
| `dob` | date | 10 | No | Date of birth YYYY-MM-DD (required for MCC 6012) |
| `custom_data` | string | - | No | Base64-encoded custom data |

#### Billing Object

**Address:**

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `line_1` | string | 0-50 | Primary address line (recommended for EMV 3DS) |
| `line_2` | string | 0-50 | Secondary address line |
| `city` | string | 1-40 | Municipality |
| `state` | string | 0-3 | ISO 3166-2 state code (US only) |
| `postcode` | string | 1-40 | Postal code (recommended for MCC 6012) |
| `country_code` | string | 2 | ISO 3166 country code (recommended for EMV 3DS) |

**Contact:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `email` | string | Customer email address (recommended for EMV 3DS) |
| `phone.country_code` | string (1-3 digits) | International dialing prefix |
| `phone.number` | string (0-15 digits) | Telephone number (numeric only) |

#### Shipping Object

| Parameter | Type | Description |
|-----------|------|-------------|
| `address_match` | boolean | Set to true to auto-populate from billing address |
| `address` | object | Same structure as billing address |
| `email` | string | Shipping contact email |
| `phone` | object | Same structure as billing phone |

## Response Format

### Success Response (201 Created)

```json
{
  "customer_id": "550e8400-e29b-41d4-a716-446655440000"
}
```

The system returns a unique identifier (UUID) for the newly created customer record.

### Error Responses

**400 Bad Request:** Validation failures with parameter-level details.

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

**401 Unauthorized:** Authentication credentials invalid or missing.

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

**403 Forbidden:** User lacks permission to access the requested resource.

**409 Conflict:** A conflict occurred during processing (e.g., duplicate reference).

## Related Endpoints

- **List all customers:** `GET /v1/customers`
- **Retrieve a customer:** `GET /v1/customers/{customer_id}`
- **Update a customer:** `PUT /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
