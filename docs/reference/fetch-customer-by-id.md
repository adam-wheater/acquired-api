# Retrieve a Customer - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}`

**Description:** Retrieves a customer using the unique `customer_id`. This identifier can be found in the response after creating a new customer.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `customer_id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for the customer |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `filter` | string | No | Filter the parameters that you want to return within the response |

## Response Schema (200 OK)

The successful response returns a customer object with the following properties:

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `company_id` | string (UUID) | Yes | Unique ID assigned by Acquired.com to the company |
| `reference` | string | No | Unique reference assigned by you (max 50 chars) |
| `first_name` | string | No | Customer's first name (max 22 chars, alphabetic only) |
| `last_name` | string | No | Customer's last name (max 22 chars, alphabetic only). Required for MCC 6012 merchants. |
| `dob` | string (date) | No | Customer's date of birth. Required for MCC 6012 merchants. Format: YYYY-MM-DD |
| `custom_data` | string | No | Base64 encoded custom data |
| `billing` | object | No | Billing address and contact information |
| `shipping` | object | No | Shipping address and contact information |
| `created` | string (date-time) | Yes | Creation timestamp |
| `last_updated` | string (date-time) | Yes | Last update timestamp |

### Billing/Shipping Object Structure

| Property | Type | Description |
|----------|------|-------------|
| `address` | object | Address details |
| `email` | string | Email address (email format) |
| `phone` | object | Phone contact details |

### Address Object

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `line_1` | string | Max 50 chars | Primary address line. Recommended when processing EMV 3DS transactions. |
| `line_2` | string | Max 50 chars | Secondary address line (optional) |
| `city` | string | Max 40 chars | Municipality (required) |
| `state` | string | Max 3 chars | ISO 3166-2 state code (US orders only) |
| `postcode` | string | Max 40 chars | Postal code (required). Recommended for MCC 6012 merchants. |
| `country_code` | string | 2 chars | ISO 3166 2-character country code. Recommended when processing EMV 3DS transactions. |

### Phone Object

| Property | Type | Description |
|----------|------|-------------|
| `country_code` | string | International dialing code (1-3 digits) |
| `number` | string | Phone number (max 15 digits) |

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation",
  "instance": "/v1/customers/{customer_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details about the validation error"
    }
  ]
}
```

### 401 Unauthorized

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again",
  "instance": "/v1/customers/{customer_id}"
}
```

## Related Endpoints

- **Create a customer:** `POST /v1/customers`
- **List all customers:** `GET /v1/customers`
- **Update a customer:** `PUT /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`

## Implementation Notes

- The endpoint returns read-only fields for `company_id`, `created`, and `last_updated`
- Billing address `line_1` and `country_code` are recommended when processing EMV 3DS transactions
- Postcode and last_name are recommended for MCC 6012 merchants
- All date fields follow ISO 8601 format
