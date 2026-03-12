# List All Cards - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/cards`

### Description

This allows you to request a list of all cards without having to specify a relating customer. However, you do have the ability to filter using the `customer_id` parameter.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Query Parameters

| Parameter | Type | Required | Default | Min | Max | Description |
|-----------|------|----------|---------|-----|-----|-------------|
| `offset` | integer | No | 0 | - | - | The record to start the response on |
| `limit` | integer | No | - | 1 | 100 | Maximum records returned per response |
| `filter` | string | No | - | - | - | Filter which parameters appear in response |
| `customer_id` | string (UUID) | No | - | - | - | Filter results to cards for a specific customer |

## Response Schema (200 OK)

### Meta Object

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
        "rel": "prev",
        "href": "/v1/cards?offset=0",
        "title": "Previous page"
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

### Card Data Object

Each card in the `data` array contains:

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | Unique card identifier assigned by Acquired.com |
| `customer_id` | UUID | Yes | Associated customer ID |
| `card.holder_name` | string | No | Cardholder name (2-45 chars) |
| `card.scheme` | enum | Yes | Card scheme: `visa`, `mastercard`, `amex` |
| `card.number` | string | Yes | Last 4 digits of the card number |
| `card.expiry_month` | integer | No | Expiry month (1-12) |
| `card.expiry_year` | integer | No | Expiry year (22-32) |
| `bin.number` | string | Yes | First 6 digits of card number |
| `bin.scheme` | enum | Yes | Card scheme |
| `bin.type` | enum | Yes | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `bin.product` | enum | Yes | Card product: `consumer`, `commercial` |
| `bin.card_level` | enum | Yes | Card tier (standard, gold, platinum, business, etc. - 40+ options) |
| `bin.issuer` | string | Yes | Bank name that issued the card |
| `bin.issuer_country` | string | Yes | Country the card was issued in |
| `bin.issuer_country_code` | string | Yes | ISO 3166 2-character country code |
| `bin.eea` | boolean | Yes | Whether the card was issued in the EEA |
| `bin.non_reloadable` | boolean | Yes | Whether the card is a non-refundable prepaid card |
| `source` | enum | Yes | How the card was created: `card`, `apple_pay`, `google_pay` |
| `created` | date-time | Yes | Creation timestamp |
| `last_updated` | date-time | Yes | Last modification timestamp |
| `is_active` | boolean | No | Whether the card is active (default: true) |

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

## Key Features

- Supports pagination via offset/limit
- Allows filtering by customer ID via query parameter
- Returns comprehensive card details including BIN information
- Tracks card activation status
- Includes creation and modification timestamps
- Card numbers are masked, showing only last 4 digits

## Related Endpoints

- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
- **Get card details:** `GET /v1/cards/{card_id}`
- **Update card details:** `PUT /v1/cards/{card_id}`
