# Acquired.com API Documentation

Combined reference — scraped 2026-03-12T21:27:24Z
Source: https://docs.acquired.com/reference

---

## Table of Contents

- [authentication-2](#authentication-2)
- [cards](#cards)
- [components-1](#components-1)
- [confirm-funds-variable-recurring-payment](#confirm-funds-variable-recurring-payment)
- [country-currency-codes](#country-currency-codes)
- [create-account](#create-account)
- [create-a-collection](#create-a-collection)
- [create-a-direct-debit-cancellation](#create-a-direct-debit-cancellation)
- [create-a-mandate-cancellation](#create-a-mandate-cancellation)
- [create-a-mandate](#create-a-mandate)
- [create-a-payment-link](#create-a-payment-link)
- [create-apple-pay](#create-apple-pay)
- [create-a-retry](#create-a-retry)
- [create-capture](#create-capture)
- [create-customer](#create-customer)
- [create-funds](#create-funds)
- [create-google-pay](#create-google-pay)
- [create-internal-transfer](#create-internal-transfer)
- [create-login](#create-login)
- [create-mandate](#create-mandate)
- [create-merchant-session](#create-merchant-session)
- [create-payee](#create-payee)
- [create-payment-links](#create-payment-links)
- [create-payment](#create-payment)
- [create-payment-stored-card](#create-payment-stored-card)
- [create-payout](#create-payout)
- [create-recurring-payment](#create-recurring-payment)
- [create-refund](#create-refund)
- [create-reversal](#create-reversal)
- [create-session-id](#create-session-id)
- [create-single-immediate-payment](#create-single-immediate-payment)
- [create-variable-recurring-payment](#create-variable-recurring-payment)
- [create-void](#create-void)
- [customers-3](#customers-3)
- [direct-debit-1](#direct-debit-1)
- [dynamic-descriptor](#dynamic-descriptor)
- [expanding-responses](#expanding-responses)
- [faster-payments](#faster-payments)
- [fetch-accounts-by-mid](#fetch-accounts-by-mid)
- [fetch-all-accounts](#fetch-all-accounts)
- [fetch-all-cards](#fetch-all-cards)
- [fetch-all-customers](#fetch-all-customers)
- [fetch-all-payees](#fetch-all-payees)
- [fetch-all-transactions](#fetch-all-transactions)
- [fetch-aspsp](#fetch-aspsp)
- [fetch-cards-by-id](#fetch-cards-by-id)
- [fetch-customer-by-id](#fetch-customer-by-id)
- [fetch-customer-cards](#fetch-customer-cards)
- [fetch-mandate-by-id](#fetch-mandate-by-id)
- [fetch-mandate](#fetch-mandate)
- [fetch-recon-by-id](#fetch-recon-by-id)
- [fetch-recon-reports](#fetch-recon-reports)
- [fetch-transaction-by-id](#fetch-transaction-by-id)
- [handling-errors](#handling-errors)
- [hosted-checkout](#hosted-checkout)
- [http-responses](#http-responses)
- [introduction](#introduction)
- [issuer-response-codes](#issuer-response-codes)
- [list-mandates](#list-mandates)
- [list-supported-banks](#list-supported-banks)
- [list-variable-recurring-payments](#list-variable-recurring-payments)
- [pagination](#pagination)
- [pay-by-bank-1](#pay-by-bank-1)
- [pay-by-bank](#pay-by-bank)
- [payment-methods-1](#payment-methods-1)
- [payments](#payments)
- [post-confirmation-of-payee](#post-confirmation-of-payee)
- [reasons](#reasons)
- [references](#references)
- [reports](#reports)
- [statuses](#statuses)
- [tools](#tools)
- [transactions](#transactions)
- [update-card-details](#update-card-details)
- [update-customer](#update-customer)
- [update-session-id](#update-session-id)
- [variable-recurring-payments-1](#variable-recurring-payments-1)

---

<a id="authentication-2"></a>

## authentication-2

# Create an access_token - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/login`

## Description

Access to the API is by Bearer Tokens, this request generates an `access_token` that authenticates subsequent API calls. Client credentials are passed through the login endpoint to obtain the token.

## Request Body

The endpoint accepts `application/json` with the following required fields:

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `app_id` | string | 8 characters exactly | Unique ID assigned by Acquired.com to your application |
| `app_key` | string | 32 characters exactly | Secret key used as part of the authentication process |

**Example Request:**
```json
{
  "app_id": "72693514",
  "app_key": "de0eef006b4c30440aeca8ca5ac58789"
}
```

## Response Schemas

### 200 OK Response

Success returns token authentication details:

| Field | Type | Description |
|-------|------|-------------|
| `token_type` | string | Details the token type returned (e.g., "Bearer") |
| `expires_in` | integer | Time in seconds until the `access_token` expires |
| `access_token` | string | Unique token that can be used to authenticate and log into the API |

**Example:**
```json
{
  "token_type": "Bearer",
  "expires_in": 3600,
  "access_token": "eyJ0eXAiOiJKV1QiLC....."
}
```

### 400 Bad Request

Validation failures return error details including `invalid_parameters` array with parameter names and reasons.

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/login",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

### 401 Unauthorized

Authentication with the API failed, please check your details and try again.

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Usage Notes

- Generated tokens function as bearer tokens for subsequent API requests
- Must be included in the Authorization header for all authenticated endpoints
- Token format: `Authorization: Bearer {access_token}`
- Tokens expire after the duration specified in `expires_in` (typically 3600 seconds / 1 hour)

---

<a id="cards"></a>

## cards

# Cards - API Documentation

## Overview

The Cards endpoints allow you to manage payment cards associated with customers. Cards can be retrieved individually, as a full list, filtered by customer, or updated. All cards are associated to a `customer_id`.

## Endpoints

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/v1/customers/{customer_id}/cards` | Get customer cards |
| GET | `/v1/cards` | List all cards |
| GET | `/v1/cards/{card_id}` | Get card details |
| PUT | `/v1/cards/{card_id}` | Update card details |

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** `Authorization: Bearer {access_token}`

## Card Object Schema

### Root Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | The unique ID assigned by Acquired.com to the card when it is created |
| `customer_id` | UUID | Yes | The unique ID assigned by Acquired.com to the customer when it is created |
| `card` | object | No | Card information object |
| `bin` | object | Yes | Bank Identification Number details |
| `source` | string | Yes | Identifies how the card was created: `card`, `apple_pay`, or `google_pay` |
| `created` | date-time | Yes | The date and time that the card record was created |
| `last_updated` | date-time | Yes | The date and time the card record was last updated |
| `is_active` | boolean | No | Indicates whether a card is active or not (default: true) |

### Card Object Properties

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, letters/spaces only | The name of the cardholder |
| `scheme` | enum | - | Card scheme: `visa`, `mastercard`, `amex` |
| `number` | string | - | The last 4 digits of the card number (read responses) / full card number (update requests) |
| `expiry_month` | integer | 1-12 | The month the card expires in |
| `expiry_year` | integer | 22-32 | The year the card expires in |

### BIN Object Properties (Read-Only)

| Property | Type | Description |
|----------|------|-------------|
| `number` | string | The first 6 digits of the card number |
| `scheme` | enum | Card scheme: `visa`, `mastercard`, `amex` |
| `type` | enum | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `product` | enum | Card product: `consumer`, `commercial` |
| `card_level` | enum | Card tier (standard, gold, platinum, business, etc.) |
| `issuer` | string | The name of the bank that issued the card |
| `issuer_country` | string | The name of the country the card was issued in |
| `issuer_country_code` | string | The ISO 3166 2-character country code the card was issued in |
| `eea` | boolean | If the card was issued in the EEA |
| `non_reloadable` | boolean | If the card is a non-refundable prepaid card |

## Pagination

List endpoints return paginated results with a `meta` object:

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      {
        "rel": "self",
        "href": "/v1/cards?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/cards?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/cards?offset=125",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/cards?offset=25",
        "title": "Next page"
      }
    ]
  }
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/cards",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
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

## Key Notes

- Card numbers are masked in responses, showing only the last 4 digits
- BIN details are read-only and provided for informational purposes
- The `filter` query parameter allows customization of returned fields to optimize response payloads
- Cards track activation status via `is_active` field
- All date fields use ISO 8601 format

---

<a id="components-1"></a>

## components-1

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

---

<a id="confirm-funds-variable-recurring-payment"></a>

## confirm-funds-variable-recurring-payment

# Initiate a Confirmation of Funds Check Against a Mandate

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates/{mandate_id}/confirm-funds`

## Description

This endpoint enables merchants to initiate a verification process that checks whether sufficient funds are available against an established mandate. This is part of the Variable Recurring Payments workflow.

## Authentication

**Type:** Bearer Token (JWT)
**Required Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier for the mandate |

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Request Body

The endpoint accepts JSON request payloads with mandate-related details for funds confirmation:

```json
{
  "amount": "number",
  "currency": "string"
}
```

## Response Schema

### Success Response (201 Created)

Returns confirmation details of the funds check initiation, including:
- Funds availability status
- Amount checked
- Mandate reference
- Timestamp

### Error Responses

| Status Code | Scenario |
|-------------|----------|
| 400 | Validation failures or invalid parameters |
| 401 | Authentication credentials missing or invalid |
| 404 | Specified mandate not found |

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error description",
  "instance": "/v1/open-banking/mandates/{mandate_id}/confirm-funds",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Context

This endpoint operates within the Variable Recurring Payments section, which includes operations for:
- List supported banks
- Create mandates
- List mandates
- Retrieve open banking mandates
- Initiate variable recurring payments
- List variable recurring payments

---

<a id="country-currency-codes"></a>

## country-currency-codes

# Country & Currency Codes Documentation

## Overview

This reference page provides a comprehensive lookup table for ISO standards used in the Acquired API, specifically for customer address and payment processing.

## Purpose

The documentation states: *"The table below details the ISO 3166 country code used in the customer's address in the `address.country_code` parameter, the international dialling code in the `phone.country_code` parameter and the ISO 4217 `currency_code` parameter."*

## Table Structure

The reference table contains six columns:

1. **Country** - Official country/territory name
2. **Country Code** - ISO 3166 two-letter code
3. **Dialling Code** - International phone prefix
4. **Currency Name** - Full currency denomination
5. **Currency Code** - ISO 4217 three-letter code (lowercase)
6. **Numeric Currency Code** - ISO 4217 numeric identifier

## Coverage

The documentation covers 249+ countries and territories alphabetically, from Afghanistan through Wallis and Futuna. Examples include:

- **United States**: US | 1 | USD | 840
- **United Kingdom**: GB | 44 | GBP | 826
- **Japan**: JP | 81 | JPY | 392
- **Euro Zone Nations**: Multiple countries share EUR | 978

## Key Features

- Standardized ISO compliance for international transactions
- Supports multi-currency payment processing
- Enables global customer address validation
- Facilitates proper phone number formatting by region

---

<a id="create-account"></a>

## create-account

# Create an Account - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/accounts`

This endpoint enables account creation for payment processing. Submit account information to receive an `account_id` in the response.

## Authentication

- **Type:** Bearer Token
- **Alternative:** JWT

## Request Structure

The endpoint accepts account details in the request body. Based on the OpenAPI specification provided, the request should include account-related information.

## Headers

- **Company-Id** (optional): UUID identifying your company
- **Mid** (optional): UUID connecting to acquiring bank

## Response

**Success Status:** 201 Created

The successful response returns an account identifier that can be used for subsequent payment operations.

## Error Responses

The API returns structured error responses including:

- **400 Bad Request:** "Validation failed" - includes `invalid_parameters` array detailing which fields failed
- **401 Unauthorized:** "Authentication failed, verify credentials and retry"
- **404 Not Found:** Resource unavailable

Each error response contains:
- `status`: Current request status
- `error_type`: Classification of error
- `title`: Human-readable explanation
- `instance`: Location where error occurred

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Related Operations

This endpoint is part of the Faster Payments section, alongside:
- Create a payee
- Process a payout
- List all accounts
- Retrieve account details
- Internal transfer
- List all payees

---

<a id="create-a-collection"></a>

## create-a-collection

# Create a Collection

## Overview

This endpoint enables merchants to establish a Direct Debit collection against a `mandate_id` to collect payments directly from a customer's bank account.

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/collections`

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization with access token

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Headers

| Header | Type | Description |
|--------|------|-------------|
| Authorization | string | Bearer token for authentication |
| Company-Id | string (uuid) | Your assigned company identifier |

### Body Schema

The request accepts a JSON payload with the following structure:

```json
{
  "mandate_id": "string (uuid)",
  "amount": "number",
  "currency": "string",
  "reference": "string",
  "custom_data": "string (base64 encoded)"
}
```

### Required Fields
- `mandate_id` (string, UUID): Unique identifier for the mandate
- `amount` (number): Transaction amount
- `currency` (string): ISO 4217 currency code (lowercase)

### Optional Fields
- `reference` (string): Custom reference for tracking
- `custom_data` (string): Base64-encoded custom information

## Response

### Success Response (201 Created)

The endpoint returns a collection object containing:
- `collection_id`: Unique identifier for the collection
- `mandate_id`: Associated mandate reference
- `status`: Current collection status
- `amount`: Transaction amount processed
- `currency`: Currency used
- `created`: Timestamp of creation

### Error Responses

**400 Bad Request** - Validation errors with invalid parameters

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/payments/collections",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Authentication failure

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/payments/collections"
}
```

**404 Not Found** - Mandate or resource not found

## Related Operations

This endpoint is part of the Direct Debit payment methods section, which includes:
- Create a mandate
- Cancel a mandate
- Cancel Direct Debit
- Process a retry
- Retrieve mandate details

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python

---

<a id="create-a-direct-debit-cancellation"></a>

## create-a-direct-debit-cancellation

# Cancel a Direct Debit

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/cancel`

## Description

This endpoint enables cancellation of Direct Debit transactions prior to submission to banking institutions. The operation stores any custom data against the cancellation event and will fail if the transaction is not in pending status.

## Authentication

- **Type:** Bearer Token (JWT)
- **Requirement:** Required for this endpoint

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction to cancel |

## Request Body

The endpoint accepts custom data to be stored with the cancellation event:

```json
{
  "custom_data": "Base64 encoded string detailing custom data"
}
```

## Response Schemas

### Success Response (200 OK)

```json
{
  "status": "success",
  "transaction_id": "UUID",
  "cancelled_at": "ISO 8601 datetime"
}
```

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/cancel",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/transactions/{transaction_id}/cancel"
}
```

**404 Not Found:**

Transaction ID does not exist or is invalid.

## Key Constraints

- Transaction must be in **pending status** to cancel successfully
- Operation cannot proceed if transaction has already been submitted to banks
- Custom data must be Base64 encoded if provided

## Related Operations

This function operates within the Direct Debit section alongside:
- Create a mandate
- Create a collection
- Cancel a mandate
- Process a retry
- Retrieve mandate details

---

<a id="create-a-mandate-cancellation"></a>

## create-a-mandate-cancellation

# Cancel a Mandate

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates/{mandate_id}/cancel`

## Description

This endpoint enables immediate cancellation of an active mandate. The process will reject requests if the mandate isn't in an active status. Once cancelled, a mandate cannot revert to active -- you must create a fresh mandate instead. Custom data submitted with this request associates with the resulting cancellation event.

## Authentication

**Type:** Bearer Token (JWT)

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | The unique identifier for the mandate to cancel |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Authorization` | string | Bearer token for authentication |
| `Company-Id` | string (UUID) | Your assigned company identifier |

### Request Body

```json
{
  "custom_data": "string (base64 encoded)"
}
```

## Response Schemas

### Success Response (200 OK)

The API returns confirmation of the cancellation action.

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Request parameters failed validation",
  "instance": "/v1/mandates/{mandate_id}/cancel",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "error description"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/mandates/{mandate_id}/cancel"
}
```

**404 Not Found:**

Mandate not found or doesn't exist.

## Important Notes

- The mandate must maintain active status for cancellation to succeed
- Cancelled mandates are permanent -- reactivation is impossible
- Any custom data included becomes linked to the cancellation event
- You must create a new mandate if you need to resume collections

## Related Endpoints

- Create a mandate: POST `/mandates`
- Create a collection: POST `/payments/collections`
- Retrieve mandate: GET `/mandates/{mandate_id}`

---

<a id="create-a-mandate"></a>

## create-a-mandate

# Create a Mandate (Direct Debit)

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates`

The first step in the Direct Debit process is to create a mandate. A mandate must be logged against a customer's bank account before Direct Debit collections can be made.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization with Bearer token from login endpoint

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Key Information

### Purpose
Setting up a mandate enables merchants to collect payments via Direct Debit from a customer's bank account. This must be established before any collections can occur.

### Request Structure
The API expects authentication credentials and mandate details to be submitted. The system validates all parameters before processing.

### Request Parameters

- **customer_id** (string, UUID): Unique identifier for the customer
- **bank_account** (object): Bank account details including:
  - Account holder name
  - Account number
  - Sort code (for UK accounts)
  - IBAN (for international accounts)
- **mandate_reference** (string): Your unique reference for the mandate
- **scheme** (string): Payment scheme (typically "sepa" or "bacs")

### Response

Upon successful creation, the endpoint returns a mandate confirmation with relevant identifiers and status information.

**Success Response (201 Created):**

```json
{
  "mandate_id": "uuid",
  "customer_id": "uuid",
  "status": "pending_approval",
  "mandate_reference": "string",
  "created": "date-time"
}
```

## HTTP Status Codes

- **201 Created:** Mandate successfully created
- **400 Bad Request:** Invalid parameters or validation failure
- **401 Unauthorized:** Authentication failed
- **404 Not Found:** Customer or resource not found

## Error Response Format

Failed requests return error details with:
- `status`: "error"
- `error_type`: Type of error (validation, unauthorized, etc.)
- `title`: Human-readable error message
- `instance`: Where the error occurred
- `invalid_parameters`: Array of validation issues

## Related Operations

The Direct Debit section includes complementary endpoints for:
- Creating collections against established mandates
- Canceling mandates when needed
- Processing retry attempts
- Retrieving mandate details

## Integration Notes

This endpoint requires a valid company context and appropriate access credentials. The mandate creation is a prerequisite step in the broader Direct Debit workflow, allowing subsequent collection operations against customer bank accounts.

---

<a id="create-a-payment-link"></a>

## create-a-payment-link

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

---

<a id="create-apple-pay"></a>

## create-apple-pay

# Apple Pay API Documentation

## Overview

The Apple Pay endpoint enables processing of encrypted Apple Pay payment requests through the Acquired API.

**Endpoint:** `POST https://test-api.acquired.com/v1/payments/apple-pay`

**Authentication:** Bearer Token or JWT

## Request Structure

When users select the Apple Pay button, "a payment request will be created and an encrypted payload will be returned" to your system.

### Core Request Components

The request body includes:

**Transaction Object:**
- `order_id` (string, 2-50 chars): Unique reference for payment request
- `amount` (number): Total charge amount
- `currency` (string): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- `moto` (boolean): Set true for phone transactions
- `capture` (boolean): false for authorization only; true for immediate capture
- `custom_data` (string): Base64 encoded custom data
- `custom1`, `custom2` (strings): Additional reference fields

**Payment Object:**
- Apple Pay encrypted payload data

**Customer Object:**
- `customer_id` (UUID): Link to existing customer
- Billing/shipping address information
- Contact details

**3DS Configuration:**
- `is_active` (boolean): Enable EMV 3-D Secure authentication
- `challenge_preference`: Challenge handling preference
- `redirect_url`: Post-authentication redirect destination
- `webhook_url`: Notification endpoint for auth status updates

## Response

**Success (201 Created):**
Returns transaction details including:
- `transaction_id`: Unique transaction identifier
- Payment status and authorization details
- Card information (masked)

**Error Responses:**
- 400: Validation failures with specific parameter details
- 401: Authentication failure
- 403: Access denied to requested resource

## Required Headers

- `Company-Id`: UUID for your company
- `Mid`: UUID linking to acquiring bank (optional)

---

<a id="create-a-retry"></a>

## create-a-retry

# Process a Retry

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/retry`

## Description

This endpoint enables retrying Direct Debit transactions that have been declined, specifically in cases where the failure occurred due to insufficient funds.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Identifier for the transaction to retry |

## Request Body

The request body may include custom data to associate with the retry event:

```json
{
  "custom_data": "Base64 encoded string"
}
```

## Response Codes

| Status | Description |
|--------|-------------|
| 201 | Created/Success - Retry initiated |
| 400 | Bad Request - Validation errors |
| 401 | Unauthorized - Authentication failures |
| 404 | Not Found - Transaction not found |

### Error Response Format

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/retry",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/transactions/{transaction_id}/retry"
}
```

## Implementation Languages

Code examples available in: Shell, Node, Ruby, PHP, Python

## Related Endpoints

This retry functionality is part of the Direct Debit section, which also includes:
- Create a mandate
- Create a collection
- Cancel a mandate
- Cancel a Direct Debit
- Retrieve a mandate

## Security Notes

Requests require valid bearer token authentication obtained through the `/login` endpoint using your `app_id` and `app_key`.

---

<a id="create-capture"></a>

## create-capture

# Process a Capture

## Overview

Process an authorisation of the transaction and then capture the funds later. Append the required `transaction_id` to the URL and include the capture amount in the request body. This operation converts an authorization hold into an actual charge on the cardholder's account.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/capture`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/capture`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/capture`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction to be captured |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique company identifier assigned by Acquired.com |
| `Mid` | string (UUID) | No | Merchant ID assigned by Acquired.com connecting to specific acquiring bank |

### Request Body Schema

The capture request accepts an amount specification to allow partial captures:

```json
{
  "amount": 15.02
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | Yes | The monetary value to capture from the authorized transaction |

## Key Concepts

- A capture operation requires a preceding authorization. The merchant must first process an authorization (by setting `capture: false` during payment creation), then later submit the capture request to complete the transaction lifecycle.
- The capture amount can be **less than** the originally authorized amount, enabling **partial capture** scenarios.
- The remaining authorized balance typically becomes available for release back to the cardholder.
- If no amount is specified, the full authorized amount may be captured.

## Response Format

### Success Response (200 OK)

```json
{
  "transaction_id": "string (UUID)",
  "status": "string",
  "amount": 15.02,
  "currency": "gbp",
  "transaction_type": "string",
  "created": "2024-01-15T10:30:00Z",
  "reference": "string"
}
```

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Capture processed successfully |
| 400 | Bad Request - Your request parameters did not pass validation |
| 401 | Unauthorized - Authentication failed |
| 404 | Not Found - Resource does not exist |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/capture",
  "invalid_parameters": [
    {
      "parameter": "amount",
      "reason": "Amount exceeds the authorized amount"
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
  "instance": "/v1/transactions/{transaction_id}/capture"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/capture"
}
```

## Related Endpoints

- [Create Payment](transactions.md) - Initial authorization and payment processing
- [Process a refund](create-refund.md) - Reversing previously captured amounts
- [Process a void](create-void.md) - Canceling an authorization before capture occurs
- [Process a reversal](create-reversal.md) - Automatic void/refund determination
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="create-customer"></a>

## create-customer

# Create a Customer - API Documentation

## Overview

The Create a Customer endpoint allows you to establish a customer record that serves as the central reference point for all associated transactions, beneficiaries, and payment instruments. Everything can be linked back to a `customer_id`, making this the foundational step in the payment workflow.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/customers`
**Authentication:** Bearer Token (JWT)

## Request Parameters

### Header Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `Company-Id` | UUID | Yes | Unique identifier assigned by Acquired for your company |

### Request Body Schema

#### Customer Fields

| Parameter | Type | Length | Required | Description |
|-----------|------|--------|----------|-------------|
| `reference` | string | 0-50 | No | Your unique customer reference (alphanumeric, hyphens, periods, commas) |
| `first_name` | string | 0-22 | No | Customer's first name (alphabetic only) |
| `last_name` | string | 0-22 | No | Customer's last name (required for MCC 6012 merchants) |
| `dob` | date | 10 | No | Date of birth YYYY-MM-DD (required for MCC 6012) |
| `custom_data` | string | - | No | Base64-encoded custom data |

#### Billing Object

**Address:**

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `line_1` | string | 0-50 | Primary address line (recommended for EMV 3DS) |
| `line_2` | string | 0-50 | Secondary address line |
| `city` | string | 1-40 | Municipality |
| `state` | string | 0-3 | ISO 3166-2 state code (US only) |
| `postcode` | string | 1-40 | Postal code (recommended for MCC 6012) |
| `country_code` | string | 2 | ISO 3166 country code (recommended for EMV 3DS) |

**Contact:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `email` | string | Customer email address (recommended for EMV 3DS) |
| `phone.country_code` | string (1-3 digits) | International dialing prefix |
| `phone.number` | string (0-15 digits) | Telephone number (numeric only) |

#### Shipping Object

| Parameter | Type | Description |
|-----------|------|-------------|
| `address_match` | boolean | Set to true to auto-populate from billing address |
| `address` | object | Same structure as billing address |
| `email` | string | Shipping contact email |
| `phone` | object | Same structure as billing phone |

## Response Format

### Success Response (201 Created)

```json
{
  "customer_id": "550e8400-e29b-41d4-a716-446655440000"
}
```

The system returns a unique identifier (UUID) for the newly created customer record.

### Error Responses

**400 Bad Request:** Validation failures with parameter-level details.

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

**401 Unauthorized:** Authentication credentials invalid or missing.

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

**403 Forbidden:** User lacks permission to access the requested resource.

**409 Conflict:** A conflict occurred during processing (e.g., duplicate reference).

## Related Endpoints

- **List all customers:** `GET /v1/customers`
- **Retrieve a customer:** `GET /v1/customers/{customer_id}`
- **Update a customer:** `PUT /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`

---

<a id="create-funds"></a>

## create-funds

# Send Funds to a Card - API Documentation

## Overview

This endpoint enables direct fund transfers to customer cards using Visa Direct and Mastercard Send technologies. The API accepts transaction details and card information to initiate transfers.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/credit`

## Authentication

The endpoint requires Bearer token authentication via JWT.

## Request Structure

### Headers
- `Company-Id` (UUID): Your company identifier assigned by Acquired
- `Mid` (UUID): Merchant ID connecting to your acquiring bank

### Request Body

The request follows this general structure:

```json
{
  "transaction": {
    "order_id": "string",
    "amount": "number",
    "currency": "string",
    "moto": "boolean",
    "capture": "boolean",
    "custom_data": "string",
    "custom1": "string",
    "custom2": "string"
  },
  "payment": {
    "card": {
      "holder_name": "string",
      "number": "string",
      "expiry_month": "integer",
      "expiry_year": "integer",
      "cvv": "string"
    },
    "reference": "string"
  },
  "customer": {
    "customer_id": "UUID",
    "reference": "string",
    "first_name": "string",
    "last_name": "string",
    "dob": "date",
    "custom_data": "string",
    "billing": {},
    "shipping": {}
  }
}
```

### Transaction Parameters

| Parameter | Type | Description | Required |
|-----------|------|-------------|----------|
| `order_id` | string | Your unique reference for the payment | Yes |
| `amount` | number | Transfer amount (decimal format) | Yes |
| `currency` | string | ISO 4217 code (lowercase) | Yes |
| `moto` | boolean | Phone-based payment indicator | No |
| `capture` | boolean | Auto-capture funds (default: true) | No |
| `custom_data` | string | Base64-encoded custom metadata | No |
| `custom1` | string | Additional reference data | No |
| `custom2` | string | Additional reference data | No |

### Supported Currencies

aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

### Payment Card Parameters

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, letters/spaces | Card holder's name |
| `number` | string | Valid card number | Full card number |
| `expiry_month` | integer | 1-12 | Expiration month |
| `expiry_year` | integer | 22-32 | Two-digit year |
| `cvv` | string | 3-4 digits | Security code |

### Customer Information

Customer data can include:
- Basic details (first/last name, DOB)
- Billing address with postal/country codes
- Shipping address (with option to match billing)
- Contact information (email, phone with country code)

**Note:** Last name and DOB are required for MCC 6012 merchants.

## Response Format

A successful transfer returns a 201 status with transaction confirmation details including:
- Transaction ID
- Status confirmation
- Settlement information

## Error Responses

The API returns standardized error responses:

**400 Bad Request** - Validation failures with specific parameter issues
**401 Unauthorized** - Authentication failures
**404 Not Found** - Resource doesn't exist

Error responses include:
- `status`: Error indicator
- `error_type`: Category (validation, unauthorized, forbidden)
- `title`: Human-readable explanation
- `instance`: Endpoint where error occurred
- `invalid_parameters`: Array of specific validation failures

## Key Considerations

- Billing address information improves AVS checks
- Email addresses support EMV 3DS processing
- Phone numbers require international dialing codes
- Custom data must be Base64-encoded
- The `reference` parameter appears on cardholders' bank statements when supported

---

<a id="create-google-pay"></a>

## create-google-pay

# Google Pay API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments/google-pay`

This endpoint processes Google Pay payments. When users select Google Pay as their payment method, the system creates a payment request and returns an encrypted payload to your application.

## Authentication

The endpoint requires Bearer token authentication via JWT. Include your access token in the Authorization header.

## Request Parameters

### Headers
- **Company-Id** (optional): UUID identifying your company
- **Mid** (optional): UUID connecting to a specific acquiring bank

### Request Body

The request accepts a JSON object with the following primary sections:

**Transaction Object** (required):
- `order_id` (string, 2-50 chars): Your unique payment reference
- `amount` (float): Total charge amount
- `currency` (enum): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- `moto` (boolean, default: false): Phone payment flag
- `capture` (boolean, default: true): Authorization-only mode when false
- `custom_data` (string): Base64-encoded custom information
- `custom1`, `custom2` (string, max 50 chars): Additional reference fields

**Payment Object**:
- `google_pay_token`: Encrypted Google Pay payload
- `reference` (string, max 18 chars): Bank statement descriptor
- `create_card` (boolean, default: false): Save for future use

**Customer Object** (optional):
- `customer_id` (UUID): Existing customer identifier
- `reference` (string, max 50 chars): Your customer reference
- `first_name` (string, max 22 chars): Customer's given name
- `last_name` (string, max 22 chars): Customer's surname
- `dob` (date format YYYY-MM-DD): Date of birth
- `custom_data` (string): Base64-encoded custom data
- Billing and shipping address objects with: line_1, line_2, city, state, postcode, country_code
- Email and phone contact information

**3DS Object** (optional):
- `is_active` (boolean): Enable EMV 3-D Secure authentication
- `challenge_preference` (enum): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- `challenge_window_size` (enum): full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- `contact_url` (URI): Support page link
- `redirect_url` (URI): Post-authentication redirect
- `webhook_url` (URI): Webhook notification endpoint

## Response

**Success (201 Created)**:
Returns a JSON object containing:
- `transaction_id` (string): Unique transaction identifier
- Payment status and details
- Card information (if created)
- Authorization codes and timestamps

**Error Responses**:

- **400 Bad Request**: Validation failures with invalid_parameters array detailing specific field errors
- **401 Unauthorized**: Authentication failure
- **404 Not Found**: Resource not found

## Key Features

- Supports both authorization and capture modes
- Optional customer creation during payment
- 3-D Secure authentication available
- Card tokenization for recurring payments
- Custom data transmission for reconciliation
- Webhook support for asynchronous updates

---

<a id="create-internal-transfer"></a>

## create-internal-transfer

# Internal Transfer API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/internal-transfer`
**Description:** "Process an internal transfer between accounts."

## Authentication

The endpoint requires Bearer token authentication via JWT credentials.

## Request Structure

### Headers
- `Company-Id` (UUID): Required header identifying your company
- `Mid` (UUID): Optional header for specific acquiring bank connection

### Request Body

The request accepts a JSON payload with the following structure:

```json
{
  "transaction": {
    "order_id": "string",
    "amount": "number",
    "currency": "string",
    "custom_data": "string",
    "custom1": "string",
    "custom2": "string"
  },
  "payment": {
    "reference": "string"
  }
}
```

### Transaction Parameters

| Parameter | Type | Requirements | Details |
|-----------|------|--------------|---------|
| `order_id` | String | Required | Unique reference; 2-50 characters, pattern: `[\w\-]*` |
| `amount` | Number | Required | Total charge amount (float format) |
| `currency` | String | Required | ISO 4217 code (lowercase): aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar |
| `custom_data` | String | Optional | Base64 encoded custom data |
| `custom1` | String | Optional | Additional reference data; max 50 characters |
| `custom2` | String | Optional | Additional reference data; max 50 characters |

### Payment Parameters

| Parameter | Type | Details |
|-----------|------|---------|
| `reference` | String | Bank statement reference (1-18 characters, pattern: `[\w\- ]*`) |

## Response Format

### Success Response (201 Created)

```json
{
  "transaction_id": "string",
  "status": "string",
  "amount": "number",
  "currency": "string"
}
```

### Error Responses

**400 Bad Request** - Validation failure
**401 Unauthorized** - Authentication failure
**404 Not Found** - Resource not found

Error responses include:
- `status`: "error"
- `error_type`: validation, unauthorized, or conflict
- `title`: Human-readable error message
- `instance`: Where error occurred
- `invalid_parameters`: Array of failed parameters with reasons

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Key Notes

The documentation mentions "expand responses" and "pagination" are available features for related API operations. This endpoint is part of the Faster Payments section within the Acquired API suite.

---

<a id="create-login"></a>

## create-login

# Create an access_token (Login) - API Documentation

## Overview

The authentication endpoint generates bearer tokens required for API access. Access to the API is by Bearer Tokens, this request generates an `access_token` that can be used in the authorization header of each request.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/login`

### Request Body

The login endpoint requires application credentials in JSON format:

| Parameter | Type | Length | Required | Description |
|-----------|------|--------|----------|-------------|
| `app_id` | string | 8 chars | Yes | Unique ID assigned by Acquired.com to your application |
| `app_key` | string | 32 chars | Yes | Secret key used as part of the authentication process |

**Example Request:**
```json
{
  "app_id": "72693514",
  "app_key": "de0eef006b4c30440aeca8ca5ac58789"
}
```

### Successful Response (200 OK)

The endpoint returns authentication details:

| Field | Type | Description |
|-------|------|-------------|
| `token_type` | string | Details the token type returned (typically "Bearer") |
| `expires_in` | integer | Time in seconds until the `access_token` expires (example: 3600) |
| `access_token` | string | Unique token that can be used to authenticate and log into the API |

**Example Response:**
```json
{
  "token_type": "Bearer",
  "expires_in": 3600,
  "access_token": "eyJ0eXAiOiJKV1QiLC....."
}
```

### Error Responses

**400 Bad Request:** Invalid credentials or missing required parameters. Response includes validation details with parameter-specific error reasons.

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/login",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

**401 Unauthorized:** Authentication failed.

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/login"
}
```

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Usage Notes

- Generated tokens function as bearer tokens for subsequent API requests
- Must be included in authorization headers for authenticated endpoints throughout the platform
- Token format: `Authorization: Bearer {access_token}`

---

<a id="create-mandate"></a>

## create-mandate

# Create a Mandate (Open Banking / VRP)

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates`

This endpoint creates a mandate and returns the details needed to direct the customer to their bank for authorization.

## Authentication

**Type:** Bearer JWT Token

Required header: Authorization with Bearer token obtained from the login endpoint.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

### Body Schema

The request body accepts a JSON object with the following structure:

#### Core Transaction Details
- **mandate_reference** (string): Unique reference for the mandate
- **customer_id** (string, UUID): Unique identifier for the customer
- **frequency** (string): Payment frequency (e.g., monthly, weekly, one-time)
- **start_date** (string, date): When the mandate becomes active
- **end_date** (string, date): Optional end date for the mandate

#### Customer Information
- **first_name** (string): Customer's given name
- **last_name** (string): Customer's family name
- **email** (string): Customer email address
- **reference** (string): Your internal customer reference

#### Banking Details
- **iban** (string): International Bank Account Number
- **account_holder_name** (string): Name on the bank account
- **country_code** (string): ISO 3166 2-character code

### Example Request Body

```json
{
  "mandate_reference": "MANDATE-001",
  "customer_id": "uuid",
  "frequency": "monthly",
  "start_date": "2026-04-01",
  "end_date": "2027-04-01",
  "first_name": "John",
  "last_name": "Doe",
  "email": "john.doe@example.com",
  "reference": "CUST-001",
  "iban": "GB82WEST12345698765432",
  "account_holder_name": "John Doe",
  "country_code": "GB"
}
```

## Response Format

### Success Response (201 Created)

The successful response includes:
- **mandate_id** (string, UUID): Unique mandate identifier assigned by system
- **status** (string): Current mandate status
- **redirect_url** (string): URL to send customer for bank authorization
- **created** (string, date-time): Timestamp of creation
- **expires_at** (string, date-time): Mandate expiration time

```json
{
  "mandate_id": "uuid",
  "status": "pending_authorization",
  "redirect_url": "https://bank.example.com/authorize/...",
  "created": "2026-04-01T12:00:00Z",
  "expires_at": "2026-04-01T12:30:00Z"
}
```

### Error Responses

**400 Bad Request** - Validation failures including:
- Invalid parameter format
- Missing required fields
- Out-of-range values

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/mandates",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Authentication failures

**404 Not Found** - Resource doesn't exist

## Key Notes

The mandate creation process initiates open banking authorization. Customers must complete verification through their bank portal using the provided redirect URL. The system supports multiple payment frequencies and includes optional expiration dates for time-limited arrangements.

---

<a id="create-merchant-session"></a>

## create-merchant-session

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

---

<a id="create-payee"></a>

## create-payee

# Create a Payee - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}/payees`

The endpoint enables creation of new payees stored against customer profiles, returning a unique `payee_id` for subsequent payout operations.

## Authentication

- **Type:** Bearer Token or JWT
- **Scheme:** Bearer authentication required in request headers

## Request Parameters

### Path Parameters
- `customer_id` (required): UUID format identifier assigned by Acquired.com to the customer

### Request Body Schema

The request accepts a JSON payload with payee information. The API specification indicates support for payee creation but the specific field requirements are contained within the OpenAPI schema definition provided in the document.

## Response Format

### Success Response (201 Created)

The successful response includes:
- HTTP Status: 201
- Response headers including `Company-Id` and `Mid` (both UUID format)
- JSON body containing the created payee details including a unique `payee_id`

**Key field:**
- `payee_id`: "a unique identifier assigned by Acquired.com to the payee when created" - which can then be referenced in payout operations.

### Error Responses

**400 Bad Request:**
- `status`: "error"
- `error_type`: "validation"
- `title`: Validation failure message
- `invalid_parameters`: Array of parameter-specific errors

**401 Unauthorized:**
- `status`: "error"
- `error_type`: "unauthorized"
- `title`: Authentication failure notification
- `instance`: Error endpoint reference

**403 Forbidden:**
- Permission/access restrictions

**404 Not Found:**
- Returns when specified customer_id doesn't exist

**409 Conflict:**
- State conflicts during creation

## Headers

- `Company-Id`: UUID assigned by Acquired.com (recommended in header)
- `Authorization`: Bearer token required

## Related Operations

This endpoint is part of the Faster Payments suite, which includes:
- Process a payout (uses the `payee_id`)
- Create account
- List all payees
- Internal transfers

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python

---

<a id="create-payment-links"></a>

## create-payment-links

# Generate a link_id - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-links`

This endpoint creates a payment link containing order or payment details. The returned `link_id` can be appended to your environment URL for sharing with customers.

**Format:** `https://test-pay.acquired.com/v1/{link_id}` (test) or `https://pay.acquired.com/v1/{link_id}` (production)

---

## Authentication

- **Type:** Bearer Token
- **Alternative:** JWT
- **Header:** Authorization: Bearer {access_token}

---

## Base URL

- **Test:** `https://test-api.acquired.com/v1/payment-links`
- **Production:** `https://api.acquired.com/v1/payment-links`

---

## Request Body Schema

The request accepts a JSON object with the following structure:

### Transaction Object (Required)

| Parameter | Type | Description | Example |
|-----------|------|-------------|---------|
| `order_id` | string | Unique reference for the payment request | `1f1f2a61-5b68-4725-a0ce-9560514ec00b` |
| `amount` | number | Total amount to charge | `15.02` |
| `currency` | string | ISO 4217 currency code (lowercase) | `gbp` |
| `moto` | boolean | Set true for phone orders | `false` |
| `capture` | boolean | Set false to authorize only | `true` |
| `custom_data` | string | Base64 encoded custom data | `L3BheW1lbnQtbGlua3MgcGF5IGN1c3RvbV9kYXRh` |
| `custom1` | string | Additional customer/reference data | Max 50 chars |
| `custom2` | string | Additional customer/reference data | Max 50 chars |

**Supported Currencies:** aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

### Payment Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `card` | object | Card payment details |
| `create_card` | boolean | Create card for future use (default: false) |
| `reference` | string | Bank statement reference (max 18 chars) |

**Card Details:**
- `holder_name` (required): Cardholder name
- `number` (required): Card number
- `expiry_month` (required): Month (1-12)
- `expiry_year` (required): Year (22-32)
- `cvv` (required): 3-4 digit security code

### Customer Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `customer_id` | string (UUID) | Existing customer ID |
| `reference` | string | Your customer reference |
| `first_name` | string | Customer first name |
| `last_name` | string | Customer last name |
| `dob` | string (date) | Date of birth (YYYY-MM-DD) |
| `custom_data` | string | Base64 encoded custom data |
| `billing` | object | Billing address details |
| `shipping` | object | Shipping address details |

**Address Fields:**
- `line_1`: First address line
- `line_2`: Second address line
- `city`: City
- `state`: State code (US only, 3 chars)
- `postcode`: Postal code
- `country_code`: ISO 3166 2-character code
- `email`: Email address
- `phone`: Phone object with `country_code` and `number`

### 3D Secure Object (Optional)

| Parameter | Type | Description |
|-----------|------|-------------|
| `is_active` | boolean | Enable EMV 3-D Secure authentication |
| `challenge_preference` | string | challenge_mandated, challenge_preferred, no_challenge_requested, no_preference |
| `challenge_window_size` | string | full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400 |
| `contact_url` | string (URI) | Support page URL |
| `redirect_url` | string (URI) | Post-authentication redirect URL |
| `webhook_url` | string (URI) | Webhook notification endpoint |

---

## Response Codes

### 201 Created

Successful payment link generation. Response includes Company-Id and Mid headers.

### 400 Bad Request

Validation failed. Response includes:
- `status`: "error"
- `error_type`: "validation"
- `title`: Human-readable error message
- `instance`: Location of error
- `invalid_parameters`: Array of parameter errors with `parameter` and `reason`

### 401 Unauthorized

Authentication failed. Response includes:
- `status`: "error"
- `error_type`: "unauthorized"
- `title`: "Authentication with the API failed, please check your details and try again."
- `instance`: Endpoint path

### 403 Forbidden

Access denied to requested resource. Response includes error details with `error_type`: "forbidden"

---

## Request Headers

| Header | Type | Description |
|--------|------|-------------|
| `Company-Id` | UUID | Unique ID for your company (optional) |
| `Mid` | UUID | Acquiring bank merchant ID (optional) |
| `Authorization` | string | Bearer token for authentication |

---

## Important Notes

- **MCC 6012 Requirements:** Merchants in this category must provide `last_name` and `dob` for customers
- **3DS Recommendation:** Customer billing address and email are recommended for EMV 3DS transactions
- **AVS Checks:** Full address and postcode recommended
- **Custom Data:** All custom_data fields must be Base64 encoded
- **Pattern Restrictions:** Fields have specific character patterns; special characters must match defined regex patterns
- **Webhook vs Redirect:** When 3DS is active with webhook_url inside tds object, use redirect_url for post-authentication navigation

---

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python

---

<a id="create-payment"></a>

## create-payment

# Process a Payment - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments`

**Description:** "Submit a payment request into the API and receive a response. If you want to create a card for future use, submit the full card details within the card object and set the `create_card` parameter to `true`"

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization header required with Bearer token

## Required Headers

| Header | Type | Description |
|--------|------|-------------|
| Company-Id | UUID | Unique identifier assigned by Acquired.com for your company |
| Mid | UUID | Unique ID connecting to a specific acquiring bank |

## Request Body Schema

### Transaction Object (Required)

| Parameter | Type | Description | Required |
|-----------|------|-------------|----------|
| order_id | String | Unique reference for payment request (2-50 chars, alphanumeric with hyphens) | Yes |
| amount | Float | Total amount to charge | Yes |
| currency | String | ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar) | Yes |
| moto | Boolean | Set true for phone orders (default: false) | No |
| capture | Boolean | Set false to authorize only; true = sale (default: true) | No |
| custom_data | String | Base64 encoded custom data | No |
| custom1 | String | Additional internal reference (0-50 chars) | No |
| custom2 | String | Additional internal reference (0-50 chars) | No |

### Payment Object

**Card Object:**
- `holder_name` (String, 2-45 chars): Cardholder name (required)
- `number` (String): Full card number (required)
- `expiry_month` (Integer, 1-12): Expiration month (required)
- `expiry_year` (Integer, 22-32): Expiration year (required)
- `cvv` (String, 3-4 chars): Security code (required)

**Additional Parameters:**
- `create_card` (Boolean): Set true to save card for future use (default: false)
- `reference` (String, 1-18 chars): Card statement reference where supported

### Customer Object

The customer data structure includes:
- `customer_id` (UUID): Existing customer reference
- `reference` (String): Your unique customer identifier
- `first_name` (String, 0-22 chars)
- `last_name` (String, 0-22 chars) - *Required for MCC 6012 merchants*
- `dob` (Date): Date of birth - *Required for MCC 6012 merchants*
- `custom_data` (String): Base64 encoded data

**Billing Address:**
- `line_1` (0-50 chars): *Recommended for 3DS and AVS*
- `line_2` (0-50 chars)
- `city` (1-40 chars)
- `state` (0-3 chars): US state code only
- `postcode` (1-40 chars): *Recommended for MCC 6012; Required for 3DS/AVS*
- `country_code` (2 chars): ISO 3166 code - *Recommended for 3DS*

**Shipping Address:**
- `address_match` (Boolean): Auto-populate if same as billing
- Same structure as billing address

**Contact Information:**
- `email` (String): Email format - *Recommended for 3DS*
- `phone.country_code` (String, 1-3 digits)
- `phone.number` (String, 0-15 digits)

### 3D Secure (TDS) Object

| Parameter | Type | Values | Description |
|-----------|------|--------|-------------|
| is_active | Boolean | true/false | Enable EMV 3DS authentication |
| challenge_preference | String | challenge_mandated, challenge_preferred, no_challenge_requested, no_preference | Cardholder challenge preference |
| challenge_window_size | String | full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400 | Preferred window size |
| contact_url | URI | Max 256 chars | Link to support page |
| redirect_url | URI | Max 256 chars | Post-authentication redirect location |
| webhook_url | URI | Max 256 chars | Notification endpoint for auth process |

## Response Codes

**201 Created:** Successful payment submission
- Headers include Company-Id and Mid

**400 Bad Request:** Validation errors
- Returns error_type: "validation"
- Includes invalid_parameters array with parameter names and reasons

**401 Unauthorized:** Authentication failed
- error_type: "unauthorized"
- Message: "Authentication with the API failed, please check your details and try again"

## Response Schema Structure

Successful responses include transaction details, card information, customer data, and authorization results with relevant timestamps and status indicators.

## Key Notes

- Full card details required for processing
- "Recommended when processing EMV 3DS transactions" - address and email fields enhance security
- Base64 encoding used for custom_data fields
- Phone numbers follow international dialing code format
- Card expiry years range from 22-32 (representing 2022-2032)

---

<a id="create-payment-stored-card"></a>

## create-payment-stored-card

# Process a Payment (card_id) - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payments/reuse`
**Description:** "Process a payment using a stored card. Simply pass the `card_id` and the cvv value in your request instead of passing the full card details."

---

## Authentication

**Type:** Bearer Token (JWT)
**Header:** Authorization with Bearer token from `/login` endpoint

---

## Request Parameters

### Header Parameters
- **Company-Id** (optional): Unique identifier assigned by Acquired.com for your company (UUID format)
- **Mid** (optional): Unique identifier connecting to a specific acquiring bank (UUID format)

### Request Body Schema

The request accepts a JSON object with the following structure:

#### Transaction Object (Required)
- **order_id** (string, 2-50 chars): Unique reference for the payment request
- **amount** (number): Total amount to charge
- **currency** (string): ISO 4217 currency code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean, default: false): Set true for phone payments
- **capture** (boolean, default: true): Set false to authorize only
- **custom_data** (string): Base64 encoded custom data
- **custom1** (string, 0-50 chars): Additional customer/reference data
- **custom2** (string, 0-50 chars): Additional customer/reference data

#### Payment Object (Required)
- **card_id** (string, UUID): The unique identifier of the stored card
- **cvv** (string, 3-4 digits): Security code for the stored card
- **reference** (string, 1-18 chars): Optional text to appear on cardholder statement

#### Customer Object (Optional)
- **customer_id** (string, UUID): Unique customer identifier
- **reference** (string, 0-50 chars): Merchant's customer reference
- **first_name** (string, 0-22 chars): Customer first name
- **last_name** (string, 0-22 chars): Customer last name
- **dob** (string, date format YYYY-MM-DD): Date of birth (required for MCC 6012)
- **custom_data** (string): Base64 encoded custom data
- **billing** (object): Billing address details
- **shipping** (object): Shipping address details

#### Address Object (Billing/Shipping)
- **line_1** (string, 0-50 chars): First address line
- **line_2** (string, 0-50 chars): Second address line
- **city** (string, 1-40 chars): City name
- **state** (string, 0-3 chars): ISO 3166-2 state code (US only)
- **postcode** (string, 1-40 chars): Postal code
- **country_code** (string, 2 chars): ISO 3166 country code

#### Phone Object
- **country_code** (string, 1-3 digits): International dialing code
- **number** (string, 0-15 digits): Phone number

#### 3D Secure Object (Optional)
- **is_active** (boolean, default: false): Enable EMV 3DS authentication
- **challenge_preference** (string): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size** (string): full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- **contact_url** (string, max 256 chars): Support page URI
- **redirect_url** (string, max 256 chars): Post-authentication redirect URI
- **webhook_url** (string, max 256 chars): Webhook notification endpoint

---

## Response Codes

### 201 Created
Successful payment processed. Response includes transaction details, authorization codes, and card information.

### 400 Bad Request
Validation error. Response includes error type and invalid parameter details.

### 401 Unauthorized
Authentication failed. Check credentials.

### 404 Not Found
Resource not found (card_id may be invalid).

---

## Language/SDK Support

- Shell/cURL
- Node.js
- Ruby
- PHP
- Python

---

## Key Notes

- This endpoint reuses stored card data, reducing PCI compliance burden
- CVV must be provided for each transaction (not stored)
- Supports EMV 3D Secure authentication when enabled
- Custom data fields accept base64-encoded information
- Address information recommended for fraud prevention and EMV 3DS processing

---

<a id="create-payout"></a>

## create-payout

# Process a Payout - API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/pay-out`

This endpoint enables execution of payments to customers from an Acquired-managed account.

## Authentication

- **Type**: Bearer Token (JWT)
- **Header**: Authorization with access token from login endpoint

## Request Parameters

### Headers
- `Company-Id` (uuid): Unique identifier for your company
- `Mid` (uuid, optional): Acquiring bank connection identifier

### Request Body Schema

The request accepts a JSON payload with the following structure:

```json
{
  "payee": {
    "payee_id": "uuid",
    "account_number": "string",
    "sort_code": "string"
  },
  "transaction": {
    "order_id": "string (2-50 chars)",
    "amount": "number (float)",
    "currency": "enum (gbp, eur, usd, etc.)",
    "custom_data": "string (base64 encoded)"
  },
  "reference": "string (max 18 chars)"
}
```

> **Note:** You can provide either `payee_id` OR `account_number` + `sort_code` in the payee object.

### Core Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `order_id` | string | Unique reference for payout request |
| `amount` | number | Total amount to transfer |
| `currency` | enum | ISO 4217 currency code (lowercase) |
| `payee_id` | uuid | Existing payee identifier |
| `account_number` | string | Beneficiary bank account |
| `sort_code` | string | UK sort code for routing |
| `reference` | string | Statement reference (max 18 chars) |

## Response Codes

### Success Response (201 Created)

```json
{
  "transaction_id": "string (uuid)",
  "status": "pending|processed|failed",
  "payout": {
    "payee_id": "uuid",
    "amount": "number",
    "currency": "string"
  }
}
```

### Error Responses

**400 Bad Request**: "Your request parameters did not pass our validation."
- Returns `invalid_parameters` array with detailed field-level errors

**401 Unauthorized**: "Authentication with the API failed, please check your details and try again."

**404 Not Found**: Payee or resource not located

## Supported Currencies

AED, AUD, CAD, CHF, CNY, DKK, EUR, GBP, HKD, JPY, MXN, SEK, USD, ZAR

## Related Endpoints

- **Create a Payee** (POST `/payees`): Register beneficiary before payout
- **Retrieve Transaction** (GET `/transactions/{id}`): Check payout status
- **List Payees** (GET `/payees`): View existing beneficiaries

## Notes

- Payee must exist before processing payout (or provide account_number + sort_code directly)
- Reference field appears on beneficiary's bank statement where supported

---

<a id="create-recurring-payment"></a>

## create-recurring-payment

# Recurring Payments API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments/recurring`

This endpoint enables "charging customers on a regular basis, i.e. for subscriptions or credit-based services."

## Authentication

- **Type**: Bearer Token (JWT)
- **Method**: Include access token in Authorization header
- **Token Generation**: Use `/login` endpoint with `app_id` and `app_key`

## Required Headers

| Header | Description | Type |
|--------|-------------|------|
| `Company-Id` | Unique identifier for your company | UUID |
| `Mid` | Unique ID connecting to acquiring bank | UUID (optional) |

## Request Body Structure

### Transaction Object
- **order_id** (string, 2-50 chars): Your unique reference for payment
- **amount** (float): Total charge amount
- **currency** (enum): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean): Set true for phone orders
- **capture** (boolean): Default true; set false for auth-only
- **custom_data** (string): Base64 encoded custom metadata
- **custom1, custom2** (string): Additional reference fields

### Payment Object
- **card**: Card details (holder_name, number, expiry_month, expiry_year, cvv)
- **create_card** (boolean): Save card for future use
- **reference** (string, 1-18 chars): Bank statement descriptor

### Customer Object
Complete customer information including:
- **customer_id** or create inline with first_name, last_name, dob
- **Billing address**: line_1, line_2, city, state, postcode, country_code
- **Shipping address**: Optional; can mirror billing
- **Email and phone**: Recommended for 3DS transactions

### TDS (3-D Secure) Object
- **is_active** (boolean): Enable EMV 3DS authentication
- **challenge_preference**: challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size**: full_screen, windowed_250x400, windowed_390x400, windowed_500x600, windowed_600x400
- **contact_url**: Support page link
- **redirect_url**: Post-authentication redirect destination
- **webhook_url**: Notification endpoint for auth process updates

## Response Format

**Success (201 Created)**: Returns transaction object with:
- Transaction ID
- Status (pending, success, failed)
- Authorization details
- Card information (masked)

**Error Responses**:
- **400**: Validation failure - returns invalid_parameters array
- **401**: Authentication failed
- **403**: Forbidden/insufficient permissions
- **409**: Conflict (e.g., duplicate reference)

## Language Support

Supported: Shell, Node, Ruby, PHP, Python

## Key Notes

- Recurring payments link transactions to stored customer records
- All transactions associate with a `customer_id`
- Address data recommended for fraud prevention and 3DS
- Phone number format: country_code + number (max 15 digits)
- Date of birth required for MCC 6012 merchants
- Custom data must be Base64 encoded

---

<a id="create-refund"></a>

## create-refund

# Process a Refund

## Overview

Process a refund for a specific payment. Append the required `transaction_id` to the URL and enter the amount to be refunded in the body of the request.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/refund`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/refund`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/refund`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | The unique identifier for the transaction being refunded |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Authorization` | string | Bearer token for API authentication (required) |
| `Company-Id` | string (UUID) | Unique ID assigned by Acquired for your company (optional) |

### Request Body Schema

The endpoint accepts JSON with the following structure:

```json
{
  "amount": 15.02,
  "reason": "customer_request",
  "metadata": {}
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | Yes | The refund amount in the transaction's original currency |
| `reason` | string | No | Reason code for the refund |
| `metadata` | object | No | Additional custom data for the refund |

## Important Notes

- Refunds can typically only be processed against captured transactions
- Partial refunds are generally supported if the remaining balance permits
- The refund amount must not exceed the original transaction amount

## Response Schemas

### 201 Created (Success)

Successful refund processing returns:

```json
{
  "status": "success",
  "refund_id": "string (uuid)",
  "transaction_id": "string (uuid)",
  "amount": 15.02,
  "currency": "gbp",
  "created": "2024-01-15T10:30:00Z",
  "reason": "customer_request"
}
```

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/refund",
  "invalid_parameters": [
    {
      "parameter": "amount",
      "reason": "Amount exceeds the original transaction amount"
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
  "instance": "/v1/transactions/{transaction_id}/refund"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/refund"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="create-reversal"></a>

## create-reversal

# Process a Reversal

## Overview

This endpoint allows you to automatically determine whether a payment should be voided or refunded based on settlement status and acquiring bank rules. The system supports both full and partial reversals.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/reversal`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/reversal`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/reversal`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Unique identifier for the transaction being reversed |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

### Request Body Schema

The API accepts a JSON payload with the following structure:

```json
{
  "amount": 15.02,
  "reason": "customer_request",
  "custom_data": "base64encodedstring",
  "custom1": "reference_value_1",
  "custom2": "reference_value_2"
}
```

### Field Definitions

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `amount` | number (float) | No | The reversal amount. If not provided, processes a **full reversal** |
| `reason` | string | No | Reason code for the reversal |
| `custom_data` | string | No | Base64 encoded string for custom metadata |
| `custom1` | string | No | Additional reference data (max 50 characters, alphanumeric and special characters) |
| `custom2` | string | No | Additional reference data (max 50 characters, alphanumeric and special characters) |

## Key Features

- **Automatic Determination:** The system intelligently decides between void and refund operations based on settlement status
- **Full & Partial Support:** Process complete or partial transaction reversals
- **Settlement Aware:** Takes into account current settlement status of transactions
- **Bank Rule Compliance:** Adheres to acquiring bank-specific requirements

## Usage Notes

- Reversals can typically only be processed on recent transactions
- The specific reversal type (void vs. refund) depends on settlement timing
- Partial reversals require an explicit `amount` parameter
- Full reversals process when no `amount` is specified

## Response Schemas

### 201 Created (Success)

Successful reversal processing returns:

```json
{
  "transaction_id": "string (uuid)",
  "status": "string",
  "reversal_type": "string",
  "amount": 15.02,
  "currency": "gbp",
  "created": "2024-01-15T10:30:00Z",
  "reason": "customer_request"
}
```

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/transactions/{transaction_id}/reversal",
  "invalid_parameters": [
    {
      "parameter": "amount",
      "reason": "Amount exceeds the original transaction amount"
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
  "instance": "/v1/transactions/{transaction_id}/reversal"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/reversal"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="create-session-id"></a>

## create-session-id

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

---

<a id="create-single-immediate-payment"></a>

## create-single-immediate-payment

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

---

<a id="create-variable-recurring-payment"></a>

## create-variable-recurring-payment

# Initiate a Variable Recurring Payment

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/open-banking/vrps`

This endpoint enables the initiation of a variable recurring payment against an established mandate.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Structure

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

### Body Schema

The API accepts JSON payloads with the following components:

#### Core Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | The unique identifier of the mandate authorizing the recurring payment |
| `amount` | number | Yes | The payment amount to be processed |
| `currency` | string | Yes | ISO 4217 currency code (lowercase) |
| `description` | string | No | Payment description for record-keeping |
| `reference` | string | No | Your unique reference identifier for this transaction |

### Example Request Body

```json
{
  "mandate_id": "550e8400-e29b-41d4-a716-446655440000",
  "amount": 25.99,
  "currency": "gbp",
  "description": "Monthly subscription payment",
  "reference": "PAY-2026-001"
}
```

## Response Codes

### Success Response (201 Created)

Returns transaction details including:
- `transaction_id`: Unique identifier for the payment
- `mandate_id`: Associated mandate
- `status`: Current payment status
- `amount`: Amount processed
- `currency`: Currency used
- `created`: Timestamp of creation

### Error Responses

**400 Bad Request** - Invalid parameters or missing required fields

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/vrps",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized** - Invalid or expired access token

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/open-banking/vrps"
}
```

**404 Not Found** - Mandate ID does not exist or is invalid

## Related Endpoints

Within the Variable Recurring Payments section:
- List supported banks: GET `/open-banking/supported-banks`
- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- List VRPs: GET `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`

## Language Support

Code examples available in: Shell, Node, Ruby, PHP, Python

## Additional Resources

Refer to the Fundamentals section for guidance on handling errors, pagination, and expanding response data.

---

<a id="create-void"></a>

## create-void

# Process a Void

## Overview

Process a cancellation of a transaction before it settles through a consumer's account. This operation differs from refunds, which occur post-settlement.

## Endpoint Details

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/void`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}/void`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}/void`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Request Parameters

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Unique identifier for the transaction to be voided |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for API authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to specific acquiring bank |

## Key Notes

- Voids cancel transactions **before settlement**
- This operation differs from refunds, which occur **post-settlement**
- The transaction must be eligible for voiding based on its current status
- Once a transaction has been settled, it cannot be voided (use refund instead)

## Response Structure

Upon successful processing, the API returns transaction void details including:

- Transaction status updates
- Void confirmation details
- Timestamps for the operation

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Void processed successfully |
| 400 | Bad Request - Validation errors in request |
| 401 | Unauthorized - Authentication failed |
| 404 | Not Found - Transaction not found |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions/{transaction_id}/void",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Explanation of validation failure"
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
  "instance": "/v1/transactions/{transaction_id}/void"
}
```

### 404 Not Found

```json
{
  "status": "error",
  "error_type": "not_found",
  "title": "The requested resource could not be found.",
  "instance": "/v1/transactions/{transaction_id}/void"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="customers-3"></a>

## customers-3

# Customers - API Documentation

## Overview

The Customers endpoints allow you to create, retrieve, list, and update customer records within the Acquired API system. Everything can be linked back to a `customer_id`. All transactions, beneficiaries, cards etc. are associated to a customer.

Creating a customer at process initiation allows Acquired to manage your customers for you, establishing the foundation for all subsequent API operations.

## Endpoints

| Method | URL | Description |
|--------|-----|-------------|
| POST | `/v1/customers` | Create a customer |
| GET | `/v1/customers` | List all customers |
| GET | `/v1/customers/{customer_id}` | Retrieve a customer |
| PUT | `/v1/customers/{customer_id}` | Update a customer |
| GET | `/v1/customers/{customer_id}/cards` | Get customer cards |

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** `Authorization: Bearer {access_token}`

## Customer Object Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `customer_id` | UUID | Yes | Unique identifier assigned by Acquired.com |
| `company_id` | UUID | Yes | Unique company identifier assigned by Acquired |
| `reference` | string (0-50) | No | Your unique customer reference |
| `first_name` | string (0-22) | No | Customer's first name |
| `last_name` | string (0-22) | No | Customer's last name (required for MCC 6012 merchants) |
| `dob` | date (YYYY-MM-DD) | No | Customer's date of birth (required for MCC 6012) |
| `custom_data` | string | No | Base64-encoded custom data |
| `billing` | object | No | Billing address and contact details |
| `shipping` | object | No | Shipping address and contact details |
| `created` | date-time | Yes | Record creation timestamp |
| `last_updated` | date-time | Yes | Last modification timestamp |

### Billing/Shipping Object Structure

#### Address Object
| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `line_1` | string | 0-50 chars | Primary address line |
| `line_2` | string | 0-50 chars | Secondary address line |
| `city` | string | 1-40 chars | Municipality |
| `state` | string | 0-3 chars | ISO 3166-2 state code (US only) |
| `postcode` | string | 1-40 chars | Postal code |
| `country_code` | string | 2 chars | ISO 3166 country code |

#### Contact Information
| Property | Type | Description |
|----------|------|-------------|
| `email` | string | Email address |
| `phone.country_code` | string (1-3 digits) | International dialing prefix |
| `phone.number` | string (0-15 digits) | Telephone number |

#### Shipping-specific
| Property | Type | Description |
|----------|------|-------------|
| `address_match` | boolean | Set to true to replicate billing address to shipping |

## Key Notes

- All parameters marked as optional can be omitted; unspecified fields remain unchanged during updates
- The `billing.address` is recommended for 3D Secure transactions and Address Verification Service (AVS) checks
- Email address is encouraged when processing EMV 3DS authentication
- Pattern validation enforces specific character sets (alphanumeric, hyphens, periods, commas for reference)
- Phone numbers accept only numeric characters
- Last name and date of birth are required for MCC 6012 merchants

---

<a id="direct-debit-1"></a>

## direct-debit-1

# Direct Debit - API Documentation

## Overview

**Section:** Direct Debit Payment Methods

The Direct Debit section of the Acquired.com API provides endpoints for managing Direct Debit mandates, collections, cancellations, and retries.

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/mandates`

The first step in the Direct Debit process involves creating a mandate. This must be established against a customer's bank account before Direct Debit collections can proceed.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required Header:** Authorization with access token

## Request Parameters

The request body should contain:

- **customer_id** (string, UUID): Unique identifier for the customer
- **bank_account** (object): Bank account details including:
  - Account holder name
  - Account number
  - Sort code (for UK accounts)
  - IBAN (for international accounts)
- **mandate_reference** (string): Your unique reference for the mandate
- **scheme** (string): Payment scheme (typically "sepa" or "bacs")

## Response Structure

**Success Response (201 Created):**

```json
{
  "mandate_id": "uuid",
  "customer_id": "uuid",
  "status": "pending_approval",
  "mandate_reference": "string",
  "created": "date-time"
}
```

## HTTP Status Codes

- **201 Created:** Mandate successfully created
- **400 Bad Request:** Invalid parameters or validation failure
- **401 Unauthorized:** Authentication failed
- **404 Not Found:** Customer or resource not found

## Error Response Format

Failed requests return error details with:
- `status`: "error"
- `error_type`: Type of error (validation, unauthorized, etc.)
- `title`: Human-readable error message
- `instance`: Where the error occurred
- `invalid_parameters`: Array of validation issues

## Related Endpoints

- **Create a collection:** POST `/collections`
- **Cancel a mandate:** POST `/mandates/{mandate_id}/cancellations`
- **Cancel a Direct Debit:** POST `/transactions/{transaction_id}/cancel`
- **Process a retry:** POST `/transactions/{transaction_id}/retry`
- **Retrieve mandate:** GET `/mandates/{mandate_id}`

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

---

<a id="dynamic-descriptor"></a>

## dynamic-descriptor

# Dynamic Descriptor Documentation

## Overview

The `reference` field functions as a dynamic billing descriptor that provides details about purchased products or services. This optional field is only utilized when the acquiring bank supports it. According to the documentation, "The issuing bank and card scheme mandate that this reference clearly demonstrates the company responsible for the transaction that has been processed."

A clear reference appearing on customer statements can help reduce disputes.

## Key Points

**Two descriptor approaches exist:**

1. **Static descriptor** - A fixed name or code configured during account setup with acquiring partners that appears consistently across all transactions

2. **Dynamic descriptor** - A variable reference that changes per transaction, typically combining a static value with dynamic information unique to each transaction

**Important note:** For Pay by Bank transactions, the `reference` field is mandatory.

## Reference Requirements

Your reference field must comply with these specifications:

- **Character limit:** Between 1-18 alphanumeric characters (spaces included)
- **Character types:** Alphanumeric only
- **Format:** Must be suitable for card statement display

## Additional Context

The documentation acknowledges that "on occasion, the issuer will not display it correctly," indicating that while implementation is straightforward, issuer interpretation may vary unpredictably. This field is used specifically within the `/payments` endpoint according to the page description.

---

<a id="expanding-responses"></a>

## expanding-responses

# Expanding Responses - Documentation

## Overview

The Acquired API permits you to request supplementary information through the expand request parameter. This feature is accessible on specific API endpoints and influences only that particular request's response.

Objects frequently contain IDs referencing related entities. For instance, a Subscription object includes associated Price and Product identifiers. Using the expand parameter allows you to retrieve these as complete objects rather than just references. Fields marked as **Expandable** in the API reference support this functionality.

You can expand multiple objects simultaneously by listing them in the expand property, separated by commas.

## Request Example

```bash
curl --request GET \
 --url 'https://api.acquired.com/v1/subscriptions/01KGM26PY4N5N05NECS36FHHFR?expand=price,product' \
 --header 'accept: application/json'
```

## Response Comparison

### Default Response

The standard response returns only object IDs:

```json
{
  "id": "01KGFNCDRCP55BG8SBXRJ774HB",
  "product": {
    "id": "01KGFNCDRC0AFCVJM24SD37ES8"
  },
  "price": {
    "id": "01KGFNCDRCKDFX4BWFZBGG6RZS"
  }
}
```

### Expanded Response

With expand parameters, related objects are fully populated:

```json
{
  "id": "01KGFNCDRCP55BG8SBXRJ774HB",
  "product": {
    "id": "01KGFNCDRC0AFCVJM24SD37ES8",
    "name": "Premium Tier",
    "description": "Access to all premium features and priority support",
    "created_at": "2026-02-02T17:11:09.836+00:00"
  },
  "price": {
    "id": "01KGFNCDRCKDFX4BWFZBGG6RZS",
    "product_id": "01KGFNCDRC0AFCVJM24SD37ES8",
    "usage_type": "fixed",
    "amount": 999,
    "cycle_details": {
      "interval": "month",
      "interval_count": 1
    },
    "currency": "GBP",
    "created_at": "2026-02-02T17:11:09.837+00:00"
  }
}
```

---

<a id="faster-payments"></a>

## faster-payments

# Faster Payments - API Documentation

## Overview

The Faster Payments section of the Acquired API provides endpoints for managing payees, processing payouts, managing accounts, and performing internal transfers.

## Endpoints

The Faster Payments section includes the following endpoints:

### Create a Payee
**POST** `https://test-api.acquired.com/v1/customers/{customer_id}/payees`

Create a new payee stored against a customer profile. Returns a unique `payee_id` for use in payout requests.

### Process a Payout
**POST** `https://test-api.acquired.com/v1/pay-out`

Execute payments to customers from an Acquired-managed account.

### Create an Account
**POST** `https://test-api.acquired.com/v1/accounts`

Create a new account for payment processing.

### List All Accounts
**GET** `https://test-api.acquired.com/v1/accounts`

Retrieve all accounts associated with your organisation.

### Retrieve Account Details
**GET** `https://test-api.acquired.com/v1/accounts/{mid}`

Retrieve account details using a unique merchant identifier.

### Internal Transfer
**POST** `https://test-api.acquired.com/v1/payments/internal-transfer`

Process an internal transfer between accounts.

### List All Payees
**GET** `https://test-api.acquired.com/v1/payees`

Returns a paginated list of Payee records created against the authenticated company.

## Related Sections

The API documentation includes comprehensive sections for:

- **Fundamentals:** Introduction, Statuses, HTTP Responses, Error Handling, Pagination
- **Authentication:** Access token generation
- **Customers:** CRUD operations for customer records
- **Cards:** Card management and storage
- **Payments:** Transaction processing
- **Transactions:** Refunds, voids, captures, reversals
- **Additional Features:** Direct Debit, Variable Recurring Payments, Reports

---

<a id="fetch-accounts-by-mid"></a>

## fetch-accounts-by-mid

# Retrieve Account Details - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/accounts/{mid}`

This endpoint retrieves account details using a unique merchant identifier (`mid`), which can be located in the Hub under Settings > Developers tab.

## Authentication

**Type:** Bearer Token (JWT)

Requests require an `access_token` obtained through the login endpoint.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `mid` | string | Unique identifier assigned by Acquired.com connecting to a specific acquiring bank |

## Request Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| Authorization | string | Yes | Bearer token for API authentication |

## Response Format

**Success Response (200 OK):**

Returns account details as JSON object containing merchant account information associated with the provided `mid`.

## Error Responses

**400 Bad Request**
- Status: "error"
- Error Type: "validation"
- Returns invalid parameters array with parameter names and reasons

**401 Unauthorized**
- Status: "error"
- Error Type: "unauthorized"
- Message: "Authentication with the API failed, please check your details and try again"

**404 Not Found**
- Status: "error"
- Error Type: Returned when account/mid not found

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Related Endpoints

This endpoint is part of the Faster Payments section, alongside:
- List all accounts (GET)
- Create an account (POST)
- Internal transfer (POST)
- List all payees (GET)

---

<a id="fetch-all-accounts"></a>

## fetch-all-accounts

# List All Accounts - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/accounts`

**Description:** "Use the GET accounts endpoint to retrieve all accounts associated with my organisation."

## Authentication

- **Type:** Bearer Token (JWT)
- **Required Header:** Authorization with Bearer token

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `offset` | integer (int32) | No | "The record to start the response on" (default: 0) |
| `limit` | integer (int32) | No | "A limit on the scope of values returned" (1-100 max) |
| `filter` | string | No | "Filter the parameters that you want to return within the response" |

## Response Structure

The response follows a paginated format with metadata and data arrays.

### Success Response (200 OK)

**Meta Object:**
- `count` (integer): "The number of records returned as part of the response"
- `offset` (integer): "The starting record number"
- `limit` (integer): "The maximum number of records that can be returned"
- `total` (integer): "The total number of records contained in the query response"
- `links` (array): Navigation links (rel: self, first, last, prev, next)

**Data Array:** Contains account objects with complete account details

## Error Responses

**400 Bad Request:** Validation errors with invalid_parameters array

**401 Unauthorized:** "Authentication with the API failed, please check your details and try again"

## Related Endpoints

This endpoint is part of the Faster Payments section alongside:
- Create an account (POST)
- Retrieve account details (GET by ID)
- Internal transfer (POST)
- List all payees (GET)

---

<a id="fetch-all-cards"></a>

## fetch-all-cards

# List All Cards - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/cards`

### Description

This allows you to request a list of all cards without having to specify a relating customer. However, you do have the ability to filter using the `customer_id` parameter.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Query Parameters

| Parameter | Type | Required | Default | Min | Max | Description |
|-----------|------|----------|---------|-----|-----|-------------|
| `offset` | integer | No | 0 | - | - | The record to start the response on |
| `limit` | integer | No | - | 1 | 100 | Maximum records returned per response |
| `filter` | string | No | - | - | - | Filter which parameters appear in response |
| `customer_id` | string (UUID) | No | - | - | - | Filter results to cards for a specific customer |

## Response Schema (200 OK)

### Meta Object

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      {
        "rel": "self",
        "href": "/v1/cards?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/cards?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/cards?offset=125",
        "title": "Last page"
      },
      {
        "rel": "prev",
        "href": "/v1/cards?offset=0",
        "title": "Previous page"
      },
      {
        "rel": "next",
        "href": "/v1/cards?offset=25",
        "title": "Next page"
      }
    ]
  }
}
```

### Card Data Object

Each card in the `data` array contains:

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | Unique card identifier assigned by Acquired.com |
| `customer_id` | UUID | Yes | Associated customer ID |
| `card.holder_name` | string | No | Cardholder name (2-45 chars) |
| `card.scheme` | enum | Yes | Card scheme: `visa`, `mastercard`, `amex` |
| `card.number` | string | Yes | Last 4 digits of the card number |
| `card.expiry_month` | integer | No | Expiry month (1-12) |
| `card.expiry_year` | integer | No | Expiry year (22-32) |
| `bin.number` | string | Yes | First 6 digits of card number |
| `bin.scheme` | enum | Yes | Card scheme |
| `bin.type` | enum | Yes | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `bin.product` | enum | Yes | Card product: `consumer`, `commercial` |
| `bin.card_level` | enum | Yes | Card tier (standard, gold, platinum, business, etc. - 40+ options) |
| `bin.issuer` | string | Yes | Bank name that issued the card |
| `bin.issuer_country` | string | Yes | Country the card was issued in |
| `bin.issuer_country_code` | string | Yes | ISO 3166 2-character country code |
| `bin.eea` | boolean | Yes | Whether the card was issued in the EEA |
| `bin.non_reloadable` | boolean | Yes | Whether the card is a non-refundable prepaid card |
| `source` | enum | Yes | How the card was created: `card`, `apple_pay`, `google_pay` |
| `created` | date-time | Yes | Creation timestamp |
| `last_updated` | date-time | Yes | Last modification timestamp |
| `is_active` | boolean | No | Whether the card is active (default: true) |

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/cards",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
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

## Key Features

- Supports pagination via offset/limit
- Allows filtering by customer ID via query parameter
- Returns comprehensive card details including BIN information
- Tracks card activation status
- Includes creation and modification timestamps
- Card numbers are masked, showing only last 4 digits

## Related Endpoints

- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
- **Get card details:** `GET /v1/cards/{card_id}`
- **Update card details:** `PUT /v1/cards/{card_id}`

---

<a id="fetch-all-customers"></a>

## fetch-all-customers

# List All Customers - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/customers`

### Description

Returns a list of all customers. Customers are returned by creation date, with the most recently created showing first.

By default, the endpoint returns 25 records unless modified via the `limit` query parameter. You can filter returned parameters using the `filter` query parameter.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization header required with access token

## Query Parameters

| Parameter | Type | Required | Default | Min | Max | Description |
|-----------|------|----------|---------|-----|-----|-------------|
| `offset` | integer | No | 0 | - | - | The record number to start the response on |
| `limit` | integer | No | 25 | 1 | 100 | Maximum records returned per response |
| `filter` | string | No | - | - | - | Specify which parameters to return in the response |
| `reference` | string | No | - | - | - | Filter results by customer reference value |

### Header Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `Company-Id` | UUID | No | Your unique company identifier |

## Response Schema (200 OK)

The response includes pagination metadata and an array of customer objects.

### Metadata Structure

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      {
        "rel": "self",
        "href": "/v1/customers?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/customers?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/customers?offset=125",
        "title": "Last page"
      },
      {
        "rel": "prev",
        "href": "/v1/customers?offset=0",
        "title": "Previous page"
      },
      {
        "rel": "next",
        "href": "/v1/customers?offset=25",
        "title": "Next page"
      }
    ]
  }
}
```

### Customer Object Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `company_id` | UUID | Yes | Unique company identifier assigned by Acquired |
| `reference` | string | No | Your unique customer reference (max 50 chars) |
| `first_name` | string | No | Customer's first name (max 22 chars) |
| `last_name` | string | No | Customer's last name (max 22 chars) |
| `dob` | date | No | Date of birth in YYYY-MM-DD format |
| `custom_data` | string | No | Base64-encoded custom data |
| `billing` | object | No | Billing address and contact details |
| `shipping` | object | No | Shipping address and contact details |
| `created` | date-time | Yes | Record creation timestamp |
| `last_updated` | date-time | Yes | Last modification timestamp |

### Address Object (Billing/Shipping)

```json
{
  "address": {
    "line_1": "string (max 50 chars)",
    "line_2": "string (max 50 chars)",
    "city": "string (max 40 chars)",
    "state": "string (max 3 chars - US only)",
    "postcode": "string (max 40 chars)",
    "country_code": "string (2-char ISO 3166 code)"
  },
  "email": "string (email format)",
  "phone": {
    "country_code": "string (1-3 digits)",
    "number": "string (max 15 digits)"
  }
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of issue"
    }
  ]
}
```

### 401 Unauthorized

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, check your details and try again.",
  "instance": "/v1/customers"
}
```

## Additional Notes

- Results are ordered by creation date, newest first
- The `filter` parameter allows selective field returns to optimize responses
- Pagination links include "self", "first", "last", "prev", and "next" relations
- All date fields use ISO 8601 format

---

<a id="fetch-all-payees"></a>

## fetch-all-payees

# List All Payees - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/payees`

**Description:** "Returns a paginated list of Payee records created against the authenticated company."

## Authentication

- **Type:** Bearer Token (JWT)
- **Credentials:** Required

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `offset` | integer | No | "The record to start the response on." (default: 0) |
| `limit` | integer | No | "A limit on the scope of values returned in the response." (min: 1, max: 100) |
| `filter` | string | No | "Filter the parameters that you want to return within the response." |

### Headers

| Header | Type | Description |
|--------|------|-------------|
| `Company-Id` | UUID | "Unique ID assigned by Acquired.com for your company." |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays:

**Meta Object:**
- `count` (integer): "The number of records returned as part of the response."
- `offset` (integer): "The starting record number."
- `limit` (integer): "The maximum number of records that can be returned."
- `total` (integer): "The total number of records contained in the query response."
- `links` (array): Navigation links with rel types: self, first, last, prev, next

**Data Array:** Contains payee objects with pagination support (default: 25 most recent)

## Key Features

- **Scope:** "Payees are scoped to the Company (not MID)."
- **Default Behavior:** "By default, the 25 most recently created Payees are returned."
- **Sorting:** Results returned by creation date (most recent first)

## Error Responses

### 400 Bad Request
Returns validation error with invalid_parameters array containing parameter names and reasons.

### 401 Unauthorized
Authentication failed - check API credentials.

## Related Endpoints

- Create a payee: POST `/payees`
- Process a payout: POST `/payouts`
- Create an account: POST `/accounts`
- List all accounts: GET `/accounts`
- Internal transfer: POST `/internal-transfer`

---

<a id="fetch-all-transactions"></a>

## fetch-all-transactions

# List All Transactions

## Overview

Retrieve all transactions with optional filtering capabilities. If no filters are provided, only transactions from the current day will be returned.

## Endpoint Details

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/transactions`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions`
- **Production Environment:** `https://api.acquired.com/v1/transactions`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Query Parameters

The endpoint supports filtering through the following query parameters:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `start_date` | string | No | Filter transactions after this date |
| `end_date` | string | No | Filter transactions before this date |
| `order_id` | string | No | Filter by specific order identifier |
| `status` | string | No | Filter by transaction status |
| `currency` | string | No | Filter by currency code (ISO 4217, e.g. `gbp`, `usd`, `eur`) |
| `reason` | string | No | Filter by transaction reason |
| `payment_method` | string | No | Filter by payment method used |
| `transaction_type` | string | No | Filter by type of transaction |
| `recurring_type` | string | No | Filter by recurring payment type |
| `charge_date` | string | No | Filter by charge date |
| `offset` | integer | No | Starting record number (default: 0) |
| `limit` | integer | No | Maximum records returned (1-100) |
| `filter` | string | No | Specify which response parameters to return |

## Default Behavior

**Important:** If no filters are provided, only transactions from the current day will be returned.

## Request Example

```
GET https://test-api.acquired.com/v1/transactions?start_date=2024-01-01&end_date=2024-01-31&status=success&limit=50
```

## Response Structure

Responses follow a paginated format including metadata and transaction data arrays, with navigation links for pagination control.

The response includes:
- Pagination metadata (total count, offset, limit)
- Array of transaction objects
- Navigation links for paginating through results

## Key Features

- Filtering flexibility across multiple transaction attributes
- Default daily transaction retrieval without filters
- Pagination support for managing large result sets via `offset` and `limit`
- Bearer token authentication required
- Response field filtering via the `filter` parameter

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Transactions successfully retrieved |
| 400 | Bad Request - Validation error in request parameters |
| 401 | Unauthorized - Authentication credentials invalid or missing |

## Error Response Format

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation",
  "instance": "/v1/transactions",
  "invalid_parameters": [
    {
      "parameter": "start_date",
      "reason": "Invalid date format"
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
  "instance": "/v1/transactions"
}
```

## Related Endpoints

- [Retrieve a transaction](transactions.md) (GET)
- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)

---

<a id="fetch-aspsp"></a>

## fetch-aspsp

# Get a List of Support Banks - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/aspsps`

## Description

This endpoint retrieves a roster of financial institutions that Acquired currently supports, including their names, logos, and available services. This data enables you to build custom account selection interfaces without relying on pre-built UI components.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the authorization header of your request.

## Request Details

### Base URL
- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

### Parameters

No required query parameters for this endpoint.

## Response Schema

The API returns a list of supported ASPSPs (Account Servicing Payment Service Providers), with each entry containing:

- **Bank identifiers** (name, BIC code)
- **Logo URLs** for visual representation
- **Supported payment services** (payment initiation, account information)
- **Country of operation**
- **Additional metadata** relevant for integration

## Use Cases

This endpoint supports scenarios where merchants want to:

- Display available banks to end-users during checkout
- Filter banks by country or supported services
- Implement custom bank selection flows
- Maintain updated lists of supported financial institutions

## Response Format

Responses are delivered in JSON format with pagination support when applicable, following the standard Acquired API response structure with metadata about result counts and available navigation links.

---

<a id="fetch-cards-by-id"></a>

## fetch-cards-by-id

# Get Card Details - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/cards/{card_id}`

The endpoint retrieves comprehensive details about a specific card by including the corresponding `card_id` in the URL path.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `card_id` | UUID | Yes | Unique ID assigned by Acquired.com for the card |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `filter` | string | No | Filter the parameters that you want to return within the response |

## Response Schema (200 OK)

The successful response returns a card details object with the following structure:

### Root Properties

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `card_id` | UUID | Yes | The unique ID assigned by Acquired.com to the card when it is created |
| `customer_id` | UUID | Yes | The unique ID assigned by Acquired.com to the customer when it is created |
| `card` | object | - | Card information object |
| `bin` | object | Yes | Bank Identification Number details |
| `source` | string | Yes | Identifies how the card was created: `card`, `apple_pay`, `google_pay` |
| `created` | ISO 8601 | Yes | The date and time that the card record was created |
| `last_updated` | ISO 8601 | Yes | The date and time the card record was last updated |
| `is_active` | boolean | No | Indicates whether a card is active or not (default: true) |

### Card Object Properties

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `holder_name` | string | 2-45 chars, alphabetic + spaces | The name of the cardholder |
| `scheme` | enum | - | Card scheme: `visa`, `mastercard`, `amex` |
| `number` | string | - | The last 4 digits of the card number |
| `expiry_month` | integer | 1-12 | The month the card expires in |
| `expiry_year` | integer | 22-32 | The year the card expires in |

### BIN Object Properties (Read-Only)

| Property | Type | Description |
|----------|------|-------------|
| `number` | string | The first 6 digits of the card number |
| `scheme` | enum | Card scheme: `visa`, `mastercard`, `amex` |
| `type` | enum | Card type: `charge`, `credit`, `debit`, `deferred_debit`, `prepaid` |
| `product` | enum | Card product category: `consumer`, `commercial` |
| `card_level` | enum | The level of the card: `standard`, `gold`, `platinum`, `business`, etc. |
| `issuer` | string | The name of the bank that issued the card |
| `issuer_country` | string | The name of the country the card was issued in |
| `issuer_country_code` | string | The ISO 3166 2-character country code the card was issued in |
| `eea` | boolean | If the card was issued in the EEA |
| `non_reloadable` | boolean | If the card is a non-refundable prepaid card |

## Example Response

```json
{
  "card_id": "550e8400-e29b-41d4-a716-446655440000",
  "customer_id": "660e8400-e29b-41d4-a716-446655440000",
  "card": {
    "holder_name": "E Johnson",
    "scheme": "visa",
    "number": "8710",
    "expiry_month": 10,
    "expiry_year": 29
  },
  "bin": {
    "number": "424242",
    "scheme": "visa",
    "type": "debit",
    "product": "consumer",
    "card_level": "standard",
    "issuer": "acquired.com",
    "issuer_country": "united kingdom",
    "issuer_country_code": "gb",
    "eea": true,
    "non_reloadable": false
  },
  "source": "card",
  "created": "2025-06-28T06:31:27.091Z",
  "last_updated": "2025-06-28T06:31:27.091Z",
  "is_active": true
}
```

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
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
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/cards/{card_id}"
}
```

## Notes

- The response masks sensitive card information, displaying only the last 4 digits
- BIN details are read-only and provided for informational purposes
- The `filter` query parameter allows customization of returned fields to optimize response payloads
- All date fields use ISO 8601 format

## Related Endpoints

- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`
- **List all cards:** `GET /v1/cards`
- **Update card details:** `PUT /v1/cards/{card_id}`

---

<a id="fetch-customer-by-id"></a>

## fetch-customer-by-id

# Retrieve a Customer - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}`

**Description:** Retrieves a customer using the unique `customer_id`. This identifier can be found in the response after creating a new customer.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `customer_id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for the customer |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `filter` | string | No | Filter the parameters that you want to return within the response |

## Response Schema (200 OK)

The successful response returns a customer object with the following properties:

| Property | Type | Read-Only | Description |
|----------|------|-----------|-------------|
| `company_id` | string (UUID) | Yes | Unique ID assigned by Acquired.com to the company |
| `reference` | string | No | Unique reference assigned by you (max 50 chars) |
| `first_name` | string | No | Customer's first name (max 22 chars, alphabetic only) |
| `last_name` | string | No | Customer's last name (max 22 chars, alphabetic only). Required for MCC 6012 merchants. |
| `dob` | string (date) | No | Customer's date of birth. Required for MCC 6012 merchants. Format: YYYY-MM-DD |
| `custom_data` | string | No | Base64 encoded custom data |
| `billing` | object | No | Billing address and contact information |
| `shipping` | object | No | Shipping address and contact information |
| `created` | string (date-time) | Yes | Creation timestamp |
| `last_updated` | string (date-time) | Yes | Last update timestamp |

### Billing/Shipping Object Structure

| Property | Type | Description |
|----------|------|-------------|
| `address` | object | Address details |
| `email` | string | Email address (email format) |
| `phone` | object | Phone contact details |

### Address Object

| Property | Type | Constraints | Description |
|----------|------|-------------|-------------|
| `line_1` | string | Max 50 chars | Primary address line. Recommended when processing EMV 3DS transactions. |
| `line_2` | string | Max 50 chars | Secondary address line (optional) |
| `city` | string | Max 40 chars | Municipality (required) |
| `state` | string | Max 3 chars | ISO 3166-2 state code (US orders only) |
| `postcode` | string | Max 40 chars | Postal code (required). Recommended for MCC 6012 merchants. |
| `country_code` | string | 2 chars | ISO 3166 2-character country code. Recommended when processing EMV 3DS transactions. |

### Phone Object

| Property | Type | Description |
|----------|------|-------------|
| `country_code` | string | International dialing code (1-3 digits) |
| `number` | string | Phone number (max 15 digits) |

## Error Responses

### 400 Bad Request

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation",
  "instance": "/v1/customers/{customer_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details about the validation error"
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
  "instance": "/v1/customers/{customer_id}"
}
```

## Related Endpoints

- **Create a customer:** `POST /v1/customers`
- **List all customers:** `GET /v1/customers`
- **Update a customer:** `PUT /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`

## Implementation Notes

- The endpoint returns read-only fields for `company_id`, `created`, and `last_updated`
- Billing address `line_1` and `country_code` are recommended when processing EMV 3DS transactions
- Postcode and last_name are recommended for MCC 6012 merchants
- All date fields follow ISO 8601 format

---

<a id="fetch-customer-cards"></a>

## fetch-customer-cards

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

---

<a id="fetch-mandate-by-id"></a>

## fetch-mandate-by-id

# Retrieve a Mandate (Direct Debit)

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/mandates/{mandate_id}`

**Description:** This endpoint allows you to fetch information associated with a specific mandate by providing the necessary `mandate_id` parameter in your request.

## Authentication

**Type:** Bearer Token (JWT)

Required for all requests to this endpoint.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the mandate |

## Request Example

```
GET https://test-api.acquired.com/v1/mandates/{mandate_id}
Authorization: Bearer <access_token>
Company-Id: <company_uuid>
```

## Response Format

The endpoint returns mandate-related data in JSON format upon successful retrieval.

### Success Response (200 OK)

Returns the full mandate object including:
- `mandate_id`: Unique identifier
- `customer_id`: Associated customer
- `status`: Current mandate status
- `mandate_reference`: Your reference
- `bank_account`: Account details
- `scheme`: Payment scheme
- `created`: Creation timestamp
- `updated`: Last update timestamp

## HTTP Status Codes

- **200 OK** - Successful retrieval of mandate information
- **400 Bad Request** - Invalid request parameters
- **401 Unauthorized** - Authentication failed or invalid credentials
- **404 Not Found** - The specified mandate does not exist

## Error Response Structure

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error message",
  "instance": "/v1/mandates/{mandate_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "error description"
    }
  ]
}
```

## Related Operations

This endpoint is part of the Direct Debit section and works alongside:
- Create a mandate: POST `/mandates`
- Create a collection: POST `/payments/collections`
- Cancel a mandate: POST `/mandates/{mandate_id}/cancel`
- Cancel a Direct Debit: POST `/transactions/{transaction_id}/cancel`
- Process a retry: POST `/transactions/{transaction_id}/retry`

---

<a id="fetch-mandate"></a>

## fetch-mandate

# Retrieve an Open Banking Mandate

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates/{mandate_id}`

This endpoint retrieves an open banking mandate using a unique identifier. The `mandate_id` can be obtained through the `mandate_active` webhook or after a customer authenticates via Hosted Checkout.

## Authentication

**Type:** Bearer Token (JWT)

Requests require valid bearer token authentication passed in the authorization header.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## URL Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `mandate_id` | string (UUID) | Yes | Unique identifier for the open banking mandate |

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Response

### Success Response (200 OK)

The successful response returns the complete mandate details in JSON format. The response structure includes:

- Mandate identification and status information
- Customer authentication details
- Bank account information
- Creation and update timestamps
- Mandate-specific configuration (frequency, start/end dates)

### Error Responses

| Status | Error Type | Description |
|--------|-----------|-------------|
| 400 | Validation Error | Request parameters failed validation |
| 401 | Unauthorized | Authentication credentials invalid or missing |
| 404 | Not Found | Mandate identifier does not exist |

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error explanation",
  "instance": "/v1/open-banking/mandates/{mandate_id}"
}
```

## Usage Context

This endpoint is part of the Variable Recurring Payments section within the Acquired API. It functions as a retrieval mechanism for mandates created through open banking connections, enabling merchants to verify mandate status and details without reinitiating the authentication flow.

## Related Endpoints

- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- Initiate a VRP: POST `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`

---

<a id="fetch-recon-by-id"></a>

## fetch-recon-by-id

# Retrieve a Reconciliation Report

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/reports/reconciliations/{reconciliation_id}`

This endpoint allows you to fetch a specific reconciliation report by providing the `reconciliation_id` in the URL path.

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `reconciliation_id` | string (UUID) | Yes | Unique identifier for the reconciliation report |

## Request Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID for your company |

## Response Format

### Success Response (200 OK)

The response returns a specific reconciliation report object containing:
- `report_id`: Unique identifier
- `company_id`: Associated company
- `mid`: Merchant ID
- `report_date`: Date of the report
- `period_start`: Start of reporting period
- `period_end`: End of reporting period
- `total_transactions`: Number of transactions
- `total_amount`: Total settlement amount
- `currency`: Currency code
- `status`: Report status
- `created`: Creation timestamp
- `last_updated`: Last update timestamp

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/reports/reconciliations/{reconciliation_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/reports/reconciliations/{reconciliation_id}"
}
```

**404 Not Found:** The specified reconciliation report does not exist.

## Implementation Notes

- The reconciliation report retrieval is a read-only operation
- The `reconciliation_id` parameter must be a valid UUID format
- Must correspond to an existing reconciliation report in your account

## Related Endpoints

- List all Reconciliation Reports: GET `/reports/reconciliations`

---

<a id="fetch-recon-reports"></a>

## fetch-recon-reports

# List all Reconciliation Reports

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/reports/reconciliations`

**Description:** Retrieve a paginated list of available reconciliation reports, querying by Company-Id, Mid, date range, and specific fields.

## Authentication

- **Type:** Bearer Token (JWT)
- **Header:** Authorization

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Header Parameters

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum records returned per response (min: 1, max: 100) |
| `filter` | string | No | - | Filter which parameters appear in the response |
| `mid` | string | No | - | Filter results by merchant ID |
| `date_from` | string (date) | No | - | Start date for report range |
| `date_to` | string (date) | No | - | End date for report range |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays:

```json
{
  "meta": {
    "count": 10,
    "offset": 0,
    "limit": 25,
    "total": 50,
    "links": [
      {
        "rel": "self",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/reports/reconciliations?offset=40",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/reports/reconciliations?offset=10",
        "title": "Next page"
      }
    ]
  },
  "data": [
    {
      "report_id": "uuid",
      "company_id": "uuid",
      "mid": "uuid",
      "report_date": "date-time",
      "period_start": "date-time",
      "period_end": "date-time",
      "total_transactions": 0,
      "total_amount": 0.00,
      "currency": "string",
      "status": "string",
      "created": "date-time",
      "last_updated": "date-time"
    }
  ]
}
```

### Metadata Object

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records in current response |
| `offset` | integer | Starting record number |
| `limit` | integer | Maximum records per page |
| `total` | integer | Total records matching query |
| `links` | array | Pagination navigation links (self, first, last, prev, next) |

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass validation.",
  "instance": "/v1/reports/reconciliations",
  "invalid_parameters": [
    {
      "parameter": "string",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please verify credentials.",
  "instance": "/v1/reports/reconciliations"
}
```

**403 Forbidden:** Insufficient permissions for requested resource.

## Related Endpoints

- Retrieve a Reconciliation Report: GET `/reports/reconciliations/{reconciliation_id}`

---

<a id="fetch-transaction-by-id"></a>

## fetch-transaction-by-id

# Retrieve a Transaction by ID

## Overview

The "Retrieve a transaction" endpoint is a GET request that allows you to fetch transaction details using a transaction ID returned from payment submissions. Every time you submit a payment request, including card, recurring payments, Google Pay, Apple Pay, you are returned a `transaction_id`. Append the `transaction_id` to the URL to retrieve the transaction details.

## Endpoint Details

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}`

## Authentication

**Type:** Bearer Token (JWT)

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

Available credential types:
- Bearer Token
- JWT

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the transaction |

## Request Example

```
GET https://test-api.acquired.com/v1/transactions/[transaction_id]
```

No request body is required.

## Response Structure

The response returns complete transaction details in JSON format including:

- Transaction status and type
- Amount and currency information
- Payment method details
- Customer information
- Timestamp data
- Authorization codes and references

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Transaction successfully retrieved |
| 400 | Bad Request - Validation error in request parameters |
| 401 | Unauthorized - Authentication credentials invalid or missing |
| 404 | Not Found - Transaction ID does not exist |

## Error Response Format

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error description",
  "instance": "/v1/transactions/{transaction_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Explanation of validation failure"
    }
  ]
}
```

## Supported Languages

Code examples available in:
- Shell
- Node
- Ruby
- PHP
- Python

## Related Endpoints

- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="handling-errors"></a>

## handling-errors

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

---

<a id="hosted-checkout"></a>

## hosted-checkout

# Generate a link_id - API Documentation

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/payment-links`

### Description
This endpoint creates a payment link containing order or payment details. The generated `link_id` can be appended to the appropriate environment URL and shared with customers. For example: `https://test-pay.acquired.com/v1/{link_id}` (remove "test" prefix in production).

## Authentication

**Type:** Bearer Token or JWT
**Required Header:** Authorization with valid access token

## Request Parameters

The request body accepts a JSON object with transaction and payment details:

### Transaction Object (Required)
- **order_id** (string, 2-50 chars): Unique reference for the payment request
- **amount** (number): Total charge amount
- **currency** (string): ISO 4217 code (aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar)
- **moto** (boolean, default: false): Set true for phone orders
- **capture** (boolean, default: true): Set false to authorize only
- **custom_data** (string): Base64-encoded custom data
- **custom1, custom2** (string): Additional reference fields (0-50 chars)

### Payment Object
- **card** (object): Card details (holder_name, number, expiry_month, expiry_year, cvv)
- **create_card** (boolean): Set true to store card for future use
- **reference** (string): Bank statement descriptor (1-18 chars)

### Customer Object
- **customer_id** (string, UUID): Unique customer identifier
- **reference** (string): Your customer reference
- **first_name, last_name** (string): Customer names
- **dob** (string, format: YYYY-MM-DD): Date of birth
- **billing/shipping** (object): Address and contact details

### TDS (3D Secure) Object
- **is_active** (boolean): Enable EMV 3DS authentication
- **challenge_preference** (string): challenge_mandated, challenge_preferred, no_challenge_requested, no_preference
- **challenge_window_size** (string): Window dimensions for challenge
- **contact_url** (string, URI): Support page link
- **redirect_url** (string, URI): Post-authentication redirect location
- **webhook_url** (string, URI): Notification endpoint

## Response Codes

**201 Created:** Successful link generation
**400 Bad Request:** Validation errors in parameters
**401 Unauthorized:** Authentication failure
**403 Forbidden:** Access denied to resource
**404 Not Found:** Resource not found
**409 Conflict:** Conflict with existing data

## Headers

- **Company-Id** (string, UUID): Required company identifier
- **Mid** (string, UUID): Merchant ID for specific acquiring bank

## Usage Notes

This endpoint generates shareable payment links for hosted checkout flows. The link includes all transaction and customer details, enabling secure payment processing through the Acquired platform. For production environments, replace "test-api" and "test-pay" with production URLs.

---

<a id="http-responses"></a>

## http-responses

# HTTP Responses Documentation

## Overview

"Whenever you send a request to the Acquired.com API you will receive a response in JSON format and a HTTP status code. The status code indicates the success or failure of a request to the server."

## HTTP Response Codes

The API uses the following standard HTTP status codes:

| Code | Name | Description | Title |
|------|------|-------------|-------|
| 200 | OK | Standard response for successful HTTP requests. Indicates that everything worked as expected. | N/A |
| 201 | Created | The request has been fulfilled, resulting in the creation of one or more new resources. | N/A |
| 400 | Bad Request | The Acquired.com API was unable to understand your request. This could be due to a syntax error. | "Your request parameters did not pass our validation." |
| 401 | Unauthorized | Authentication to the Acquired.com API failed resulting in an error response. This could be because you incorrectly entered your API keys or failed to enter them. | "Authentication with the API failed, please check your details and try again." |
| 403 | Forbidden | You do not have the required access to the requested resource. | "You do not have access to the requested resource, please check your details and try again." |
| 404 | Not Found | The requested resource was not found. | "Unable to find the requested resource, please check your request and try again." |
| 409 | Conflict | The request conflicts with another request. | "There was a conflict when trying to complete your request." |
| 500 | Internal Server Error | An internal server error occurred while processing your request. | "An error occurred, we are investigating the reason." |

## Successful Request Responses

"When your request is successful, you will receive either a `200 - OK` or `201 - Created` response."

### 200 - OK Example

**Request:** Updating a customer record
```json
{
  "last_name": "Jones"
}
```

**Response:**
```json
{
  "status": "Success"
}
```

### 201 - Created Example

**Request:** Creating a new card
```json
{
  "holder_name": "E Johnson",
  "scheme": "visa",
  "number": "4242424242424242",
  "expiry_month": 12,
  "expiry_year": 26,
  "cvv": "123"
}
```

**Response:**
```json
{
  "card_id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

---

<a id="introduction"></a>

## introduction

# Acquired.com API - Introduction Documentation

## Overview

The Acquired.com API is structured around REST principles, enabling seamless integration of online payment functionality into websites and applications.

## Key Features

**Testing Environment**: A dedicated testing environment is available for building and testing without impacting live data or live banking interactions.

**Authentication**: Upon account creation, developers receive Hub login credentials that provide access to:
- `app_id`
- `app_key`

**Portal Access**: The Hub serves as a management portal for reviewing and managing transactions.

## Getting Started Pathway

The documentation recommends this learning sequence:

1. **Fundamentals Section** - Covers core concepts including:
   - Authentication mechanisms
   - Error handling procedures
   - Status and reason codes
   - Pagination patterns
   - Testing methodologies

2. **API Endpoints** - Comprehensive documentation within the "Acquired.com API" section featuring:
   - Detailed endpoint specifications
   - Code examples for multiple languages
   - Request/response examples

3. **Implementation Guides** - Additional articles in the guides section provide practical implementation details

## Support Resources

"If you require access to our test environment, please contact our support team via our support portal" for account setup and environment access.

Additional resources include the Postman collection (available via "Run in Postman" button) and the Postman guide documentation.

## Documentation Structure

The API reference organizes endpoints across functional areas: Authentication, Customers, Cards, Payments, Transactions, Reports, and specialized payment methods (Pay by Bank, Direct Debit, Variable Recurring Payments).

---

<a id="issuer-response-codes"></a>

## issuer-response-codes

# Issuer Response Codes Documentation

## Overview

The Acquired API returns an `issuer_response_code` parameter in each card authorization response, representing the actual transaction result code from the customer's issuing bank.

## Complete Issuer Response Codes Table

| Code | Message | Description |
|------|---------|-------------|
| 00 | Approved | Transaction approved and completed successfully |
| 01 | Refer to card issuer | Cardholder should contact issuing bank; suggest alternate card |
| 02 | Refer to card issuer's special conditions | Problem with card number; customer should use alternate card or contact bank |
| 03 | Invalid merchant | Merchant details incorrect or facility non-functional |
| 04 | Capture / pick up card | Bank requested card retention; likely lost or stolen |
| 05 | Do not honour | Issuing bank unwilling to accept transaction |
| 06 | Error | Error occurred during card transaction |
| 07 | Pick up card special condition | Card issuer requests retention due to suspected counterfeit or stolen card |
| 09 | Request in progress | Request still being processed; await final response |
| 10 | Partial amount approved | Some card issuers support partial-approval authorization |
| 12 | Invalid transaction | Error processing card; verify payment configuration |
| 13 | Invalid amount/bad message edit | Check for negative amounts or incorrect symbols |
| 14 | Invalid card number/no such number | Incorrect or nonexistent card number; verify details |
| 15 | No such issuer | Card issuer doesn't exist; card should start with 3, 4, or 5 |
| 17 | Customer cancellation | Client cancelled transaction |
| 18 | Customer dispute | Customer filed dispute for transaction |
| 19 | Re-enter transaction | Processing error; attempt transaction again |
| 22 | Suspected malfunction | Issuing bank unresponsive; verify card info |
| 30 | Format error | Contact support to verify merchant account setup |
| 31 | Bank not supported by switch | Bank doesn't allow mail/telephone/fax/email/internet orders |
| 33 | Expired card | Credit card expired; request updated billing information |
| 34 | Suspected fraud | Bank declined due to suspected fraud; monitor transactions |
| 35 | Card acceptor call acquirer security | Bank requests card pickup; contact number on card |
| 36 | Restricted card pick up | Card restrictions preventing transaction processing |
| 37 | Contact acquirer security department | Bank declined and requested card retention; monitor for fraud |
| 40 | Requested function not supported | Card issuer doesn't allow this transaction type |
| 41 | Lost card | Card reported lost or stolen; don't retry |
| 42 | No universal account | Invalid account type for card number; use alternate card |
| 43 | Stolen card | Card reported stolen; don't retry and report attempt |
| 49 | The card was declined | Card not enabled for online transactions |
| 51 | Insufficient funds | Insufficient funds or would exceed credit limit |
| 54 | Expired card | Card expired and invalid |
| 56 | No card record | Card number doesn't exist; use separate card |
| 57 | Transaction not permitted by cardholder | Card cannot be used for this transaction type |
| 58 | Transaction not permitted by terminal | Card cannot be used for this transaction or merchant not properly configured |
| 59 | Suspected fraud | Transaction declined as fraudulent; monitor all transactions |
| 61 | Exceeds withdrawal amount limit | Transaction exceeds customer's card limit |
| 62 | Restricted card | Card invalid in region/country or doesn't support online payments |
| 63 | Security violation | Card issuer indicated security issue; use alternate payment method |
| 65 | Authentication required | PSD2 Strong Customer Authentication required; retry with 3-D Secure |
| 67 | Hard capture | Card suspected counterfeit; bank requested retention |
| 69, 70, 71, 72, 73, 74 | Error: contact card issuer | Card issuer indicated issue; customer should contact bank or use alternate method |
| 75 | Allowable number of PIN tries exceeded | New cardholder; card not properly unblocked |
| 78 | Error | Incorrect PIN entered too many times; retry or use alternate method |
| 80 | Credit issuer unavailable | Issuing bank unavailable; retry or use alternate payment method |
| 85 | No reason to decline | Processing with amount 0 and AUTH_ONLY should authorize successfully |
| 91 | Authorization Platform or issuer system inoperative | Problem contacting issuing bank; retry or use alternate card |
| 92 | Network problem | Card not found for routing; mostly for test cards |
| 93 | Transaction cannot be completed | Issuing bank won't allow transaction; use alternate method |
| 96 | System malfunction | Issuing bank unavailable; retry or use alternate payment method |
| 97, 98, 99 | Error | Decryption error; contact support |
| 1A | Additional customer authentication required | Authentication required; if PSD2 in scope without 3DS, retry with 3DS |
| R1 | Revocation of authorisation | Customer cancelled continuous authorization; contact bank or use alternate payment details |
| N7 | Declined card verification value (CVV2) | Incorrect or mismatched CVV in card-not-present transaction |

---

<a id="list-mandates"></a>

## list-mandates

# List Mandates (Open Banking / VRP)

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/mandates`

Returns a paginated list of mandates from the Acquired API's Variable Recurring Payments section.

## Authentication

**Type:** Bearer Token (JWT)

This endpoint requires Bearer token authentication via JWT credentials.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum number of records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter the parameters that you want to return within the response |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |

## Response Format

### Success Response (200 OK)

The response follows a paginated structure:

```json
{
  "meta": {
    "count": 10,
    "offset": 0,
    "limit": 25,
    "total": 50,
    "links": [
      {
        "rel": "self",
        "href": "/v1/open-banking/mandates?offset=0",
        "title": "Current page"
      },
      {
        "rel": "first",
        "href": "/v1/open-banking/mandates?offset=0",
        "title": "First page"
      },
      {
        "rel": "last",
        "href": "/v1/open-banking/mandates?offset=40",
        "title": "Last page"
      },
      {
        "rel": "next",
        "href": "/v1/open-banking/mandates?offset=10",
        "title": "Next page"
      }
    ]
  },
  "data": []
}
```

**Metadata Object:**
- `count` - Number of records returned
- `offset` - Starting record number
- `limit` - Maximum records that can be returned
- `total` - Total records in query response
- `links` - Array of navigation links (self, first, last, prev, next)

**Data Array:** Contains mandate objects with complete details

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/mandates",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/open-banking/mandates"
}
```

**404 Not Found:** Resource not found

## Related Endpoints

- Create a mandate: POST `/open-banking/mandates`
- Retrieve a mandate: GET `/open-banking/mandates/{mandate_id}`
- List supported banks: GET `/open-banking/supported-banks`

---

<a id="list-supported-banks"></a>

## list-supported-banks

# List Supported Banks

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/supported-banks`

**Description:** Returns a list of banks supported for Open Banking and variable recurring payment journeys.

## Authentication

**Type:** Bearer Token (JWT)

The endpoint requires authentication via Bearer token included in the authorization header of each request.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Details

### Parameters

No query parameters are required for this endpoint.

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID assigned by Acquired.com for your company |

## Response Structure

### Success Response (200 OK)

The response returns a paginated list of supported banks for open banking operations and variable recurring payment journeys.

**Meta Object:**
- `count` (integer): Number of records in response
- `offset` (integer): Starting record position
- `limit` (integer): Maximum records returnable
- `total` (integer): Complete record count in query
- `links` (array): Pagination navigation links

**Data Array:**
Contains bank objects with institution-specific information for open banking integrations.

```json
{
  "meta": {
    "count": 0,
    "offset": 0,
    "limit": 0,
    "total": 0,
    "links": [
      {
        "rel": "first|last|prev|next|self",
        "href": "/v1/open-banking/supported-banks?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Request parameters failed validation",
  "instance": "/v1/open-banking/supported-banks",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "error description"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/open-banking/supported-banks"
}
```

## HTTP Response Codes

| Status | Description |
|--------|-------------|
| 200 | Successful retrieval of supported banks |
| 400 | Invalid request parameters |
| 401 | Authentication failure - invalid credentials |

## Implementation Notes

This endpoint is part of the "Pay by Bank" / Variable Recurring Payments API section and facilitates variable recurring payment mandates by enabling selection from an approved financial institution roster.

---

<a id="list-variable-recurring-payments"></a>

## list-variable-recurring-payments

# List Variable Recurring Payments

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/open-banking/vrps`
**Description:** Returns a paginated list of variable recurring payments.

## Authentication

**Type:** Bearer Token (JWT)
**Required Header:** Authorization with valid access token

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Request Parameters

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum number of records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter specific parameters to return in response |

### Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Response Structure

### Success Response (200 OK)

The response follows a paginated format with metadata and data arrays.

```json
{
  "meta": {
    "count": 0,
    "offset": 0,
    "limit": 0,
    "total": 0,
    "links": [
      {
        "rel": "first|last|prev|next|self",
        "href": "/v1/open-banking/vrps?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

### Metadata Object

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records in current response |
| `offset` | integer | Starting position of records |
| `limit` | integer | Maximum records per request |
| `total` | integer | Total records available |
| `links` | array | Navigation links for pagination |

### Link Relations

- `self` - Current resource link
- `first` - First page link
- `last` - Last page link
- `prev` - Previous page link
- `next` - Next page link

### Error Responses

**400 Bad Request:**

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/open-banking/vrps",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Detailed error description"
    }
  ]
}
```

**401 Unauthorized:**

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/open-banking/vrps"
}
```

## Pagination Details

The API implements cursor-based pagination through `offset` and `limit` parameters. The response metadata includes:

- **count:** Number of records in current response
- **offset:** Starting position of records
- **limit:** Maximum records per request
- **total:** Total records available
- **links:** Navigation links for pagination

## Implementation Notes

- Responses are ordered by creation date with most recently created items appearing first
- Default behavior returns 25 most recent objects unless the `limit` parameter specifies otherwise
- Use the `filter` parameter to control which fields appear in responses

## Related Endpoints

This endpoint is part of the Variable Recurring Payments section:
- List supported banks: GET `/open-banking/supported-banks`
- Create a mandate: POST `/open-banking/mandates`
- List mandates: GET `/open-banking/mandates`
- Retrieve a mandate: GET `/open-banking/mandates/{mandate_id}`
- Initiate a VRP: POST `/open-banking/vrps`
- Confirm funds: POST `/open-banking/mandates/{mandate_id}/confirm-funds`

---

<a id="pagination"></a>

## pagination

# Pagination Documentation

## Overview

The Acquired API implements pagination to manage large result sets efficiently. "When making calls to the Acquired.com API, a lot of the time, there will be a number of results to return."

**Default behavior:** 25 results returned
**Maximum limit:** 100 results per request
**Exceeding limits:** Returns either a 400 Bad Request or automatically caps results at 100

## Query Parameters

| Field | Type | Description | Default |
|-------|------|-------------|---------|
| `filter` | string | Filter parameters to return in the response | N/A |
| `limit` | integer | Number of values returned (max 100) | 25 |
| `offset` | integer | Record to start the response on | 0 |

### Example Request
```
{{url}}/customers?filter=customer_id,dob,last_name&limit=30&offset=2
```

### Example Response
```json
{
  "meta": {
    "count": 30,
    "offset": 2,
    "limit": 30,
    "total": 88
  },
  "data": [
    {
      "customer_id": "5b18a0c0-05de-dfe3-6309-cc703723b0bf",
      "dob": "1994-01-20",
      "last_name": "Barrow"
    }
  ]
}
```

## Metadata Responses

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Number of records returned in response |
| `offset` | integer | Starting object position (matches request offset) |
| `limit` | integer | Maximum records per response |
| `total` | integer | Total records available in query |

### Sample Metadata Examples

**GET /customers** (77 total records)
```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 77
  }
}
```

**GET /customers?limit=15**
```json
{
  "meta": {
    "count": 15,
    "offset": 0,
    "limit": 15,
    "total": 77
  }
}
```

**GET /customers?offset=5**
```json
{
  "meta": {
    "count": 25,
    "offset": 5,
    "limit": 25,
    "total": 77
  }
}
```

**GET /customers?offset=5&limit=5**
```json
{
  "meta": {
    "count": 5,
    "offset": 5,
    "limit": 5,
    "total": 77
  }
}
```

## HATEOAS

The API implements HATEOAS (Hypermedia As The Engine Of Application State) through `links` parameters, allowing clients to discover actions dynamically from resources themselves.

**Endpoints returning links:**
- /payments
- /payments/apple-pay
- /payments/google-pay
- /payments/recurring
- /transactions/{transaction_id}/refund
- /transactions/{transaction_id}/void
- /transactions/{transaction_id}/capture

### Payment Response Example
```json
{
  "transaction_id": "29c05255-8905-f38f-1446-03f83de52d7c",
  "status": "success",
  "issuer_response_code": "00",
  "links": [
    {
      "rel": "self",
      "href": "/v1/transactions/29c05255-8905-f38f-1446-03f83de52d7c"
    }
  ]
}
```

Submit a subsequent GET request to the `href` value to retrieve transaction details.

---

<a id="pay-by-bank-1"></a>

## pay-by-bank-1

# Pay by Bank Solutions Documentation

## Overview

The Acquired API documentation page for "Pay by Bank solutions" outlines integration approaches for their payment system. The page presents two distinct integration methods in a comparative table format.

## Integration Methods

According to the documentation, there are two primary approaches:

**Hosted Checkout**: Described as a "low effort" solution, this method provides "a prebuilt Hosted Checkout that you can customise to match your brand." Users should reference the Hosted Checkout endpoint documentation for implementation details.

**API Integration**: Characterized as "high effort," this approach enables developers to "build a fully API-based integration, allowing you to design your own checkout UI."

## Implementation Paths

For developers choosing the API route, the documentation recommends a sequential approach:

1. Start with the "Get a list of support banks" endpoint reference
2. Proceed to the "Single Immediate Payment" endpoint reference
3. Consult the "Build your own UI" guide for comprehensive guidance

## Navigation Structure

The sidebar reveals extensive API documentation organization including:
- Authentication and customer management
- Card operations and payment processing
- Multiple payment methods (Apple Pay, Google Pay, recurring payments)
- Specialized solutions (Direct Debit, Faster Payments, Variable Recurring Payments)
- Transaction management and reporting tools

The documentation emphasizes that merchants can select integration depth based on their technical resources and customization requirements.

---

<a id="pay-by-bank"></a>

## pay-by-bank

# Pay by Bank Solutions Documentation

## Overview

The page presents two integration approaches for the Pay by Bank solution:

**Hosted Checkout** - Low effort implementation using a "prebuilt Hosted Checkout that you can customise to match your brand."

**API** - High effort, fully custom integration enabling merchants to "design your own checkout UI."

## Integration Paths

### Hosted Checkout Route
Developers should navigate to the Hosted Checkout endpoint reference to generate a `link_id` for quick implementation.

### API Integration Route
For custom solutions, the documented sequence involves:

1. **Get supported banks** - Retrieve available financial institutions via the "Get a list of support banks" endpoint
2. **Process payment** - Execute transactions using the "Single Immediate Payment" reference
3. **Build custom UI** - Refer to the "Build your own UI guide" for implementation details

## Related API Endpoints

The documentation cross-references several payment processing sections:
- Payments (including Apple Pay, Google Pay, recurring payments)
- Transactions (refunds, voids, captures, reversals)
- Direct Debit operations
- Variable Recurring Payments
- Faster Payments infrastructure

## Navigation Structure

The reference menu organizes content into Fundamentals (statuses, responses, error handling, pagination) and an Acquired API section spanning authentication through reporting capabilities.

---

<a id="payment-methods-1"></a>

## payment-methods-1

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

---

<a id="payments"></a>

## payments

# Process a Payment - API Documentation

## Endpoint Overview

**POST** `https://test-api.acquired.com/v1/payments`

"Submit a payment request into the API and receive a response. If you want to create a card for future use, submit the full card details within the card object and set the `create_card` parameter to `true`"

## Authentication

- **Type**: Bearer Token (JWT)
- **Required Header**: Authorization with valid access token

## Request Headers

| Header | Type | Description |
|--------|------|-------------|
| Company-Id | UUID | Unique ID assigned by Acquired.com for your company |
| Mid | UUID | Unique ID connecting to a specific acquiring bank |

## Request Body Structure

### Transaction Object (Required)

| Field | Type | Description | Required |
|-------|------|-------------|----------|
| order_id | String | Unique reference for payment (2-50 chars, alphanumeric with hyphens) | Yes |
| amount | Float | Total amount to charge | Yes |
| currency | String | ISO 4217 code in lowercase | Yes |
| moto | Boolean | Set true for phone payments (default: false) | No |
| capture | Boolean | Set false to authorize only; true triggers sale (default: true) | No |
| custom_data | String | Base64 encoded custom data | No |
| custom1 | String | Additional customer/reference data (0-50 chars) | No |
| custom2 | String | Additional customer/reference data (0-50 chars) | No |

### Payment Object

**Card Details:**
- holder_name: String (2-45 chars, alphabetic)
- number: String (full card number)
- expiry_month: Integer (1-12)
- expiry_year: Integer (22-32)
- cvv: String (3-4 digits)
- create_card: Boolean (set true to save for future use)
- reference: String (1-18 chars, appears on bank statement when supported)

### Customer Object (Optional)

Includes customer_id or full customer details:
- first_name / last_name
- date_of_birth (format: YYYY-MM-DD)
- Billing address with line_1, line_2, city, state, postcode, country_code
- Shipping address (with address_match option)
- Email and phone contact information

### 3D Secure (TDS) Object (Optional)

- is_active: Boolean
- challenge_preference: Enum (challenge_mandated, challenge_preferred, no_challenge_requested, no_preference)
- challenge_window_size: Enum (full_screen, windowed sizes from 250x400 to 600x400)
- contact_url: URI to support page
- redirect_url: URI for post-authentication redirect
- webhook_url: Endpoint for notifications

## Supported Currencies

aed, aud, cad, chf, cny, dkk, eur, gbp, hkd, jpy, mxn, sek, usd, zar

## Response Codes

| Code | Status | Description |
|------|--------|-------------|
| 201 | Created | Payment processed successfully |
| 400 | Bad Request | Validation errors in request parameters |
| 401 | Unauthorized | Authentication failed |

## Key Notes

- If `capture` is false, transaction_type will be authorization only
- Full card details required unless using stored card_id
- "Required for MCC 6012 merchants" applies to last_name and dob fields
- Recommended fields include billing address and email for 3DS transactions
- Base URL for test environment: `https://test-api.acquired.com/v1`
- Base URL for production: `https://api.acquired.com/v1`

---

<a id="post-confirmation-of-payee"></a>

## post-confirmation-of-payee

# Confirmation of Payee

## Endpoint Overview

**Method:** POST
**URL:** `https://test-api.acquired.com/v1/tools/confirmation-of-payee`

## Purpose

This endpoint enables verification of payee account details prior to adding them to the system or initiating a payment transaction. It allows you to verify your payee's account details before adding them to our system or initiating a payment.

## Authentication

The endpoint supports two credential types:
- Bearer token authentication
- JWT authentication

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | No | Unique ID for your company |

## Request Structure

The API accepts a JSON request body containing payee verification details. The request includes account holder information and bank account details to validate against the financial institution's records.

### Expected Parameters

- **account_name** (string): Name of the account holder
- **sort_code** (string): Bank sort code (UK)
- **account_number** (string): Bank account number
- **account_type** (string): Type of account (personal/business)

## Response Codes

| Status | Description |
|--------|-------------|
| 201 Created | Successful verification |
| 400 Bad Request | Validation errors in request parameters |
| 401 Unauthorized | Authentication failure |
| 404 Not Found | Resource not found |

## Response Structure

### Success Response (201 Created)

Returns confirmation status indicating whether the payee details match the account holder information on file.

### Error Responses

Error responses include:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error details",
  "instance": "/v1/tools/confirmation-of-payee",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Additional Context

This tool operates within the broader Acquired API framework, which provides payment processing, customer management, and transaction handling capabilities. The confirmation of payee function serves as a validation utility within the payment ecosystem, helping to prevent misdirected payments and fraud.

## Code Examples

Available in: Shell, Node, Ruby, PHP, Python

---

<a id="reasons"></a>

## reasons

# Reasons - Acquired API Documentation

## Overview

The Acquired API uses specific reason codes to provide detailed context about transaction outcomes. "If the status is `success`, then a reason will not be returned. This is the same for the status `error`."

## Status: `declined`

Decline reasons are indexed by frequency of occurrence:

| Reason | Description |
|--------|-------------|
| `insufficient_funds` | Account lacked sufficient funds at transaction time (retries may succeed later) |
| `generic_decline` | Bank unwilling to accept transaction; customer must contact bank |
| `closed_account` | Customer no longer has account at that bank |
| `lost_or_stolen` | Card expired or reported lost; request updated card info |
| `restricted_card` | Card issuer declined due to restrictions |
| `expired_card` | Card expired or incorrect expiry date entered |
| `suspected_fraud` | Card flagged as fraudulent by issuing bank |
| `cardholder_cancellation` | Cardholder ordered bank to decline future attempts |
| `invalid_cvv` | Invalid security code or card information typo |
| `invalid_pin` | Incorrect PIN code entered |
| `invalid_avs` | Incorrect address information provided |
| `soft_decline` | For recurring payments, merchant must re-tokenize card |

## Status: `blocked`

Blocked status results from configured rules. "The `blocked` reasons have a RAG status of low, medium or high, depending on how often these reasons are returned."

High occurrence reasons include `6012_data` and `cardholder_cancelled`. Medium occurrence includes BIN filters (`bin_bank`, `bin_scheme`, `bin_country`, `blocklist_bin`) and transaction limits. Low occurrence covers velocity checks, blocklists, and fraud scoring.

## EMV 3DS Reasons

### Status: `tds_error`
- `acs_technical_issue`: Technical issue after cardholder challenge completion
- `bad_gateway`: System technical issue encountered

### Status: `tds_expired`
- `challenge_time_out`: Session expired; cardholder didn't complete issuing bank challenge

### Status: `tds_failed`
Includes authentication failures such as `challenge_not_completed`, `card_authentication_failed`, `challenge_failed`, `no_card_record`, `suspected_fraud`, and `low_confidence`.

---

<a id="references"></a>

## references

# References Page - Acquired API Documentation

## Overview

The References page serves as a landing point for foundational API documentation materials. "In this section you can find references that will be useful when analysing your request and response data."

## Available Reference Sections

The page provides three main reference categories:

1. **Issuer Response Codes** - Documentation covering response codes from payment issuers
2. **Country & Currency Codes** - Reference tables for supported countries and currencies
3. **Dynamic Descriptor** - Information about dynamic descriptor implementation

## Page Structure

This is a basic reference page (not an API endpoint) located at `/reference/references`. It functions as an organizational hub within the "Fundamentals" category of the Acquired API documentation.

## Context

The References section sits within the broader API documentation structure alongside:
- Statuses
- Reasons
- HTTP Responses
- Expanding Responses
- Handling Errors
- Pagination

---

<a id="reports"></a>

## reports

# Reports - Overview

## Section Overview

The Reports section of the Acquired.com API provides endpoints for accessing reconciliation reports and transaction data.

## Available Endpoints

### List all Reconciliation Reports
**GET** `/reports/reconciliations`
Retrieve a paginated list of available reconciliation reports, querying by Company-Id, Mid, date range, and specific fields.

### Retrieve a Reconciliation Report
**GET** `/reports/reconciliations/{reconciliation_id}`
Fetch a specific reconciliation report by its unique identifier.

## Authentication

All endpoints in this section require Bearer Token (JWT) authentication.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Common Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Authorization` | string | Yes | Bearer token for authentication |
| `Company-Id` | string (UUID) | Yes | Unique ID assigned by Acquired.com for your company |
| `Mid` | string (UUID) | No | Unique ID connecting to a specific acquiring bank |

## Common Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `offset` | integer (int32) | No | 0 | The record to start the response on |
| `limit` | integer (int32) | No | 25 | Maximum records returned (min: 1, max: 100) |
| `filter` | string | No | - | Filter which parameters appear in the response |
| `date_from` | date | No | - | Start of date range for report filtering |
| `date_to` | date | No | - | End of date range for report filtering |

## Pagination Response Format

```json
{
  "meta": {
    "count": 0,
    "offset": 0,
    "limit": 0,
    "total": 0,
    "links": [
      {
        "rel": "first|last|prev|next|self",
        "href": "/v1/reports/reconciliations?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

## Error Response Format

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|forbidden",
  "title": "Human-readable error message",
  "instance": "/v1/reports/reconciliations",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

## Usage Notes

- Reports are paginated; use offset/limit for navigation
- Filter parameter enables selective field returns
- Date range parameters enable time-based filtering
- Timestamps return in ISO 8601 format

## Code Examples

Available in: Shell, Node, Ruby, PHP, Python

---

<a id="statuses"></a>

## statuses

# Statuses Documentation

## Overview

The Acquired.com API returns a status within the response for every request made. The status is a string type indicating the current request status.

For statuses including `declined`, `blocked`, `tds_error`, `tds_expired`, and `tds_failed`, an associated reason provides more specific detail about what occurred. Reference the reasons guide for additional information.

---

## Card Payment Statuses

| Status | Description |
|--------|-------------|
| `success` | The request or payment has been successfully processed. |
| `declined` | Payment request was declined by the bank, or user failed to authenticate. |
| `blocked` | A pre-configured rule stopped the payment request from processing. |
| `error` | The request was received, but an error occurred within the platform. |
| `tds_error` | User has not abandoned the process, but a technical issue arose during authentication. |
| `tds_expired` | Session expired as the cardholder did not complete the authentication process. |
| `tds_failed` | The authentication process has failed due to various possible reasons. |
| `tds_pending` | Awaiting authentication completion. |

---

## Pay by Bank Statuses

| Status | Description |
|--------|-------------|
| `settled` | Payment received into your settlement account (Acquired-held accounts only). |
| `executed` | Payment request has been executed by the bank. |
| `cancelled` | User rejected the payment without providing consent. |
| `expired` | Payment abandoned by the user (merchants don't receive this status). |
| `error` | Payment failed due to internal error or network issues. |
| `declined` | Payment declined by bank, or user failed to authenticate. |

---

## Payout Statuses

| Status | Description |
|--------|-------------|
| `success` | The request or payout has been successfully processed. |
| `declined` | The payout request was declined by the bank. |
| `error` | The request was received, but an error occurred within the platform. |

---

<a id="tools"></a>

## tools

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

---

<a id="transactions"></a>

## transactions

# Retrieve a Transaction

## Overview

This endpoint allows you to fetch transaction details using a `transaction_id`. Every time you submit a payment request, including card, recurring payments, Google Pay, Apple Pay, you are returned a `transaction_id`. Append the `transaction_id` to the URL to retrieve the transaction details.

## Endpoint Details

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`

### Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1/transactions/{transaction_id}`
- **Production Environment:** `https://api.acquired.com/v1/transactions/{transaction_id}`

## Authentication

This endpoint requires Bearer token authentication. Two credential types are supported:

- Bearer Token
- JWT

Include your access token in the Authorization header:
```
Authorization: Bearer {access_token}
```

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `transaction_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the transaction |

## Request

No request body is required for this GET request. Simply append your transaction identifier to the URL path.

### Supported Languages

The endpoint documentation provides code examples in:
- Shell
- Node
- Ruby
- PHP
- Python

## Response

The API returns transaction details in JSON format. The response includes comprehensive information about the payment transaction including:

- Transaction status and type
- Amount and currency information
- Payment method details
- Customer information
- Timestamp data
- Authorization codes and references

## HTTP Status Codes

| Status | Description |
|--------|-------------|
| 200 | OK - Transaction successfully retrieved |
| 400 | Bad Request - Validation error in request parameters |
| 401 | Unauthorized - Authentication credentials invalid or missing |
| 404 | Not Found - Transaction ID does not exist |

## Error Response Format

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error description",
  "instance": "/v1/transactions/{transaction_id}",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Explanation of validation failure"
    }
  ]
}
```

## Related Endpoints

- [Process a refund](create-refund.md) (POST)
- [Process a void](create-void.md) (POST)
- [Process a capture](create-capture.md) (POST)
- [Process a reversal](create-reversal.md) (POST)
- [List all transactions](fetch-all-transactions.md) (GET)

---

<a id="update-card-details"></a>

## update-card-details

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

---

<a id="update-customer"></a>

## update-customer

# Update a Customer - API Documentation

## Endpoint Overview

**Method:** PUT
**URL:** `https://test-api.acquired.com/v1/customers/{customer_id}`

The endpoint permits modification of customer details via the `customer_id` parameter. Any parameters not provided will be left unchanged.

## Authentication

- **Type:** Bearer Token (JWT)
- **Required:** Yes

## Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `customer_id` | string (UUID) | Yes | Unique identifier assigned by Acquired.com for the customer |

## Request Body Schema

All fields are optional for updates. Only include fields you wish to modify.

### Basic Information

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `reference` | string | 0-50 chars | Your internal customer identifier (alphanumeric, hyphens, periods, commas) |
| `first_name` | string | 0-22 chars | Customer's given name (alphabetic only) |
| `last_name` | string | 0-22 chars | Customer's surname (required for MCC 6012) |
| `dob` | string | date format YYYY-MM-DD | Date of birth (required for MCC 6012) |
| `custom_data` | string | - | Base64-encoded custom data |

### Billing Address

| Parameter | Type | Length | Description |
|-----------|------|--------|-------------|
| `billing.address.line_1` | string | 0-50 chars | Primary address line |
| `billing.address.line_2` | string | 0-50 chars | Secondary address line |
| `billing.address.city` | string | 1-40 chars | Municipality |
| `billing.address.state` | string | 0-3 chars | ISO 3166-2 state code (US only) |
| `billing.address.postcode` | string | 1-40 chars | Postal code |
| `billing.address.country_code` | string | 2 chars | ISO 3166 country code |
| `billing.email` | string | - | Email address |
| `billing.phone.country_code` | string | 1-3 digits | International dialing prefix |
| `billing.phone.number` | string | 0-15 digits | Phone number |

### Shipping Address

Same structure as billing, with additional field:

| Parameter | Type | Description |
|-----------|------|-------------|
| `shipping.address_match` | boolean | Set to true to mirror billing address details |

All other shipping fields follow the same structure as billing (`shipping.address.*`, `shipping.email`, `shipping.phone.*`).

## Response Codes

### 200 OK - Success

```json
{
  "status": "success"
}
```

### 400 Bad Request - Validation failure

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/customers/{customer_id}",
  "invalid_parameters": [
    {
      "parameter": "field_name",
      "reason": "explanation of validation failure"
    }
  ]
}
```

### 401 Unauthorized - Authentication credentials invalid or missing

```json
{
  "status": "error",
  "error_type": "unauthorized",
  "title": "Authentication with the API failed, please check your details and try again.",
  "instance": "/v1/customers/{customer_id}"
}
```

### 409 Conflict - Request conflicts with existing data or state

## Notes

- Any parameters not provided will be left unchanged
- Field validation enforces pattern matching (alphanumeric constraints, special character restrictions)
- `company_id` and timestamp fields (`created`, `last_updated`) are read-only and cannot be modified
- Phone numbers accept only numeric characters

## Related Endpoints

- **Create a customer:** `POST /v1/customers`
- **List all customers:** `GET /v1/customers`
- **Retrieve a customer:** `GET /v1/customers/{customer_id}`
- **Get customer cards:** `GET /v1/customers/{customer_id}/cards`

---

<a id="update-session-id"></a>

## update-session-id

# Update a session_id - API Documentation

## Endpoint Overview

**HTTP Method:** PUT
**URL:** `https://test-api.acquired.com/v1/payment-sessions/{session_id}`

## Description

"This request allows you to update any details of the previously created `session_id.`" When payment completion occurs within Components, the authorization will use the most recent parameters configured against the `session_id`.

## Authentication

**Type:** Bearer Token (JWT)

## Base URL

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Request Parameters

The endpoint accepts a PUT request to modify an existing payment session. You submit only the parameters requiring updates; any unmodified parameters remain unchanged.

### Path Parameter

| Parameter | Type | Description |
|-----------|------|-------------|
| `session_id` | String (UUID) | The unique identifier of the payment session to update |

## Language/SDK Support

Documentation indicates support for:
- Shell
- Node
- Ruby
- PHP
- Python

## Related Endpoints

Within the Components section, you can also:
- **Generate a session_id** (POST `/payment-sessions`)

## Navigation Context

This endpoint belongs to the **Components** category under the Acquired API reference, alongside session generation functionality.

---

**Note:** Complete request/response schema details and code examples are referenced in the original API specification. The request body accepts the same structure as the Generate session_id endpoint (transaction, payment, customer, and tds objects) - only the fields being updated need to be included in the request.

---

<a id="variable-recurring-payments-1"></a>

## variable-recurring-payments-1

# Variable Recurring Payments (VRP) - Overview

## Section Overview

The Variable Recurring Payments section of the Acquired.com API provides endpoints for managing Open Banking VRP mandates and payments. VRP enables merchants to collect variable amounts from customers through Open Banking connections with their banks.

## Available Endpoints

### List Supported Banks
**GET** `/open-banking/supported-banks`
Returns a list of banks supported for Open Banking and variable recurring payment journeys.

### Create a Mandate
**POST** `/open-banking/mandates`
Creates a mandate and returns the details needed to direct the customer to their bank for authorization.

### List Mandates
**GET** `/open-banking/mandates`
Returns a paginated list of mandates.

### Retrieve an Open Banking Mandate
**GET** `/open-banking/mandates/{mandate_id}`
Retrieves an open banking mandate using a unique identifier.

### Initiate a Variable Recurring Payment
**POST** `/open-banking/vrps`
Initiates a variable recurring payment against an established mandate.

### List Variable Recurring Payments
**GET** `/open-banking/vrps`
Returns a paginated list of variable recurring payments.

### Confirm Funds Check Against a Mandate
**POST** `/open-banking/mandates/{mandate_id}/confirm-funds`
Initiates a confirmation of funds check against an established mandate.

## Authentication

All endpoints in this section require Bearer Token (JWT) authentication.

## Base URLs

- **Test Environment:** `https://test-api.acquired.com/v1`
- **Production Environment:** `https://api.acquired.com/v1`

## Pagination

List endpoints support pagination through:
- `offset` (integer): The record to start the response on
- `limit` (integer): Maximum number of records returned (1-100)
- `filter` (string): Filter parameters to return within response

### Pagination Response Structure

```json
{
  "meta": {
    "count": 0,
    "offset": 0,
    "limit": 0,
    "total": 0,
    "links": [
      {
        "rel": "first|last|prev|next|self",
        "href": "/v1/path?offset=0",
        "title": "Page description"
      }
    ]
  },
  "data": []
}
```

## Error Response Format

All endpoints return errors in the standard format:

```json
{
  "status": "error",
  "error_type": "validation|unauthorized|not_found",
  "title": "Human-readable error message",
  "instance": "/v1/endpoint-path",
  "invalid_parameters": [
    {
      "parameter": "parameter_name",
      "reason": "Details what is causing the error"
    }
  ]
}
```

---

