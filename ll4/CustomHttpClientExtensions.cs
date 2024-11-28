using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExtensions
{
    public static class CustomHttpClientExtensions
    {
        public static IServiceCollection AddHttpClient<TClient, TImplementation>(
            this IServiceCollection services,
            Action<HttpClient> configureClient)
            where TClient : class
            where TImplementation : class, TClient
        {
            services.AddTransient<TClient, TImplementation>();

            services.AddTransient(serviceProvider =>
            {
                var httpClient = new HttpClient();
                configureClient?.Invoke(httpClient);
                return httpClient;
            });

            return services;
        }
    }
}