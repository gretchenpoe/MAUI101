using MAUI101.Maui.Models;
using SQLite;

namespace MAUI101.Maui.Repositories;

public interface IDbConnectionProvider
{
    Task<SQLiteAsyncConnection> Init();
}

public class DbConnectionProvider : IDbConnectionProvider
{
    string _dbPath;

    private SQLiteAsyncConnection conn;

    public async Task<SQLiteAsyncConnection> Init()
    {
        if (conn != null)
            return conn;

        _dbPath = FileSystem.AppDataDirectory + Path.DirectorySeparatorChar + "WatsonPughPetAdoption.db3"; 

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<User>();    
        await conn.CreateTableAsync<AdoptionForm>();  

        return conn;
    }
}