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
