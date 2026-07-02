using MAUI101.Maui.Models;
using SQLite;

namespace MAUI101.Maui.Repositories;

public class AdoptionFormRepository : IAdoptionFormRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<AdoptionForm>();    
    }

    public AdoptionFormRepository()
    {
        _dbPath = FileSystem.AppDataDirectory + Path.DirectorySeparatorChar + "AdoptionForms.db3";                        
    }

    public async Task AddNewAdoptionForm(AdoptionForm form)
    {            
        try
        {
            await Init();
            int result = await conn.InsertAsync(form);

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, form.FirstName);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", form.FirstName, ex.Message);
        }

    }

    public async Task<List<AdoptionForm>> GetAllAdoptionForms()
    {
        try
        {
            await Init();
            return await conn.Table<AdoptionForm>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<AdoptionForm>();
    }
}