using Infrastructure.HttpClient;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var productAPIURL = configuration.GetValue<string>("ProductEndpoint");
        services
            .AddRefitClient<IProductAPIClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(productAPIURL!));

        services
          .AddRefitClient<IProductCategoryAPIClient>()
          .ConfigureHttpClient(c => c.BaseAddress = new Uri(productAPIURL!));
        return services;
    }
}