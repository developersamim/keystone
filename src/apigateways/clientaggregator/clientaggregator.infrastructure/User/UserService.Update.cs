using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.User
{
    public partial class UserService
    {
        public async Task UpdateProfile(string userId, Dictionary<string, object> userProfile)
        {
            var result = await Client.PutAsJsonAsync($"{ControllerUrl}/{userId}", userProfile);
            ValidateResponse(result);
        }
    }
}
