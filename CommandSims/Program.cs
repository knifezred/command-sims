using CommandSims.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TimeService>();
builder.Services.AddHostedService<GameService>();
using IHost host = builder.Build();
await host.RunAsync();