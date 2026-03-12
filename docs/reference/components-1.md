# Generate a session_id - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-sessions`

## Description

When utilizing the Components solution, you must submit a request containing payment details to generate a `session_id`. This identifier stores transaction-specific information needed to finalize the payment. Minimum required fields include `order_id`, `amount`, and `currency`. It's recommended to include your 3DS requirements at this stage.

## Authentication

**Type:** Bearer Token (JWT)

## Request Body Schema

The request structure contains several nested objects:

### Transaction Object (Required)
- **order_id** (string, required): Unique reference for the payment request
  - Length: 2-50 characters
  - Pattern: alphanumeric and hyphens
  - Example: `1f1f2a61-5b68-4725-a0ce-9560514ec00b`

- **amount** (number, required): Total charge amount
  - Format: float
  - Example: `15.02`

- **currency** (string, required): ISO 4217 code (lowercase)
  - Supported: aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar
  - Example: `gbp`

- **moto** (boolean, optional): Phone payment indicator
  - Default: `false`

- **capture** (boolean, optional): Authorize-only flag
  - Default: `true`

- **custom_data** (string, optional): Base64-encoded custom information

- **custom1, custom2** (strings, optional): Additional reference data
  - Max length: 50 characters

### Payment Object
- **card** (object): Card details including holder_name, number, expiry_month, expiry_year, cvv
- **create_card** (boolean): Indicates whether to store card for future transactions
- **reference** (string, optional): Bank statement descriptor (1-18 characters)

### Customer Object
Includes optional customer identification via `customer_id` or inline customer details (first_name, last_name, dob, billing address, shipping address, contact information).

### 3DS Object (tds)
- **is_active** (boolean): Enable EMV 3D Secure authentication
- **challenge_preference** (string): "challenge_mandated" | "challenge_preferred" | "no_challenge_requested" | "no_preference"
- **challenge_window_size** (string): Display size options for authentication windows
- **contact_url** (string): Support page URI
- **redirect_url** (string): Post-authentication redirect destination
- **webhook_url** (string): Notification endpoint for authorization updates

## Response Format

Successful responses return a `session_id` that holds transaction-specific information for component processing and payment completion.

## Related Operations

- Update a session_id (PUT)
- Additional payment methods supported: card_id-based payments, Apple Pay, Google Pay, recurring payments
