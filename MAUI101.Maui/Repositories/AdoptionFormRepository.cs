using MAUI101.Maui.Models;
using System.Diagnostics;

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
        var conn = await _dbConnectionProvider.Init();
        int result = await conn.InsertAsync(form);

        StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, form.FirstName);
        Debug.WriteLine(StatusMessage);
    }

    public async Task<List<AdoptionForm>> GetAllAdoptionForms()
    {
        var conn = await _dbConnectionProvider.Init();
        return await conn.Table<AdoptionForm>().ToListAsync();
    }
}