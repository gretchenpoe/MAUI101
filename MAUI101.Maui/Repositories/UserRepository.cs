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
        try
        {
            var conn = await _dbConnectionProvider.Init();
            int result = await conn.InsertAsync(user);

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, user.UserName);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", user.UserName, ex.Message);
        }

    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        try
        {
            var conn = await _dbConnectionProvider.Init();
            return await conn.Table<User>().Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            throw;
        }
    }
}