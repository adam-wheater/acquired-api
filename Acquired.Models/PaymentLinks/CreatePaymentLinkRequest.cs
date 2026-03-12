using Acquired.Models.Payments;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.PaymentLinks;

public class CreatePaymentLinkRequest
{
    [JsonProperty("transaction")]
    [Required]
    public PaymentTransaction Transaction { get; set; } = null!;

    [JsonProperty("payment")]
    public PaymentDetails? Payment { get; set; }

    [JsonProperty("customer")]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds")]
    public PaymentTds? Tds { get; set; }
}
