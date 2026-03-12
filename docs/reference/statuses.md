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
