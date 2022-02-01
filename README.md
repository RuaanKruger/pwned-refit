![](https://raw.githubusercontent.com/RuaanKruger/pwned-refit/main/logo.png)
# pwned-refit
Combining [Refit](https://reactiveui.github.io/refit/) and ["Have I been Pwned"](https://haveibeenpwned.com/).

## What is this project?
[Refit](https://reactiveui.github.io/refit/) is an amazing library and is my preferred way of communicating with API's where I can't generate the client from OpenAPI specifications (via [NSwag](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)).

My team and I needed to connect to a few API where we didn't have a specification, so this is an ideal example for them to look at.

## Concepts
### Refit 
Here's a simplified example of a client:
```csharp
public interface IBreachClient
{
    [Get("/api/v3/breachedaccount/{account}")]
    Task<BreachDetail[]> GetAllBreachesForAccount(string account, 
        bool truncateResponse = false, 
        string domain = null, 
        bool includeUnverified = false);
    
    [Get("/api/v3/breaches")]
    Task<BreachDetail[]> GetBreaches(string domain = default);
}
```

See full source for [IBreachClient](https://github.com/RuaanKruger/pwned-refit/blob/main/src/HaveIBeenRefitted/IBreachClient.cs)

### Delegating Handler
Delegating handlers are used to provide the HIBP token and user agent to each request. Although you could use similar attributes inside your interface (as per the example below), I prefer the handler approach.  
  
```csharp
[Headers("User-Agent: HaveIBeenRefitted Client")]
public interface IBreachClient
{
    [Get("/api/v3/breaches")]
    Task<BreachDetail[]> GetBreaches(string domain = default);
}
```
  
See full source for [UserAgentDelegatingHandler](https://github.com/RuaanKruger/pwned-refit/blob/main/src/HaveIBeenRefitted/DelegatingHandlers/UserAgentDelegatingHandler.cs) or [TokenProviderDelegatingHandler](https://github.com/RuaanKruger/pwned-refit/blob/main/src/HaveIBeenRefitted/DelegatingHandlers/TokenProviderDelegatingHandler.cs).

## How to use
Simply initialize the service via the provided service extension method:
```csharp
var services = builder.Services.AddHaveIBeenRefitted(
    o =>
    {
        o.UserAgent = "Have I Been Refitted";
        o.BaseUrl = "https://haveibeenpwned.com";
    });
```

I have included a [minimal API](https://github.com/RuaanKruger/pwned-refit/blob/main/samples/HaveIBeenRefitted.MinimalApi/Program.cs) that should hopefully explain everything!