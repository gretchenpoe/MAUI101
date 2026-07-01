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

        public Task<List<Pet>> GetPetsAsync()
        {
            return _restService.GetPetsAsync();
        }

    }
}