using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HaveIBeenRefitted.Options;
using Microsoft.Extensions.Options;

namespace HaveIBeenRefitted.DelegatingHandlers
{
    /// <summary>
    /// The API token can either be provided via the IOptions during service registration or as an environment variable "HIBP_API"
    /// See <a href="https://haveibeenpwned.com/api/v3/#Authorisation"></a>
    /// </summary>
    /// <remarks>
    /// If the <see cref="HaveIBeenRefittedOptions.ApiKey"/> property has been set, it will take preference over the environment variable
    /// </remarks>
    public class TokenProviderDelegatingHandler : DelegatingHandler
    {
        private readonly HaveIBeenRefittedOptions _options;

        public TokenProviderDelegatingHandler(IOptions<HaveIBeenRefittedOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("hibp-api-key", _options.ApiKey ?? Environment.GetEnvironmentVariable("HIBP_API"));
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}