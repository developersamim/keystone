using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using clientapp;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using clientapp.Services;
using clientapp.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IUserService, UserService>
    (client =>
    {
        client.BaseAddress = new Uri("http://localhost:9000");
    })
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

//builder.Services.AddScoped(
//sp => sp.GetService<IHttpClientFactory>().CreateClient("userApi"));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("oidc", options.ProviderOptions);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
