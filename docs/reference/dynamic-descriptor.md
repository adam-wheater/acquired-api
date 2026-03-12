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
