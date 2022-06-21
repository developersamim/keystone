using clientaggregator.application.Contracts.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Contracts.Infrastructure.User
{
    public interface IUserService
    {
        Task<CustomerProfileDto> GetProfile(string userId);
        Task UpdateProfile(string userId, Dictionary<string, object> userProfile);
        Task DeleteProfileElements(string userId, List<string> keys);
        Task<CustomerProfileDto> GetUserByClaims(Dictionary<string, string> claims);
        Task<IEnumerable<CustomerProfileDto>> GetUsers();
    }
}
