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
