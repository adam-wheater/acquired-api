# List All Accounts - API Documentation

## Endpoint Overview

**Method:** GET
**URL:** `https://test-api.acquired.com/v1/accounts`

**Description:** "Use the GET accounts endpoint to retrieve all accounts associated with my organisation."

## Authentication

- **Type:** Bearer Token (JWT)
- **Required Header:** Authorization with Bearer token

## Base URLs

- **Test:** `https://test-api.acquired.com/v1`
- **Production:** `https://api.acquired.com/v1`

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `offset` | integer (int32) | No | "The record to start the response on" (default: 0) |
| `limit` | integer (int32) | No | "A limit on the scope of values returned" (1-100 max) |
| `filter` | string | No | "Filter the parameters that you want to return within the response" |

## Response Structure

The response follows a paginated format with metadata and data arrays.

### Success Response (200 OK)

**Meta Object:**
- `count` (integer): "The number of records returned as part of the response"
- `offset` (integer): "The starting record number"
- `limit` (integer): "The maximum number of records that can be returned"
- `total` (integer): "The total number of records contained in the query response"
- `links` (array): Navigation links (rel: self, first, last, prev, next)

**Data Array:** Contains account objects with complete account details

## Error Responses

**400 Bad Request:** Validation errors with invalid_parameters array

**401 Unauthorized:** "Authentication with the API failed, please check your details and try again"

## Related Endpoints

This endpoint is part of the Faster Payments section alongside:
- Create an account (POST)
- Retrieve account details (GET by ID)
- Internal transfer (POST)
- List all payees (GET)
