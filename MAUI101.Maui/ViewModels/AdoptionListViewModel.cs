using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using MAUI101.Maui.Services;

namespace MAUI101.Maui.ViewModels
{
    public partial class AdoptionListViewModel : ObservableObject
    {
         private readonly IPetService _petService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Pet> _pets;

        public AdoptionListViewModel(IPetService petService)
        {
            _petService = petService;
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            if (IsLoading) return;

            if(Pets != null && Pets.Count > 0) {
                IsLoading = false;
                return;
            }

            try
            {
                IsLoading = true; // Shows spinner
                Pets = await _petService.GetPetsAsync();
            }
            finally
            {
                IsLoading = false; // Hides spinner
            }
        }
      
    }
}