# Single Immediate Payment API Documentation

## Overview

The "Single immediate payment" endpoint enables merchants to request consent from an ASPSP (Account Servicing Payment Service Provider) and generate an authentication URL for users.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/single-immediate-payment`

## Authentication

This endpoint requires Bearer token authentication via JWT or Bearer credentials.

## Request Parameters

The API schema indicates this endpoint accepts request bodies with transaction details, payment information, and customer data. The complete request structure appears to support:

- Transaction details (order_id, amount, currency, custom data fields)
- Payment method specifications
- Customer information
- 3D Secure configuration options
- Webhook URL specifications

## Response Format

Successful requests return a 201 (Created) status code with response headers for:
- `Company-Id` (UUID format)
- `Mid` (UUID format)

Error responses include:
- **400 Bad Request** - Validation failures with detailed invalid parameters
- **401 Unauthorized** - Authentication failures
- **404 Not Found** - Resource unavailable

## Key Features

The endpoint integrates with the broader Acquired API ecosystem for:
- Customer management
- Card storage and retrieval
- Payment processing
- Transaction handling
- Direct debit and variable recurring payments

## Related Documentation

Access additional guidance through the navigation menu covering fundamentals, authentication, customer operations, card management, payments, hosted checkout, and transaction processing.
