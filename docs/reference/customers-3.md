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
