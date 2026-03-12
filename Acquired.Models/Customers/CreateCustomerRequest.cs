using Acquired.Models.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Customers;

public class CreateCustomerRequest
{
    [JsonProperty("reference")]
    [Required]
    [StringLength(50)]
    public string Reference { get; set; } = null!;

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

public class ShippingContact : AcquiredContact
{
    [JsonProperty("address_match")]
    public bool? AddressMatch { get; set; }
}
