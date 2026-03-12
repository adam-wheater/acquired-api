# Update Card Details - API Documentation

## Overview

The Update Card Details endpoint allows you to modify specific card information without re-entering all details. You can update the card number, expiry month/year, cardholder name, or use the `is_active` parameter to disable a card.

## Endpoint Details

**Method:** PUT
**URL:** `https://test-api.acquired.com/v1/cards/{card_id}`

### Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `card_id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for the card |

## Request Body

The request accepts a JSON object with the following optional fields (partial updates supported):

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| `holder_name` | string | 2-45 characters, alphabetic + spaces | The name of the cardholder |
| `number` | string | Valid card number format | The card number |
| `expiry_month` | integer | 1-12 | The month the card expires in |
| `expiry_year` | integer | 22-32 | The year the card expires in |
| `is_active` | boolean | Default: false | Indicates whether a card is active or not. Set to `false` if you want to disable the card |

### Example Request

```json
{
  "holder_name": "E Johnson",
  "number": "4000011180138710",
  "expiry_month": 10,
  "expiry_year": 26,
  "is_active": false
}
```

## Response Formats

### 200 OK - Success

```json
{
  "status": "success"
}
```

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation",
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
  "title": "Authentication with the API failed, please check your details and try again",
  "instance": "/v1/cards/{card_id}"
}
```

## Key Features

- **Partial Updates:** Only include fields you wish to modify; unspecified fields remain unchanged
- **Card Deactivation:** Set `is_active` to `false` to disable an active card
- **Secure Updates:** Changes require valid Bearer Token authentication
- **Validation:** All submitted parameters are validated before processing

## Notes

- The `number` field accepts the full card number for updates (unlike GET responses which only show last 4 digits)
- Setting `is_active` to `false` effectively disables the card for future transactions
- All validation rules (character limits, patterns) are enforced on update requests

## Language Support

Available SDKs: Shell, Node, Ruby, PHP, Python

## Related Endpoints

- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
- **List all cards:** `GET /v1/cards`
- **Get card details:** `GET /v1/cards/{card_id}`
