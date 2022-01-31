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
        public string UserAgent { get; set; }

        /// <inheritdoc />
        public HaveIBeenRefittedOptions Value { get; }
        
        
    }
}