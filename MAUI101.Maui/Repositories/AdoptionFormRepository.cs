using MAUI101.Maui.Models;

namespace MAUI101.Maui.Repositories;

public class AdoptionFormRepository : IAdoptionFormRepository
{
    private IDbConnectionProvider _dbConnectionProvider;

    public string StatusMessage { get; set; }

    public AdoptionFormRepository(IDbConnectionProvider dbConnectionProvider)
    {
       _dbConnectionProvider = dbConnectionProvider;                       
    }

    public async Task AddNewAdoptionForm(AdoptionForm form)
    {            
        try
        {
            var conn = await _dbConnectionProvider.Init();
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
            var conn = await _dbConnectionProvider.Init();
            return await conn.Table<AdoptionForm>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<AdoptionForm>();
    }
}