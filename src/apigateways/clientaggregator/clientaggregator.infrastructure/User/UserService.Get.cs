using clientaggregator.application.Contracts.Models.Profile;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace clientaggregator.infrastructure.User
{
    public partial class UserService
    {
        public async Task<CustomerProfileDto> GetProfile(string userId)
        {
            var keyValuePairs = new Dictionary<string, string>
            {
                { "userId", userId }
            };
            var url = QueryHelpers.AddQueryString($"{ControllerUrl}", keyValuePairs);

            var response = await Client.GetAsync(url);
            var result = await ValidateResponse<CustomerProfileDto>(response);
            return result;
        }

        public async Task<CustomerProfileDto> GetUserByClaims(Dictionary<string, string> claims)
        {
            var user = new CustomerProfileDto();

            var url = $"{ControllerUrl}/claims";
            url = QueryHelpers.AddQueryString(url, claims);

            var response = await Client.GetAsync(url);
            ValidateResponse(response);

            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
                user = await response.Content.ReadFromJsonAsync<CustomerProfileDto>();

            return user;
        }
    }
}
