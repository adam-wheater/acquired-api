# Generate a Merchant-Session API Documentation

## Overview

This endpoint allows you to generate a merchant session for Apple Pay payment processing. The endpoint validates merchant identity to establish a secure session for Apple Pay transactions.

**Endpoint:** `POST https://test-api.acquired.com/v1/payment-methods/apple-pay/session`

**Production URL:** `https://api.acquired.com/v1/payment-methods/apple-pay/session`

## Authentication

The API requires Bearer token authentication. You must first obtain an access token through the login endpoint before making requests to this endpoint.

**Authentication Method:** Bearer Token (JWT)

## Key Purpose

As stated in the documentation: "For every new Apple Pay payment request you receive a session object. You must validate your merchant identity in order to generate the merchant-session."

## Request Details

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

Upon successful request, the API returns a session object containing the necessary credentials for processing Apple Pay transactions.

## Related Operations

This endpoint is part of the Payment Methods section of the Acquired API, which includes functionality for:
- Generating session identifiers
- Processing various payment types (card, Apple Pay, Google Pay)
- Managing payment links and hosted checkout

## Documentation Location

This operation is documented under the Payment Methods category within the Acquired API Reference, accessible via the main API documentation portal.
