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
