using System;
using HaveIBeenRefitted.DelegatingHandlers;
using HaveIBeenRefitted.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace HaveIBeenRefitted.Extensions
{
    /// <summary>
    /// Service Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all of the necessary Pwned service functionality to
        /// the <paramref name="services"/> collection for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="options">The action used to configure options.</param>
        /// <returns>The same <paramref name="services"/> instance with other services added.</returns>
        /// <exception cref="ArgumentNullException">
        /// If either the <paramref name="services"/> or <paramref name="options"/> are <c>null</c>.
        /// </exception>
        public static IServiceCollection AddHaveIBeenRefitted(
            this IServiceCollection services,
            Action<HaveIBeenRefittedOptions> options)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services), "The IServiceCollection cannot be null.");
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options), "The Action<HaveIBeenRefittedOptions> cannot be null.");
            }

            services.Configure(options);

            return AddHaveIBeenRefitted(services);
        }
        
        private static IServiceCollection AddHaveIBeenRefitted(IServiceCollection services)
        {
            // Options
            services.AddOptions<HaveIBeenRefittedOptions>();
            
            // Delegating Handlers
            services.AddSingleton<TokenProviderDelegatingHandler>();
            services.AddSingleton<UserAgentDelegatingHandler>();
            
            
            // Breach client
            services
                .AddRefitClient<IBreachClient>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var options = provider.GetService<IOptions<HaveIBeenRefittedOptions>>();
                    if (options?.Value != null)
                    {
                        client.BaseAddress = new Uri(options.Value.BaseUrl);
                    }
                    else
                    {
                        throw new ArgumentException(nameof(options));
                    }
                }) 
                .AddHttpMessageHandler<TokenProviderDelegatingHandler>()
                .AddHttpMessageHandler<UserAgentDelegatingHandler>();

            return services;
        }
    }
}