using System;
namespace identity_server.IdentityServerConfig.KeyVault;

public class KeyVaultConfig
{
    public string KeyVaultName { get; set; }
    public string KeyVaultCertificateName { get; set; }
    public int KeyVaultRolloverHours { get; set; }
}

