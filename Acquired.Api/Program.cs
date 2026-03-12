using Acquired.Api.Middleware;
using Acquired.Services.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAcquiredServices(builder.Configuration);
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<AcquiredExceptionMiddleware>();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
