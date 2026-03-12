# Tools - Overview

## Section Overview

The Tools section of the Acquired.com API provides utility endpoints for verification and validation operations within the payment ecosystem.

## Available Tools

### Confirmation of Payee
**POST** `https://test-api.acquired.com/v1/tools/confirmation-of-payee`

Verify your payee's account details before adding them to our system or initiating a payment.

## Authentication

All endpoints in this section require Bearer Token (JWT) authentication.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Navigation Context

The Tools section is part of the broader Acquired API reference documentation, which includes:

- Authentication endpoints
- Customer management
- Card operations
- Payment processing
- Hosted Checkout functionality
- Components for session management
- Payment Methods configuration
- Transaction handling
- Pay by Bank solutions
- Faster Payments operations
- Direct Debit management
- Variable Recurring Payments
- Reporting capabilities

## Code Examples

Available in: Shell, Node, Ruby, PHP, Python

## Integration Notes

- Authentication tokens are generated via the `/login` endpoint using your `app_id` and `app_key`
- Include the `Company-Id` header for request routing
- Responses follow standard HTTP status codes with detailed error messages for validation failures
