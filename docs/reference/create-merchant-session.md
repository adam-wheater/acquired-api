# Generate a Merchant-Session - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-methods/apple-pay/session`

This endpoint generates a merchant session object for Apple Pay payment requests. Validation of merchant identity is required to generate the merchant-session.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header as a Bearer token.

## Request Details

### Base URLs
- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| Authorization | String | Yes | Bearer token for authentication |
| Company-Id | UUID | Optional | Unique ID for your company |

### Request Body

The endpoint accepts a JSON payload for the merchant session request. The complete schema includes nested objects for transaction details, payment information, customer data, and 3D Secure configuration.

**Key Request Parameters:**

- **transaction** (object): Contains order details including order_id, amount, currency
- **payment** (object): Payment method configuration
- **customer** (object): Customer information for the transaction
- **tds** (object): 3D Secure authentication settings

## Response

### Success Response (201 Created)

A successful request returns a merchant session object with the following structure:

```json
{
  "session_id": "string (UUID)",
  "merchant_id": "string",
  "display_name": "string",
  "initiative": "string",
  "initiative_context": "string"
}
```

### Error Responses

**400 Bad Request**
- Invalid request parameters
- Includes error_type, title, and invalid_parameters array

**401 Unauthorized**
- Authentication failure
- Invalid or expired token

**404 Not Found**
- Resource not found

## Use Cases

This endpoint is essential for Apple Pay implementations. Developers must validate merchant identity and generate a new session object for each payment request to ensure secure transaction processing.

## Language Support

Documentation includes code examples for:
- Shell
- Node
- Ruby
- PHP
- Python
