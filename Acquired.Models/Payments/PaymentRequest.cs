using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class PaymentRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public PaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }
}

public class PaymentTransaction
{
    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;

    [JsonProperty("moto", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Moto { get; set; }

    [JsonProperty("capture", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Capture { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }

    [JsonProperty("custom1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom1 { get; set; }

    [JsonProperty("custom2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom2 { get; set; }
}

public class PaymentDetails
{
    [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCard? Card { get; set; }

    [JsonProperty("create_card", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CreateCard { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}

public class PaymentCard
{
    [JsonProperty("holder_name")]
    public string HolderName { get; set; } = default!;

    [JsonProperty("number")]
    public string Number { get; set; } = default!;

    [JsonProperty("expiry_month")]
    public string ExpiryMonth { get; set; } = default!;

    [JsonProperty("expiry_year")]
    public string ExpiryYear { get; set; } = default!;

    [JsonProperty("cvv", NullValueHandling = NullValueHandling.Ignore)]
    public string? Cvv { get; set; }
}

public class PaymentCustomer
{
    [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomerId { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FirstName { get; set; }

    [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastName { get; set; }

    [JsonProperty("dob", NullValueHandling = NullValueHandling.Ignore)]
    public string? Dob { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }

    [JsonProperty("billing", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentBilling? Billing { get; set; }

    [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentShipping? Shipping { get; set; }
}

/// <summary>
/// Billing for payment requests — address fields are FLAT (not nested under an address object).
/// </summary>
public class PaymentBilling
{
    [JsonProperty("line_1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line1 { get; set; }

    [JsonProperty("line_2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line2 { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }

    [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
    public string? State { get; set; }

    [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
    public string? Postcode { get; set; }

    [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? CountryCode { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public PhoneModel? Phone { get; set; }
}

/// <summary>
/// Shipping for payment requests — address fields are FLAT (not nested under an address object).
/// </summary>
public class PaymentShipping
{
    [JsonProperty("address_match", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AddressMatch { get; set; }

    [JsonProperty("line_1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line1 { get; set; }

    [JsonProperty("line_2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line2 { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }

    [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
    public string? State { get; set; }

    [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
    public string? Postcode { get; set; }

    [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? CountryCode { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public PhoneModel? Phone { get; set; }
}
