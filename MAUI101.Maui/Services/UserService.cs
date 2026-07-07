using MAUI101.Maui.Models;
using MAUI101.Maui.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MAUI101.Maui.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IUserRepository repository, IPasswordHelper passwordHelper)
        {
            _repository = repository;
            _passwordHelper = passwordHelper;
        }

        public async Task AddNewUser(string userName, string password)
        {
            var user = new User
            {
                UserName = userName
            };

            user.PasswordHash = _passwordHelper.HashPassword(user, password);
            await _repository.AddNewUser(user);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _repository.GetUserByUserName(userName);
        }

        public async Task<bool> VerifyUserPassword(string userName, string password)
        {
            var user = await _repository.GetUserByUserName(userName);
            
            if (user == null)
                return false;

            return _passwordHelper.VerifyPassword(user, user.PasswordHash, password);
        }

    }
}