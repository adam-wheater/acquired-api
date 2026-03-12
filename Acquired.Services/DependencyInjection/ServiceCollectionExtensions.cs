using Acquired.Services.Auth;
using Acquired.Services.Cards;
using Acquired.Services.Configuration;
using Acquired.Services.Customers;
using Acquired.Services.DirectDebit;
using Acquired.Services.FasterPayments;
using Acquired.Services.Http;
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
using Polly.Extensions.Http;

namespace Acquired.Services.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAcquiredServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration
        services.Configure<AcquiredOptions>(configuration.GetSection(AcquiredOptions.SectionName));

        // Singletons
        services.AddSingleton<IAcquiredTokenService, AcquiredTokenService>();
        services.AddHttpContextAccessor();

        // HTTP client with Polly
        services.AddHttpClient("Acquired", (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AcquiredOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        // Scoped HTTP client wrapper
        services.AddScoped<IAcquiredHttpClient, AcquiredHttpClient>();

        // Business services
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<ICardsService, CardsService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IPaymentSessionsService, PaymentSessionsService>();
        services.AddScoped<IPaymentLinksService, PaymentLinksService>();
        services.AddScoped<ITransactionsService, TransactionsService>();
        services.AddScoped<IPayByBankService, PayByBankService>();
        services.AddScoped<IPayeesService, PayeesService>();
        services.AddScoped<IPayoutsService, PayoutsService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<ITransfersService, TransfersService>();
        services.AddScoped<IMandatesService, MandatesService>();
        services.AddScoped<ICollectionsService, CollectionsService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<IToolsService, ToolsService>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(
            medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 3);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(delay);
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30));
    }
}
