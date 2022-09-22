using System;
namespace identity_server.IdentityServerConfig;

public class KeyVaultSetting
{
    public string VaultName { get; set; }
    public string SigningCertName { get; set; }
    public int RolloverHours { get; set; }

    public string VaultUrl => $"https://{VaultName}.vault.azure.net/";
}

