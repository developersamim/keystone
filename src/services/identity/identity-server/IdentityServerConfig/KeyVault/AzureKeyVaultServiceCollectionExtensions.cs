using System;
using IdentityServer.IdentityServerConfig.KeyVault;
using IdentityServer4.Stores;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace identity_server.IdentityServerConfig.KeyVault;

public static class AzureKeyVaultServiceCollectionExtensions
{
    public static IServiceCollection AddKeyVaultSigningCredentials(this IServiceCollection @this)
    {
        var azureServiceTokenProvider = new AzureServiceTokenProvider();
        var authenticationCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
        var keyVaultClient = new KeyVaultClient(authenticationCallback);
        @this.AddMemoryCache();
        @this.AddSingleton(keyVaultClient);
        @this.AddSingleton<AzureKeyVaultSigningCredentialsStore>();
        @this.AddSingleton<ISigningCredentialStore>(services => services.GetRequiredService<AzureKeyVaultSigningCredentialsStore>());
        @this.AddSingleton<IValidationKeysStore>(services => services.GetRequiredService<AzureKeyVaultSigningCredentialsStore>());
        return @this;
    }
}

