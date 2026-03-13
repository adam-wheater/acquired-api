using Newtonsoft.Json;

namespace Acquired.Models.Customers;

public class CustomerResponse
{
    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("company_id")]
    public string? CompanyId { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("first_name")]
    public string? FirstName { get; set; }

    [JsonProperty("last_name")]
    public string? LastName { get; set; }

    [JsonProperty("dob")]
    public string? Dob { get; set; }

    [JsonProperty("custom_data")]
    public string? CustomData { get; set; }

    [JsonProperty("billing", NullValueHandling = NullValueHandling.Ignore)]
    public CustomerBilling? Billing { get; set; }

    [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
    public CustomerShipping? Shipping { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("last_updated")]
    public string? LastUpdated { get; set; }
}
