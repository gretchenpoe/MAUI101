using MAUI101.Maui.Models;
using SQLite;

namespace MAUI101.Maui.Repositories;

public class UserRepository : IUserRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<User>();    
    }

    public UserRepository()
    {
        _dbPath = FileSystem.AppDataDirectory + Path.DirectorySeparatorChar + "WatsonPughPetAdoption.db3";                        
    }

    public async Task AddNewUser(User user)
    {            
        try
        {
            await Init();
            int result = await conn.InsertAsync(user);

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, user.UserName);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", user.UserName, ex.Message);
        }

    }

    public async Task<User> GetUserByUserName(string userName)
    {
        try
        {
            await Init();
            return await conn.Table<User>().Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new User();
    }
}