# Handling Errors - Documentation

## Overview

When communicating with the Acquired.com API, all responses arrive in JSON format accompanied by HTTP status codes. Failed requests can be identified through the `status`, `error_type`, `title`, and related fields to diagnose and resolve issues.

## Error Response Attributes

| Field | Type | Description | Example |
|-------|------|-------------|---------|
| `status` | string | Response status indicator | `error` |
| `error_type` | string | Classification of error returned | `bad_request`, `unauthorized`, `forbidden`, `conflict`, `internal_server_error`, `configuration` |
| `title` | string | Human-readable explanation providing error details | "Authentication with the API failed, please check your details and try again." |
| `instance` | string | Location where the error occurred | `/v1/login` |
| `invalid_parameters` | object | Details about parameter-specific errors | -- |
| `parameter` | string | Identifies where the error exists | `payment.card.scheme` |
| `reason` | string | Explanation of what caused the parameter error | "Scheme and number values do not match." |

## Error Response Examples

### 401 - Unauthorized

**Request with invalid app_key:**
```json
{
  "app_id": "app_id",
  "app_key": "deliberately_incorrect_app_key"
}
```

**Response:**
```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

### 409 - Conflict

**Request attempting to create duplicate customer:**
```json
{
  "reference": "customer_number_00001",
  "first_name": "Edward",
  "last_name": "Johnson",
  "dob": "1988-10-03",
  "billing": {
    "address": {
      "line_1": "152 Aldgate Street",
      "line_2": "",
      "city": "London",
      "state": "",
      "postcode": "E1 7RT",
      "country_code": "GB"
    }
  }
}
```

**Response:**
```json
{
  "status": "error",
  "error_type": "conflict",
  "title": "There was a conflict when trying to complete your request",
  "instance": "/v1/customers",
  "invalid_parameters": [
    {
      "parameter": "reference",
      "reason": "A customer already exists using reference customer_number_00001"
    }
  ]
}
```

### 400 - Bad Request

The API reports multiple validation errors simultaneously rather than one at a time.

**Request with multiple syntax errors:**
```json
{
  "transaction": {
    "order_id": "{{$guid}}",
    "amount": 20.99,
    "currency": "Gbpg",
    "moto": false,
    "capture": true
  },
  "payment": {
    "card_id": "",
    "card": {
      "holder_name": "E Smith",
      "scheme": "vis",
      "number": "400001118013871",
      "expiry_month": 10,
      "expiry_year": 26,
      "cvv": "12"
    },
    "create_card": true,
    "reference": "Custom Ref 001"
  },
  "customer": {
    "customer_id": "4f3970f9-3cf8-424d-299e-631dea1b7d30"
  },
  "tds": {
    "is_active": false,
    "challenge_preference": "no_preference",
    "challenge_window_size": "full_screen",
    "contact_url": "https://yourdomain.com/contact",
    "redirect_url": "https://qaacs.acquired.com/merchant_redirect/test_success",
    "webhook_url": ""
  }
}
```

**Response:**
```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/payments",
  "invalid_parameters": [
    {
      "parameter": "transaction.currency",
      "reason": "format is invalid, must be [aed,aud,acd,chf,cny,dkk,eur,gbp,hkd,jpy,mxn,sek,usd,zar]"
    },
    {
      "parameter": "payment.card.scheme",
      "reason": "payment.card.scheme must be string and in visa,mastercard,amex"
    },
    {
      "parameter": "payment.card.number",
      "reason": "payment.card.number validation failed"
    },
    {
      "parameter": "payment.card.cvv",
      "reason": "payment.card.cvv validation failed"
    }
  ]
}
```

## Error Logs

The Acquired.com Hub displays a comprehensive log of all API errors, including the initial request and complete response data.

**To access error logs:**

1. Navigate to Settings > Developers
2. Select the Error Log tab
3. Locate the required error (using available filters and export functionality)
4. Click the ellipsis menu and select View Details for full error information
