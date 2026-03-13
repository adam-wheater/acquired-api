using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Customers;

public class CustomerRequest
{
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
    public CustomerBilling? Billing { get; set; }

    [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
    public CustomerShipping? Shipping { get; set; }
}

public class CustomerBilling
{
    [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
    public AddressModel? Address { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public PhoneModel? Phone { get; set; }
}

public class CustomerShipping
{
    [JsonProperty("address_match", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AddressMatch { get; set; }

    [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
    public AddressModel? Address { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public PhoneModel? Phone { get; set; }
}
