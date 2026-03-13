using Acquired.Services.Auth;
using Acquired.Services.Cards;
using Acquired.Services.Configuration;
using Acquired.Services.Customers;
using Acquired.Services.DirectDebit;
using Acquired.Services.FasterPayments;
using Acquired.Services.Http;
using Acquired.Services.OpenBanking;
using Acquired.Services.PayByBank;
using Acquired.Services.PaymentLinks;
using Acquired.Services.Payments;
using Acquired.Services.PaymentSessions;
using Acquired.Services.Reports;
using Acquired.Services.Tools;
using Acquired.Services.Transactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace Acquired.Services.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAcquiredServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Bind configuration
        services.Configure<AcquiredOptions>(
            configuration.GetSection(AcquiredOptions.SectionName));

        // 2. Register TokenService as Singleton
        services.AddSingleton<ITokenService, TokenService>();

        // 3. Register AcquiredHttpClient as Scoped
        services.AddScoped<IAcquiredHttpClient, AcquiredHttpClient>();

        // 4. Named HttpClient for auth (no retry policies)
        services.AddHttpClient("AcquiredAuth", (sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<AcquiredOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });

        // 5. Named HttpClient for API with Polly resilience policies
        var delay = Backoff.DecorrelatedJitterBackoffV2(
            medianFirstRetryDelay: TimeSpan.FromSeconds(1),
            retryCount: 3);

        services.AddHttpClient("AcquiredApi", (sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<AcquiredOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        })
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(delay))
        .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        // 6. Register all business services as Scoped
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentSessionService, PaymentSessionService>();
        services.AddScoped<IPaymentLinkService, PaymentLinkService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IPayByBankService, PayByBankService>();
        services.AddScoped<IFasterPaymentService, FasterPaymentService>();
        services.AddScoped<IDirectDebitService, DirectDebitService>();
        services.AddScoped<IOpenBankingService, OpenBankingService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IToolService, ToolService>();

        // 7. Register IHttpContextAccessor
        services.AddHttpContextAccessor();

        return services;
    }
}
