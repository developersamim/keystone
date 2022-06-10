using clientapp.Models;

namespace clientapp.Infrastructure.Contracts;

public interface IUserService
{
    Task<List<UserProfileDto>> GetUsers();
    Task UpdateProfile(UserUpdateProfileDto request);
}
