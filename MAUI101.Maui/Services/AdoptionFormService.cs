using MAUI101.Maui.Models;
using MAUI101.Maui.Repositories;

namespace MAUI101.Maui.Services;
public class AdoptionFormService : IAdoptionFormService
{
    IAdoptionFormRepository _repository;

    public AdoptionFormService(IAdoptionFormRepository repository)
    {
        _repository = repository;
    }

    public async Task AddNewAdoptionForm(AdoptionForm form)
    {
        await _repository.AddNewAdoptionForm(form);
    }

    public async Task<List<AdoptionForm>> GetAllAdoptionForms()
    {
        return await _repository.GetAllAdoptionForms();
    }
}