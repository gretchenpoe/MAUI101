using MAUI101.Maui.Models;

namespace MAUI101.Maui.Services;
public interface IRestService
{
    Task<List<Pet>> GetPetsAsync();

    Task<Pet?> GetPetByIdAsync(string id);
}