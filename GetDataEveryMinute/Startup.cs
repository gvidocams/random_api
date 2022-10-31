using System;
using Atea;
using Atea.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

[assembly: FunctionsStartup(typeof(GetDataEveryMinute.Startup))]


namespace GetDataEveryMinute;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddRefitClient<IPublicApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.publicapis.org"));

        builder.Services
            .AddScoped<IBlobStorageProvider, StorageProvider>();
        builder.Services
            .AddScoped<ILogStorageProvider, StorageProvider>();

        builder.Services
            .AddSingleton<IAzureConfiguration, AzureConfiguration>();
        
        builder.Services
            .AddScoped<IBlobService, BlobService>();
        builder.Services
            .AddScoped<ILogService, LogService>();
    }
}