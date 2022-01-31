using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HaveIBeenRefitted.Options;
using Microsoft.Extensions.Options;

namespace HaveIBeenRefitted.DelegatingHandlers
{
    /// <summary>
    /// Each request to the API must be accompanied by a user agent request header.
    /// Typically this should be the name of the app consuming the service.
    /// A missing user agent will result in an HTTP 403 response. 
    /// See <a href="https://haveibeenpwned.com/api/v3/#UserAgent"></a>
    /// </summary>
    public class UserAgentDelegatingHandler : DelegatingHandler
    {
        private readonly HaveIBeenRefittedOptions _options;

        public UserAgentDelegatingHandler(IOptions<HaveIBeenRefittedOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("user-agent", _options.UserAgent);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}