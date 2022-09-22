using System;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace common.api.KeyVault;

public static class KeyVaultExtension
{
    public static IHostBuilder ConfigureKeyVault(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, builder) =>
        {
            if (ctx.HostingEnvironment.IsProduction())
            {
                var config = builder.Build();
                var vaultUrl = $"https://{config["KeyVault:VaultName"]}.vault.azure.net/";
                Log.Information($"Key Vault Url: {vaultUrl}");
                var secretClient = new SecretClient(
                    new Uri(vaultUrl),
                    new DefaultAzureCredential());

                builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            }
        });

        return builder;
    }
}

