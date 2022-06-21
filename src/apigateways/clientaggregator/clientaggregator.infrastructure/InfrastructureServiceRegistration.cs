using clientaggregator.application.Contracts.Infrastructure.User;
using clientaggregator.infrastructure.Settings;
using clientaggregator.infrastructure.User;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace clientaggregator.infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureSerivces(this IServiceCollection services, ApiSetting apiSettings, string clientCredentialsTokenKey)
        {
            services.AddHttpClient<IUserService, UserService>(o =>
            {
                o.BaseAddress = new Uri(apiSettings.UserApi);
                o.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(apiSettings.HandlerLifetimeMinutes))
                .AddClientAccessTokenHandler(clientCredentialsTokenKey);

            services.AddHttpClient<IVerifyEmailService, VerifyEmailService>(o =>
            {
                o.BaseAddress = new Uri(apiSettings.UserApi);
                o.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(apiSettings.HandlerLifetimeMinutes))
                .AddClientAccessTokenHandler(clientCredentialsTokenKey);

            return services;
        }
    }
}
