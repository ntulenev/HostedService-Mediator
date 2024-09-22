using Serilog;

using HostedServiceMediatRExample.DataConsumers;
using HostedServiceMediatRExample.Models;
using HostedServiceMediatRExample.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddTransient<IDataConsumer<Request>, FakeRequestConsumer>();
builder.Services.AddHostedService<RequestService>();
// Register mediator
builder.Services.AddMediatR
(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
);
builder.Services.AddHealthChecks();
builder.Host.UseDefaultServiceProvider(x =>
{
    x.ValidateOnBuild = true;
    x.ValidateScopes = true;
});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
var app = builder.Build();
app.UseHealthChecks("/hc");
app.UseHttpsRedirection();

await app.RunAsync();
