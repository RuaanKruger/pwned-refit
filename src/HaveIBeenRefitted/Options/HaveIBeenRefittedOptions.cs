using Microsoft.Extensions.Options;

namespace HaveIBeenRefitted.Options
{
    /// <summary>
    /// Client options
    /// </summary>
    public class HaveIBeenRefittedOptions : IOptions<HaveIBeenRefittedOptions>
    {
        /// <summary>
        /// Gets or sets the HTTP header value for <c>User-Agent</c>.
        /// </summary>
        /// <remarks>
        /// If you set this user agent, it would be preferred return value in the delegating handler
        /// <see cref="DelegatingHandlers.UserAgentDelegatingHandler"/> 
        /// </remarks>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// The base URL for the HIBP API.
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// An HIBP subscription key is required to make an authorised call and can be obtained on the API key page.
        /// </summary>
        public string ApiKey { get; set; }

        /// <inheritdoc />
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public HaveIBeenRefittedOptions Value { get; }
 
    }
}