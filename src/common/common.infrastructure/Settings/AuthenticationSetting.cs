using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.infrastructure.Settings;

public class AuthenticationSetting
{
    public string Authority { get; set; }

    /// <summary>
    /// Only required in EnableIntrospection is true or if Client is authenticating against another api
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Only required in EnableIntrospection is true or if Client is authenticating against another api
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// Only required if Client is authenticating against another api
    /// </summary>
    public string Scopes { get; set; }

    public string TokenEndpoint { get; set; }
}
