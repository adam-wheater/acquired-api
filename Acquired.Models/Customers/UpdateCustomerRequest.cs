using Acquired.Models.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Customers;

public class UpdateCustomerRequest
{
    [JsonProperty("reference")]
    [StringLength(50)]
    public string? Reference { get; set; }

    [JsonProperty("first_name")]
    [StringLength(22)]
    public string? FirstName { get; set; }

    [JsonProperty("last_name")]
    [StringLength(22)]
    public string? LastName { get; set; }

    [JsonProperty("dob")]
    public string? Dob { get; set; }

    [JsonProperty("custom_data")]
    public string? CustomData { get; set; }

    [JsonProperty("billing")]
    public AcquiredContact? Billing { get; set; }

    [JsonProperty("shipping")]
    public ShippingContact? Shipping { get; set; }
}
