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
