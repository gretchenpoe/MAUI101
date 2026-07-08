using System.Diagnostics;
using MAUI101.Maui.Models;

namespace MAUI101.Maui.Repositories;

public class UserRepository : IUserRepository
{
    private IDbConnectionProvider _dbConnectionProvider;

    public string StatusMessage { get; set; }

    public UserRepository(IDbConnectionProvider dbConnectionProvider)
    {
        _dbConnectionProvider = dbConnectionProvider;                    
    }

    public async Task AddNewUser(User user)
    {            
        var conn = await _dbConnectionProvider.Init();
        int result = await conn.InsertAsync(user);

        StatusMessage = string.Format("{0} record(s) added (UserName: {1})", result, user.UserName);
        Debug.WriteLine(StatusMessage);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        var conn = await _dbConnectionProvider.Init();
        return await conn.Table<User>().Where(u => u.UserName == userName).FirstOrDefaultAsync();
    }
}