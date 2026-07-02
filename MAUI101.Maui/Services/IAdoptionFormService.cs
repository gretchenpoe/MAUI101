using MAUI101.Maui.Models;

namespace MAUI101.Maui.Services
{
    public interface IAdoptionFormService
    {
        Task AddNewAdoptionForm(AdoptionForm form);
        Task<List<AdoptionForm>> GetAllAdoptionForms();

    }
}