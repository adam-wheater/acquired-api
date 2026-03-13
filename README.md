# Acquired.com API Proxy Service

A .NET 8 ASP.NET Core Web API that acts as a proxy layer to the [Acquired.com](https://acquired.com) payment platform API, providing a consistent interface for payment processing, customer management, transaction operations, open banking, direct debit, and more.

## Table of Contents

- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Request Headers](#request-headers)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Resilience](#resilience)
- [Token Management](#token-management)
- [Payment Flows](#payment-flows)
- [Postman Collection](#postman-collection)
- [Test Cards](#test-cards)
- [Health Check](#health-check)

## Architecture

```
┌──────────────┐     ┌─────────────────────┐     ┌─────────────────────────┐
│   Consumer   │────>│  Acquired.Api Proxy  │────>│  Acquired.com REST API  │
│   (Client)   │<────│   (.NET 8 / Kestrel) │<────│  test-api.acquired.com  │
└──────────────┘     └─────────────────────┘     └─────────────────────────┘
                              │
                     ┌────────┴────────┐
                     │                 │
              Acquired.Services  Acquired.Models
              (HTTP, Auth, Biz)  (DTOs, Common)
```

The proxy receives requests from consumers, authenticates with Acquired.com via JWT tokens, and forwards the request to the upstream API. Responses are passed back transparently, including error responses.

## Project Structure

```
Acquired.sln
├── Acquired.Models/                    # Shared DTOs and model definitions
│   ├── Auth/                           # LoginRequest, LoginResponse
│   ├── Cards/                          # CardResponse, UpdateCardRequest
│   ├── Common/                         # AcquiredErrorResponse, PaginatedResponse,
│   │                                   # PaginationQuery, AddressModel, PhoneModel,
│   │                                   # ContactModel, LinkModel, TdsModel
│   ├── Customers/                      # CustomerRequest, CustomerResponse,
│   │                                   # CreateCustomerResponse
│   ├── DirectDebit/                    # CreateMandateRequest, MandateResponse
│   ├── FasterPayments/                 # PayoutRequest, CreatePayeeRequest,
│   │                                   # AccountResponse, PayeeResponse
│   ├── OpenBanking/                    # CreateObMandateRequest, CreateVrpRequest,
│   │                                   # ConfirmFundsRequest, ObMandateResponse
│   ├── PayByBank/                      # SingleImmediatePaymentRequest
│   ├── PaymentLinks/                   # PaymentLinkRequest, SendPaymentLinkRequest
│   ├── Payments/                       # PaymentRequest, ReusePaymentRequest,
│   │                                   # ApplePayRequest, GooglePayRequest,
│   │                                   # RecurringPaymentRequest, CreditPaymentRequest,
│   │                                   # InternalTransferRequest, CollectionRequest
│   ├── PaymentSessions/                # PaymentSessionRequest, UpdatePaymentSessionRequest
│   ├── Reports/                        # ReconQuery
│   ├── Tools/                          # ConfirmationOfPayeeRequest/Response
│   └── Transactions/                   # RefundRequest, CaptureRequest, ReversalRequest,
│                                       # RetryRequest, TransactionResponse
├── Acquired.Services/                  # Business logic and HTTP client layer
│   ├── Auth/                           # ITokenService, TokenService (Singleton)
│   ├── Cards/                          # ICardService, CardService
│   ├── Configuration/                  # AcquiredOptions
│   ├── Customers/                      # ICustomerService, CustomerService
│   ├── DependencyInjection/            # ServiceCollectionExtensions
│   ├── DirectDebit/                    # IDirectDebitService, DirectDebitService
│   ├── Exceptions/                     # AcquiredException
│   ├── FasterPayments/                 # IFasterPaymentService, FasterPaymentService
│   ├── Http/                           # IAcquiredHttpClient, AcquiredHttpClient
│   ├── OpenBanking/                    # IOpenBankingService, OpenBankingService
│   ├── PayByBank/                      # IPayByBankService, PayByBankService
│   ├── PaymentLinks/                   # IPaymentLinkService, PaymentLinkService
│   ├── Payments/                       # IPaymentService, PaymentService
│   ├── PaymentSessions/                # IPaymentSessionService, PaymentSessionService
│   ├── Reports/                        # IReportService, ReportService
│   ├── Tools/                          # IToolService, ToolService
│   └── Transactions/                   # ITransactionService, TransactionService
├── Acquired.Api/                       # ASP.NET Core Web API host
│   ├── Controllers/                    # 13 API controllers
│   ├── Middleware/                      # AcquiredExceptionMiddleware
│   ├── Program.cs                      # Application entry point
│   ├── appsettings.json                # Configuration
│   └── Properties/
├── docs/                               # Scraped API documentation
│   ├── reference/                      # 77 individual endpoint docs (markdown)
│   └── combined.md                     # Combined reference (~7,400 lines)
├── postman/                            # Postman collection and environment
│   ├── Acquired_API.postman_collection.json
│   └── Acquired_Sandbox.postman_environment.json
└── scripts/
    └── scrape-docs.sh                  # Reusable documentation scraper
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An Acquired.com sandbox account with `app_id` and `app_key`

### Configuration

Update `Acquired.Api/appsettings.json` (or use environment variables / user secrets):

```json
{
  "Acquired": {
    "BaseUrl": "https://test-api.acquired.com/v1",
    "AppId": "<your-app-id>",
    "AppKey": "<your-app-key>",
    "TokenBufferSeconds": 60,
    "TimeoutSeconds": 30
  }
}
```

| Setting | Description | Default |
|---------|-------------|---------|
| `BaseUrl` | Acquired.com API base URL | `https://test-api.acquired.com/v1` |
| `AppId` | Your 8-character application ID | — |
| `AppKey` | Your 32-character application key | — |
| `TokenBufferSeconds` | Seconds before expiry to refresh the token | `60` |
| `TimeoutSeconds` | HTTP request timeout | `30` |

For production, change `BaseUrl` to `https://api.acquired.com/v1`.

### Build & Run

```bash
# Build
dotnet build

# Run
dotnet run --project Acquired.Api

# Verify
curl http://localhost:5000/health
```

## Request Headers

| Header | Type | Required | Description |
|--------|------|----------|-------------|
| `Company-Id` | UUID | Yes (most endpoints) | Unique identifier assigned by Acquired.com for your company |
| `Mid` | UUID | No | Unique ID connecting to a specific acquiring bank |
| `X-Correlation-Id` | string | No | Correlation ID for request tracing (auto-generated if not provided) |

The middleware extracts `Company-Id`, `Mid`, and `X-Correlation-Id` from incoming requests and propagates them to the upstream Acquired.com API. The correlation ID is always included in the response headers.

## API Endpoints

### Authentication

Authentication is handled automatically by the proxy. The `TokenService` manages JWT tokens using your `AppId` and `AppKey`, caching tokens and refreshing them before expiry.

| Upstream Endpoint | Description |
|-------------------|-------------|
| `POST /v1/login` | Generates a Bearer token (handled internally) |

### Customers

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/customers` | Create a customer |
| GET | `/v1/customers` | List all customers |
| GET | `/v1/customers/{customerId}` | Retrieve a customer |
| PUT | `/v1/customers/{customerId}` | Update a customer |

### Cards

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/v1/customers/{customerId}/cards` | Get cards by customer |
| GET | `/v1/cards` | List all cards |
| GET | `/v1/cards/{cardId}` | Get card details |
| PUT | `/v1/cards/{cardId}` | Update card details |

### Payments

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/payments` | Process a payment (full card details) |
| POST | `/v1/payments/reuse` | Process a payment with a stored card (`card_id`) |
| POST | `/v1/payments/apple-pay` | Process an Apple Pay payment |
| POST | `/v1/payments/google-pay` | Process a Google Pay payment |
| POST | `/v1/payments/recurring` | Process a recurring payment |
| POST | `/v1/payments/credit` | Process a credit/payout to card |
| POST | `/v1/payments/internal-transfer` | Process an internal transfer between accounts |
| POST | `/v1/payments/collections` | Create a Direct Debit collection |

### Payment Sessions

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/payment-sessions` | Create a payment session |
| PUT | `/v1/payment-sessions/{sessionId}` | Update a payment session |

### Payment Links

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/payment-links` | Create a payment link |
| POST | `/v1/payment-links/{linkId}/send` | Send a payment link |

### Payment Methods

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/payment-methods/apple-pay/session` | Create an Apple Pay merchant session |

### Transactions

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/v1/transactions/{transactionId}` | Retrieve a transaction |
| GET | `/v1/transactions` | List all transactions |
| POST | `/v1/transactions/{transactionId}/refund` | Process a refund |
| POST | `/v1/transactions/{transactionId}/void` | Process a void (pre-settlement) |
| POST | `/v1/transactions/{transactionId}/capture` | Capture an authorized payment |
| POST | `/v1/transactions/{transactionId}/reversal` | Process a reversal (auto void/refund) |
| POST | `/v1/transactions/{transactionId}/cancel` | Cancel a Direct Debit transaction |
| POST | `/v1/transactions/{transactionId}/retry` | Retry a failed Direct Debit transaction |

### Pay by Bank

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/v1/aspsps` | Get supported banks (ASPSPs) |
| POST | `/v1/single-immediate-payment` | Create a single immediate payment |

### Faster Payments

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/customers/{customerId}/payees` | Create a payee for a customer |
| GET | `/v1/payees` | List all payees |
| POST | `/v1/pay-out` | Process a payout |
| POST | `/v1/accounts` | Create an account |
| GET | `/v1/accounts` | List all accounts |
| GET | `/v1/accounts/{mid}` | Get account by MID |

### Direct Debit

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/mandates` | Create a mandate |
| GET | `/v1/mandates/{mandateId}` | Retrieve a mandate |
| POST | `/v1/mandates/{mandateId}/cancel` | Cancel a mandate |

### Open Banking / Variable Recurring Payments

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/v1/open-banking/supported-banks` | List supported banks |
| POST | `/v1/open-banking/mandates` | Create an Open Banking mandate |
| GET | `/v1/open-banking/mandates` | List mandates |
| GET | `/v1/open-banking/mandates/{mandateId}` | Retrieve a mandate |
| POST | `/v1/open-banking/vrps` | Initiate a variable recurring payment |
| GET | `/v1/open-banking/vrps` | List variable recurring payments |
| POST | `/v1/open-banking/mandates/{mandateId}/confirm-funds` | Confirm funds against a mandate |

### Reports

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/v1/reports/reconciliations` | List reconciliation reports |
| GET | `/v1/reports/reconciliations/{reconciliationId}` | Retrieve a reconciliation report |

**Additional query parameters:** `date_from`, `date_to` (date format), `offset`, `limit`, `filter`.

### Tools

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/v1/tools/confirmation-of-payee` | Verify payee account details |

**Request body:** `account_name`, `sort_code`, `account_number`, `account_type`.

### Endpoint Summary

| Controller | Endpoints |
|------------|-----------|
| Customers | 4 |
| Cards | 4 |
| Payments | 8 |
| Payment Sessions | 2 |
| Payment Links | 2 |
| Payment Methods | 1 |
| Transactions | 8 |
| Pay by Bank | 2 |
| Faster Payments | 6 |
| Direct Debit | 3 |
| Open Banking / VRP | 7 |
| Reports | 2 |
| Tools | 1 |
| Health | 1 |
| **Total** | **51** |

## Pagination

List endpoints support pagination via query parameters:

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `offset` | integer | 0 | Record offset to start from |
| `limit` | integer | 25 | Max records to return (1-100) |
| `filter` | string | — | Filter which fields appear in the response |

Paginated responses include a `meta` object with HATEOAS navigation:

```json
{
  "meta": {
    "count": 25,
    "offset": 0,
    "limit": 25,
    "total": 150,
    "links": [
      { "rel": "self", "href": "/v1/cards?offset=0", "title": "Current page" },
      { "rel": "first", "href": "/v1/cards?offset=0", "title": "First page" },
      { "rel": "last", "href": "/v1/cards?offset=125", "title": "Last page" },
      { "rel": "next", "href": "/v1/cards?offset=25", "title": "Next page" }
    ]
  },
  "data": [ ... ]
}
```

## Error Handling

The proxy transparently passes through Acquired.com API errors. All error responses follow a consistent format:

```json
{
  "status": "error",
  "error_type": "validation",
  "title": "Your request parameters did not pass our validation.",
  "instance": "/v1/payments",
  "invalid_parameters": [
    {
      "parameter": "transaction.currency",
      "reason": "format is invalid, must be [aed,aud,cad,chf,cny,dkk,eur,gbp,hkd,jpy,mxn,sek,usd,zar]"
    }
  ]
}
```

### Error Types

| HTTP Status | `error_type` | Description |
|-------------|--------------|-------------|
| 400 | `validation` | Request parameters failed validation |
| 400 | `bad_request` | Malformed request |
| 401 | `unauthorized` | Authentication failed |
| 403 | `forbidden` | Insufficient permissions |
| 404 | `not_found` | Resource not found |
| 409 | `conflict` | Duplicate or conflicting resource |
| 500 | `internal_server_error` | Unexpected server error |
| 500 | `configuration` | Server configuration issue |

### Exception Middleware

The `AcquiredExceptionMiddleware` handles two exception types:

1. **`AcquiredException`** — Upstream API errors are caught and returned with the original status code and error body.
2. **Unhandled exceptions** — Returns a generic 500 error with correlation ID for debugging.

## Resilience

The proxy uses [Polly](https://github.com/App-vNext/Polly) for resilience:

### Retry Policy
- **Strategy:** Decorrelated jitter backoff (via `Polly.Contrib.WaitAndRetry`)
- **Max retries:** 3
- **Median first retry delay:** 1 second
- **Triggers:** Transient HTTP errors (5xx, 408, network failures)

### Circuit Breaker
- **Failure threshold:** 5 consecutive failures
- **Break duration:** 30 seconds
- **Effect:** Short-circuits requests to prevent cascading failures

### Auth Client
The authentication HTTP client (`AcquiredAuth`) does **not** use retry policies — login failures should fail fast.

## Token Management

```
┌─────────────┐      ┌──────────────┐      ┌────────────────┐
│  API Request │─────>│ TokenService │─────>│ POST /v1/login │
│             │      │  (Singleton)  │      │  (if expired)  │
│             │<─────│  Cached JWT   │<─────│  app_id/key    │
└─────────────┘      └──────────────┘      └────────────────┘
```

- **Registration:** `TokenService` is registered as a **Singleton** for application-wide token sharing
- **Thread safety:** Uses `SemaphoreSlim` to prevent concurrent token refresh races
- **Caching:** Tokens are cached and reused until `TokenBufferSeconds` before expiry
- **Credentials:** `app_id` (8 chars) + `app_key` (32 chars) sent to `POST /v1/login`
- **Response:** `access_token` (JWT Bearer), `expires_in` (typically 3600s)

## Payment Flows

### Standard Payment (Sale)

```
Consumer ──> POST /v1/payments ──> Acquired API
                │
                ├─ transaction.capture = true (default)
                ├─ payment.card (full card details)
                ├─ customer (inline or customer_id)
                └─ tds (optional 3D Secure)
```

### Authorize + Capture

```
1. POST /v1/payments                    (capture: false)
   └─ Returns transaction_id

2. POST /v1/transactions/{id}/capture   (amount: partial or full)
   └─ Converts auth hold to charge
```

### Stored Card Payment

```
1. POST /v1/payments                    (create_card: true)
   └─ Returns card_id

2. POST /v1/payments/reuse              (card_id + cvv)
   └─ Charges the stored card
```

### Refund / Void / Reversal

```
POST /v1/transactions/{id}/refund      Post-settlement: amount + reason
POST /v1/transactions/{id}/void        Pre-settlement: no body required
POST /v1/transactions/{id}/reversal    Auto-determines void vs refund
```

### Direct Debit Flow

```
1. POST /v1/customers                   Create customer
2. POST /v1/mandates                    Create mandate against customer
3. POST /v1/payments/collections        Collect payment against mandate
4. POST /v1/transactions/{id}/retry     Retry failed collection (optional)
```

### Open Banking VRP Flow

```
1. GET  /v1/open-banking/supported-banks       List available banks
2. POST /v1/open-banking/mandates              Create mandate (redirect to bank)
3. POST /v1/open-banking/mandates/{id}/confirm-funds   Check funds available
4. POST /v1/open-banking/vrps                  Initiate variable recurring payment
```

### Pay by Bank (Single Immediate Payment)

```
1. GET  /v1/aspsps                             List supported banks
2. POST /v1/single-immediate-payment           Initiate bank payment
```

### Faster Payments Payout

```
1. POST /v1/customers/{id}/payees              Register payee
2. POST /v1/pay-out                            Execute payout
```

## Postman Collection

Import the collection and environment from the `postman/` directory:

- **Collection:** `postman/Acquired_API.postman_collection.json`
- **Environment:** `postman/Acquired_Sandbox.postman_environment.json`

### Environment Variables

| Variable | Description |
|----------|-------------|
| `base_url` | Proxy base URL (e.g. `http://localhost:5000`) |
| `company_id` | Your Acquired.com Company ID |
| `mid` | Your Merchant ID |

## Test Cards

Acquired.com sandbox test cards (from their documentation):

| Card Number | Scheme | Result |
|-------------|--------|--------|
| `4000011180138131` | Visa | Success |
| `4000021180138139` | Visa | Decline |
| `5200003131318132` | Mastercard | Success |

Refer to the [Acquired.com documentation](https://docs.acquired.com/reference/introduction) for the full list of test cards and response codes.

## Health Check

```bash
curl http://localhost:5000/health
# Returns: Healthy
```

## Tech Stack

| Component | Technology |
|-----------|------------|
| Runtime | .NET 8 |
| Framework | ASP.NET Core |
| Serialization | Newtonsoft.Json |
| Resilience | Polly + Polly.Contrib.WaitAndRetry |
| HTTP | IHttpClientFactory |
| Documentation | Scraped from docs.acquired.com |

## License

Private — internal use only.
