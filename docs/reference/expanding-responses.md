# Expanding Responses - Documentation

## Overview

The Acquired API permits you to request supplementary information through the expand request parameter. This feature is accessible on specific API endpoints and influences only that particular request's response.

Objects frequently contain IDs referencing related entities. For instance, a Subscription object includes associated Price and Product identifiers. Using the expand parameter allows you to retrieve these as complete objects rather than just references. Fields marked as **Expandable** in the API reference support this functionality.

You can expand multiple objects simultaneously by listing them in the expand property, separated by commas.

## Request Example

```bash
curl --request GET \
 --url 'https://api.acquired.com/v1/subscriptions/01KGM26PY4N5N05NECS36FHHFR?expand=price,product' \
 --header 'accept: application/json'
```

## Response Comparison

### Default Response

The standard response returns only object IDs:

```json
{
  "id": "01KGFNCDRCP55BG8SBXRJ774HB",
  "product": {
    "id": "01KGFNCDRC0AFCVJM24SD37ES8"
  },
  "price": {
    "id": "01KGFNCDRCKDFX4BWFZBGG6RZS"
  }
}
```

### Expanded Response

With expand parameters, related objects are fully populated:

```json
{
  "id": "01KGFNCDRCP55BG8SBXRJ774HB",
  "product": {
    "id": "01KGFNCDRC0AFCVJM24SD37ES8",
    "name": "Premium Tier",
    "description": "Access to all premium features and priority support",
    "created_at": "2026-02-02T17:11:09.836+00:00"
  },
  "price": {
    "id": "01KGFNCDRCKDFX4BWFZBGG6RZS",
    "product_id": "01KGFNCDRC0AFCVJM24SD37ES8",
    "usage_type": "fixed",
    "amount": 999,
    "cycle_details": {
      "interval": "month",
      "interval_count": 1
    },
    "currency": "GBP",
    "created_at": "2026-02-02T17:11:09.837+00:00"
  }
}
```
