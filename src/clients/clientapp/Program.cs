using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using clientapp;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using clientapp.Infrastructure;
using clientapp.Infrastructure.Contracts;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System;

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

builder.Services.AddScoped(
sp => sp.GetService<IHttpClientFactory>().CreateClient("userApi"));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("oidc", options.ProviderOptions);
    options.AuthenticationPaths.RemoteRegisterPath = "Account/Register";
    //options.ProviderOptions.AdditionalProviderParameters.Add("bypass", "true");
    options.ProviderOptions.RedirectUri = "https://localhost:4001/verifyemail";
});

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

await builder.Build().RunAsync();
