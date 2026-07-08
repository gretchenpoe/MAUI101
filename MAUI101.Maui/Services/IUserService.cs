using MAUI101.Maui.Models;

namespace MAUI101.Maui.Services;
public interface IUserService
{
    Task AddNewUser(string userName, string password);
    Task<User?> GetUserByUserName(string userName);
    Task<bool> VerifyUserPassword(string userName, string password);
}
