﻿using clientaggregator.application.Contracts.Infrastructure.Customer;
using clientaggregator.infrastructure.Customer;
using clientaggregator.infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace clientaggregator.infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureSerivces(this IServiceCollection services, ApiSetting apiSettings, string clientCredentialsTokenKey)
        {
            services.AddHttpClient<ICustomerService, CustomerServiceApi>(o =>
            {
                o.BaseAddress = new Uri(apiSettings.CustomerApi);
                o.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(apiSettings.HandlerLifetimeMinutes))
                .AddClientAccessTokenHandler(clientCredentialsTokenKey);

            return services;
        }
    }
}
