using MAUI101.Maui.Models;

namespace MAUI101.Maui.Services
{
    public class PetService : IPetService
    {
        IRestService _restService;

        public PetService(IRestService service)
        {
            _restService = service;
        }

        public async Task<List<Pet>> GetPetsAsync()
        {
            return await _restService.GetPetsAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(string id)
        {
            return await _restService.GetPetByIdAsync(id);
        }

    }
}