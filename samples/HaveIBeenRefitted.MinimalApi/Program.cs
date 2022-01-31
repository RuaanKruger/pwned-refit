using HaveIBeenRefitted;
using HaveIBeenRefitted.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services.AddHaveIBeenRefitted(
    o =>
    {
        o.UserAgent = "Have I Been Refitted";
        o.BaseUrl = "https://haveibeenpwned.com";
        // Not setting ApiKey so we can make use of our environment variable
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new()
    {
        Title = "HaveIBeenRefitted.MinimalApi", 
        Version = "v1"
    }));

await using var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "HaveIBeenRefitted.MinimalApi v1"));
}

app.MapGet("/", () => "Hello World!");
app.MapGet("api/breaches/{breachName}", (string breachName, IBreachClient client) => client.GetBreaches(breachName));

await app.RunAsync();