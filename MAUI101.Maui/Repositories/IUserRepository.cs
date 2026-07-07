using MAUI101.Maui.Models;
using SQLite;

namespace MAUI101.Maui.Repositories;

public interface IUserRepository
{
    Task AddNewUser(User user);
    Task<User> GetUserByUserName(string userName);
}