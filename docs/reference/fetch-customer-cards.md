# Get Customer Cards - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}/cards`
**Description:** Retrieve a list of all cards belonging to a customer. All cards are associated to the `customer_id`.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `customer_id` | UUID | Yes | Unique ID assigned by Acquired.com for the customer |

## Query Parameters

| Parameter | Type | Required | Default | Min | Max | Description |
|-----------|------|----------|---------|-----|-----|-------------|
| `offset` | integer | No | 0 | - | - | The record to start the response on |
| `limit` | integer | No | - | 1 | 100 | A limit on the scope of values returned in the response |
| `filter` | string | No | - | - | - | Filter the parameters that you want to return within the response |

## Success Response (200 OK)

The response follows a paginated structure with metadata and card data array.

### Pagination Metadata

```json
{
  "meta": {
    "count": 10,
    "offset": 0,
    "limit": 25,
    "total": 10,
    "links": [
      {
        "rel": "self",
        "href": "/v1/customers/{customer_id}/cards?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/customers/{customer_id}/cards?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/customers/{customer_id}/cards?offset=0",
        "title": "Last page"
      }
    ]
  }
}
```

### Card Object Schema

Each card in the `data` array contains:

| Field | Type | Read-Only | Description |
|-------|------|-----------|-------------|
| `card_id` | UUID | Yes | The unique ID assigned by Acquired.com to the card when it is created |
| `customer_id` | UUID | Yes | The unique ID assigned by Acquired.com to the customer when it is created |
| `card.holder_name` | string | No | The name of the cardholder (2-45 chars, letters/spaces) |
| `card.scheme` | enum | Yes | Card scheme: `visa`, `mastercard`, `amex` |
| `card.number` | string | Yes | The last 4 digits of the card number |
| `card.expiry_month` | integer | No | The month the card expires in (1-12) |
| `card.expiry_year` | integer | No | The year the card expires in (22-32) |
| `bin.number` | string | Yes | The first 6 digits of the card number |
| `bin.scheme` | string | Yes | Card scheme designation |
| `bin.type` | enum | Yes | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `bin.product` | enum | Yes | Card product: `consumer`, `commercial` |
| `bin.card_level` | enum | Yes | Card tier (standard, gold, platinum, etc.) |
| `bin.issuer` | string | Yes | The name of the bank that issued the card |
| `bin.issuer_country` | string | Yes | The name of the country the card was issued in |
| `bin.issuer_country_code` | string | Yes | The ISO 3166 2-character country code the card was issued in |
| `bin.eea` | boolean | Yes | If the card was issued in the EEA |
| `bin.non_reloadable` | boolean | Yes | If the card is a non-refundable prepaid card |
| `source` | enum | Yes | How the card was created: `card`, `apple_pay`, `google_pay` |
| `created` | datetime | Yes | The date and time that the card record was created |
| `last_updated` | datetime | Yes | The date and time the card record was last updated |
| `is_active` | boolean | No | Indicates whether a card is active or not (default: true) |

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers/{customer_id}/cards",
  "invalid_parameters": [
    {
      "parameter": "param_name",
      "reason": "error_detail"
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

## Related Endpoints

- **List all cards:** `GET /v1/cards`
- **Get card details:** `GET /v1/cards/{card_id}`
- **Update card details:** `PUT /v1/cards/{card_id}`
