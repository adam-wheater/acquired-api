# Generate a session_id - API Documentation

## Overview

The "Generate a session_id" endpoint allows you to create a payment session for the Acquired API Components solution. According to the documentation, when using Components, you must "submit a request that includes payment details to create a `session_id`."

## Endpoint Details

**HTTP Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-sessions`

## Required Parameters

At minimum, the following fields must be included:
- `order_id` - Unique reference for the payment request
- `amount` - Total charge amount
- `currency` - ISO 4217 currency code (lowercase)

The documentation notes that submitting "3DS requirements at this stage" is also recommended.

## Authentication

The endpoint supports two credential types:
- Bearer tokens
- JWT authentication

## Request Structure

The request body schema includes these main objects:

**Transaction Object:**
- order_id (string, 2-50 characters, alphanumeric with hyphens)
- amount (float)
- currency (enum: aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- moto (boolean, default: false) - Set true for phone payments
- capture (boolean, default: true) - False for authorization only
- custom_data (base64 encoded)
- custom1, custom2 (up to 50 characters each)

**Payment Object:**
- Card details (holder_name, number, expiry_month, expiry_year, cvv)
- create_card (boolean) - Persist card for future use
- reference (up to 18 characters)

**Customer Object:** Optional customer information including name, DOB, billing/shipping addresses, email, phone

**TDS Object:** 3D Secure configuration with challenge preferences and redirect URLs

## Response

**Success Response (200/201):**
Returns the generated `session_id` for use in subsequent payment processing steps.

**Error Responses:**
- 400: Validation errors with invalid_parameters array
- 401: Authentication failure
- 403: Access denied

## Key Notes

The `session_id` holds transaction-specific information and is required to complete payments through the Components solution. The session maintains all submitted payment and customer details for the transaction lifecycle.
