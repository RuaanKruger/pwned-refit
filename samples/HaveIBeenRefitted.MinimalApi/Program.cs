using HaveIBeenRefitted;
using HaveIBeenRefitted.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services.AddHaveIBeenRefitted(
    o =>
    {
        o.UserAgent = "USer Agent";
        o.BaseUrl = "https://haveibeenpwned.com";
        o.ApiKey = "IPIkey";
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