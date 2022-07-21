using clientapp.Models;

namespace clientapp.Infrastructure.Contracts;

public interface IUserService
{
    Task<List<UserProfileDto>> GetUsers();
    Task<HttpResponseMessage> UpdateProfile(UserUpdateProfileDto request);
    Task<UserProfileDto> GetProfile();
}
