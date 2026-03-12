# Cards - API Documentation

## Overview

The Cards endpoints allow you to manage payment cards associated with customers. Cards can be retrieved individually, as a full list, filtered by customer, or updated. All cards are associated to a `customer_id`.

## Endpoints

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/v1/customers/{customer_id}/cards` | Get customer cards |
| GET | `/v1/cards` | List all cards |
| GET | `/v1/cards/{card_id}` | Get card details |
| PUT | `/v1/cards/{card_id}` | Update card details |

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** `Authorization: Bearer {access_token}`

## Card Object Schema

### Root Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | The unique ID assigned by Acquired.com to the card when it is created |
| `customer_id` | UUID | Yes | The unique ID assigned by Acquired.com to the customer when it is created |
| `card` | object | No | Card information object |
| `bin` | object | Yes | Bank Identification Number details |
| `source` | string | Yes | Identifies how the card was created: `card`, `apple_pay`, or `google_pay` |
| `created` | date-time | Yes | The date and time that the card record was created |
| `last_updated` | date-time | Yes | The date and time the card record was last updated |
| `is_active` | boolean | No | Indicates whether a card is active or not (default: true) |

### Card Object Properties

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, letters/spaces only | The name of the cardholder |
| `scheme` | enum | - | Card scheme: `visa`, `mastercard`, `amex` |
| `number` | string | - | The last 4 digits of the card number (read responses) / full card number (update requests) |
| `expiry_month` | integer | 1-12 | The month the card expires in |
| `expiry_year` | integer | 22-32 | The year the card expires in |

### BIN Object Properties (Read-Only)

| Property | Type | Description |
|----------|------|-------------|
| `number` | string | The first 6 digits of the card number |
| `scheme` | enum | Card scheme: `visa`, `mastercard`, `amex` |
| `type` | enum | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `product` | enum | Card product: `consumer`, `commercial` |
| `card_level` | enum | Card tier (standard, gold, platinum, business, etc.) |
| `issuer` | string | The name of the bank that issued the card |
| `issuer_country` | string | The name of the country the card was issued in |
| `issuer_country_code` | string | The ISO 3166 2-character country code the card was issued in |
| `eea` | boolean | If the card was issued in the EEA |
| `non_reloadable` | boolean | If the card is a non-refundable prepaid card |

## Pagination

List endpoints return paginated results with a `meta` object:

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      {
        "rel": "self",
        "href": "/v1/cards?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/cards?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/cards?offset=125",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/cards?offset=25",
        "title": "Next page"
      }
    ]
  }
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/cards",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
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
  "instance": "/v1/login"
}
```

## Key Notes

- Card numbers are masked in responses, showing only the last 4 digits
- BIN details are read-only and provided for informational purposes
- The `filter` query parameter allows customization of returned fields to optimize response payloads
- Cards track activation status via `is_active` field
- All date fields use ISO 8601 format
