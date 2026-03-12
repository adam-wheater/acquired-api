# HTTP Responses Documentation

## Overview

"Whenever you send a request to the Acquired.com API you will receive a response in JSON format and a HTTP status code. The status code indicates the success or failure of a request to the server."

## HTTP Response Codes

The API uses the following standard HTTP status codes:

| Code | Name | Description | Title |
|------|------|-------------|-------|
| 200 | OK | Standard response for successful HTTP requests. Indicates that everything worked as expected. | N/A |
| 201 | Created | The request has been fulfilled, resulting in the creation of one or more new resources. | N/A |
| 400 | Bad Request | The Acquired.com API was unable to understand your request. This could be due to a syntax error. | "Your request parameters did not pass our validation." |
| 401 | Unauthorized | Authentication to the Acquired.com API failed resulting in an error response. This could be because you incorrectly entered your API keys or failed to enter them. | "Authentication with the API failed, please check your details and try again." |
| 403 | Forbidden | You do not have the required access to the requested resource. | "You do not have access to the requested resource, please check your details and try again." |
| 404 | Not Found | The requested resource was not found. | "Unable to find the requested resource, please check your request and try again." |
| 409 | Conflict | The request conflicts with another request. | "There was a conflict when trying to complete your request." |
| 500 | Internal Server Error | An internal server error occurred while processing your request. | "An error occurred, we are investigating the reason." |

## Successful Request Responses

"When your request is successful, you will receive either a `200 - OK` or `201 - Created` response."

### 200 - OK Example

**Request:** Updating a customer record
```json
{
  "last_name": "Jones"
}
```

**Response:**
```json
{
  "status": "Success"
}
```

### 201 - Created Example

**Request:** Creating a new card
```json
{
  "holder_name": "E Johnson",
  "scheme": "visa",
  "number": "4242424242424242",
  "expiry_month": 12,
  "expiry_year": 26,
  "cvv": "123"
}
```

**Response:**
```json
{
  "card_id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
