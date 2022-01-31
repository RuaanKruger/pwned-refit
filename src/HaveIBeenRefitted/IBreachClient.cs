using System.Threading.Tasks;
using HaveIBeenRefitted.Responses;
using Refit;

namespace HaveIBeenRefitted
{
    /// <summary>
    /// Breaches For Account
    /// </summary>
    /// <remarks>
    /// End-point: <a href="https://haveibeenpwned.com/api/v3/#BreachesForAccount"></a>
    /// </remarks>
    public interface IBreachClient
    {
        /// <summary>
        /// The API takes a single parameter which is the account to be searched for. The account is not case sensitive and will be
        /// trimmed of leading or trailing white spaces. The account should always be URL encoded.
        /// See <a href="https://haveibeenpwned.com/API/v3#BreachesForAccount"></a>
        /// </summary>
        /// <param name="account">The account to search for breaches.</param>
        /// <param name="truncateResponse">Returns the full breach model when explicitly set to false</param>
        /// <param name="domain">Filters the result set to only breaches against the domain specified.</param>
        /// <param name="includeUnverified">Returns breaches that have been flagged as "unverified"</param>
        /// <returns>An array of breach details if found, or an empty array if not found.</returns>
        [Get("/api/v3/breachedaccount/{account}")]
        Task<BreachDetail[]> GetAllBreachesForAccount(string account, 
            bool truncateResponse = false, 
            string domain = null, 
            bool includeUnverified = false);
        
        /// <summary>
        /// Gets all breaches.
        /// </summary>
        /// <param name="domain">An optional domain to filter the returned breaches to.</param>
        /// <returns>A collection of breaches.</returns>
        /// <remarks>
        /// Example JSON payload: <a href="https://haveibeenpwned.com/api/v3/breaches"></a>
        /// </remarks>
        [Get("/api/v3/breaches")]
        Task<BreachDetail[]> GetBreaches(string domain = default);
    }
}