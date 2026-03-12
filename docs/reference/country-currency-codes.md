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
