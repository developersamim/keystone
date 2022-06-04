using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.User
{
    public partial class UserService
    {
        public async Task DeleteProfileElements(string userId, List<string> keys)
        {
            var url = $"{ControllerUrl}/{userId}";
            url = QueryHelpers.AddQueryString(url, "keys", string.Join(",", keys));

            await Client.DeleteAsync(url);
        }
    }
}
