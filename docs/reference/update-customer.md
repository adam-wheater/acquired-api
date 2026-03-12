# Update a Customer - API Documentation

## Endpoint Overview

**Method:** PUT
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}`

The endpoint permits modification of customer details via the `customer_id` parameter. Any parameters not provided will be left unchanged.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `customer_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the customer |

## Request Body Schema

All fields are optional for updates. Only include fields you wish to modify.

### Basic Information

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `reference` | string | 0-50 chars | Your internal customer identifier (alphanumeric, hyphens, periods, commas) |
| `first_name` | string | 0-22 chars | Customer's given name (alphabetic only) |
| `last_name` | string | 0-22 chars | Customer's surname (required for MCC 6012) |
| `dob` | string | date format YYYY-MM-DD | Date of birth (required for MCC 6012) |
| `custom_data` | string | - | Base64-encoded custom data |

### Billing Address

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `billing.address.line_1` | string | 0-50 chars | Primary address line |
| `billing.address.line_2` | string | 0-50 chars | Secondary address line |
| `billing.address.city` | string | 1-40 chars | Municipality |
| `billing.address.state` | string | 0-3 chars | ISO 3166-2 state code (US only) |
| `billing.address.postcode` | string | 1-40 chars | Postal code |
| `billing.address.country_code` | string | 2 chars | ISO 3166 country code |
| `billing.email` | string | - | Email address |
| `billing.phone.country_code` | string | 1-3 digits | International dialing prefix |
| `billing.phone.number` | string | 0-15 digits | Phone number |

### Shipping Address

Same structure as billing, with additional field:

| Parameter | Type | Description |
|-----------|------|-------------|
| `shipping.address_match` | boolean | Set to true to mirror billing address details |

All other shipping fields follow the same structure as billing (`shipping.address.*`, `shipping.email`, `shipping.phone.*`).

## Response Codes

### 200 OK - Success

```json
{
  "status": "success"
}
```

### 400 Bad Request - Validation failure

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers/{customer_id}",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of validation failure"
    }
  ]
}
```

### 401 Unauthorized - Authentication credentials invalid or missing

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/customers/{customer_id}"
}
```

### 409 Conflict - Request conflicts with existing data or state

## Notes

- Any parameters not provided will be left unchanged
- Field validation enforces pattern matching (alphanumeric constraints, special character restrictions)
- `company_id` and timestamp fields (`created`, `last_updated`) are read-only and cannot be modified
- Phone numbers accept only numeric characters

## Related Endpoints

- **Create a customer:** `POST /v1/customers`
- **List all customers:** `GET /v1/customers`
- **Retrieve a customer:** `GET /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
