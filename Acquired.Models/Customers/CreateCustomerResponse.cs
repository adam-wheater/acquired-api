using Newtonsoft.Json;

namespace Acquired.Models.Customers;

public class CreateCustomerResponse
{
    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }
}
