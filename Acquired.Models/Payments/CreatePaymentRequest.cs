using Acquired.Models.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Payments;

public class CreatePaymentRequest
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

public class PaymentTransaction
{
    [JsonProperty("order_id")]
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string OrderId { get; set; } = null!;

    [JsonProperty("amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;

    [JsonProperty("moto")]
    public bool? Moto { get; set; }

    [JsonProperty("capture")]
    public bool? Capture { get; set; }

    [JsonProperty("custom_data")]
    public string? CustomData { get; set; }

    [JsonProperty("custom1")]
    public string? Custom1 { get; set; }

    [JsonProperty("custom2")]
    public string? Custom2 { get; set; }
}

public class PaymentDetails
{
    [JsonProperty("card")]
    public AcquiredCard? Card { get; set; }

    [JsonProperty("card_id")]
    public string? CardId { get; set; }

    [JsonProperty("create_card")]
    public bool? CreateCard { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("token")]
    public string? Token { get; set; }
}

public class PaymentCustomer
{
    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

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

    [JsonProperty("billing")]
    public AcquiredContact? Billing { get; set; }

    [JsonProperty("shipping")]
    public AcquiredContact? Shipping { get; set; }
}

public class PaymentTds
{
    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }

    [JsonProperty("challenge_preference")]
    public string? ChallengePreference { get; set; }

    [JsonProperty("challenge_window_size")]
    public string? ChallengeWindowSize { get; set; }

    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonProperty("webhook_url")]
    public string? WebhookUrl { get; set; }
}
