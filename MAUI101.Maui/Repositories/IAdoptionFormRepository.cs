using MAUI101.Maui.Models;
using SQLite;

namespace MAUI101.Maui.Repositories;

public interface IAdoptionFormRepository
{
    Task AddNewAdoptionForm(AdoptionForm form);
    Task<List<AdoptionForm>> GetAllAdoptionForms();
}