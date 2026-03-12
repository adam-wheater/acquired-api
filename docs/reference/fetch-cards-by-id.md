# Get Card Details - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/cards/{card_id}`

The endpoint retrieves comprehensive details about a specific card by including the corresponding `card_id` in the URL path.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `card_id` | UUID | Yes | Unique ID assigned by Acquired.com for the card |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `filter` | string | No | Filter the parameters that you want to return within the response |

## Response Schema (200 OK)

The successful response returns a card details object with the following structure:

### Root Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | The unique ID assigned by Acquired.com to the card when it is created |
| `customer_id` | UUID | Yes | The unique ID assigned by Acquired.com to the customer when it is created |
| `card` | object | - | Card information object |
| `bin` | object | Yes | Bank Identification Number details |
| `source` | string | Yes | Identifies how the card was created: `card`, `apple_pay`, `google_pay` |
| `created` | ISO 8601 | Yes | The date and time that the card record was created |
| `last_updated` | ISO 8601 | Yes | The date and time the card record was last updated |
| `is_active` | boolean | No | Indicates whether a card is active or not (default: true) |

### Card Object Properties

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, alphabetic + spaces | The name of the cardholder |
| `scheme` | enum | - | Card scheme: `visa`, `mastercard`, `amex` |
| `number` | string | - | The last 4 digits of the card number |
| `expiry_month` | integer | 1-12 | The month the card expires in |
| `expiry_year` | integer | 22-32 | The year the card expires in |

### BIN Object Properties (Read-Only)

| Property | Type | Description |
|----------|------|-------------|
| `number` | string | The first 6 digits of the card number |
| `scheme` | enum | Card scheme: `visa`, `mastercard`, `amex` |
| `type` | enum | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `product` | enum | Card product category: `consumer`, `commercial` |
| `card_level` | enum | The level of the card: `standard`, `gold`, `platinum`, `business`, etc. |
| `issuer` | string | The name of the bank that issued the card |
| `issuer_country` | string | The name of the country the card was issued in |
| `issuer_country_code` | string | The ISO 3166 2-character country code the card was issued in |
| `eea` | boolean | If the card was issued in the EEA |
| `non_reloadable` | boolean | If the card is a non-refundable prepaid card |

## Example Response

```json
{
  "card_id": "550e8400-e29b-41d4-a716-446655440000",
  "customer_id": "660e8400-e29b-41d4-a716-446655440000",
  "card": {
    "holder_name": "E Johnson",
    "scheme": "visa",
    "number": "8710",
    "expiry_month": 10,
    "expiry_year": 29
  },
  "bin": {
    "number": "424242",
    "scheme": "visa",
    "type": "debit",
    "product": "consumer",
    "card_level": "standard",
    "issuer": "acquired.com",
    "issuer_country": "united kingdom",
    "issuer_country_code": "gb",
    "eea": true,
    "non_reloadable": false
  },
  "source": "card",
  "created": "2025-06-28T06:31:27.091Z",
  "last_updated": "2025-06-28T06:31:27.091Z",
  "is_active": true
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/cards/{card_id}",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of validation failure"
    }
  ]
}
```

### 401 Unauthorized

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/cards/{card_id}"
}
```

## Notes

- The response masks sensitive card information, displaying only the last 4 digits
- BIN details are read-only and provided for informational purposes
- The `filter` query parameter allows customization of returned fields to optimize response payloads
- All date fields use ISO 8601 format

## Related Endpoints

- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
- **List all cards:** `GET /v1/cards`
- **Update card details:** `PUT /v1/cards/{card_id}`
