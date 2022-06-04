using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.Customer
{
    public partial class CustomerServiceApi
    {
        public async Task DeleteProfileElements(string userId, List<string> keys)
        {
            var url = $"{ControllerUrl}/{userId}";
            url = QueryHelpers.AddQueryString(url, "keys", string.Join(",", keys));

            await Client.DeleteAsync(url);
        }
    }
}
