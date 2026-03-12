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
