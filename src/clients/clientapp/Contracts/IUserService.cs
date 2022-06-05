using clientapp.Models;

namespace clientapp.Contracts;

public interface IUserService
{
    Task<List<UserProfileDto>> GetUsers();
    Task UpdateProfile(UserUpdateProfileDto request);
}
