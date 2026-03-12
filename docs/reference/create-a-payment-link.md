# Send a Payment Link - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-links/{link_id}/send`

The functionality allows merchants to "transmit a payment link via email or SMS" to customers, enabling them to complete transactions through a hosted checkout interface.

## Prerequisites

You must first generate a Hosted Checkout payment link using the `/payment-links` endpoint before appending the `link_id` to this request.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization with access token

## Request Parameters

The endpoint requires:
- `link_id` (path parameter): Unique identifier for the generated payment link
- Authentication credentials in the header

## Server Endpoints

**Test Environment:**
`https://test-api.acquired.com/v1`

**Production Environment:**
`https://api.acquired.com/v1`

## Response Details

### Success Response (200 OK)
Returns confirmation that the payment link was successfully sent via the specified delivery method.

### Error Responses

**400 - Bad Request:** Invalid parameters or malformed request
**401 - Unauthorized:** Authentication failure
**404 - Not Found:** Payment link doesn't exist

## Supported Languages

The API documentation provides code examples in:
- Shell
- Node
- Ruby
- PHP
- Python

## Related Operations

This endpoint is part of the Payments section, which also includes:
- Process a payment
- Apple Pay integration
- Google Pay integration
- Recurring payments
- Fund transfers to cards
