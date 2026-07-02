using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using System.ComponentModel.DataAnnotations;
using MAUI101.Maui.Services;

namespace MAUI101.Maui.ViewModels;

public partial class AdoptionFormViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPetService _petService;
    private readonly IAdoptionFormService _adoptionFormService;
    public AdoptionFormViewModel(IPetService petService, IAdoptionFormService adoptionFormService)
    {
        _petService = petService;
        _adoptionFormService = adoptionFormService;
    }

    [ObservableProperty]
    private AdoptionForm _adoptionDetails;

    [ObservableProperty]
    private Pet _petDetails;

    [ObservableProperty]
    private bool _isReadonly = false;


    [ObservableProperty]
    private DateTime _maxDate = DateTime.Today;

    [ObservableProperty]
    private List<string> _statesList = new List<string>   {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };


    [RelayCommand]
    private async Task SubmitForm()
    {
        var isValid = await ValidateForm();
        if (!isValid)
            return;

        // Save to local storage
        await _adoptionFormService.AddNewAdoptionForm(AdoptionDetails);

        string message = $"Name: {AdoptionDetails.FirstName} {AdoptionDetails.LastName}\nEmail: {AdoptionDetails.Email}";
        await Shell.Current.DisplayAlert("Form Submitted", message, "Success");

        Shell.Current.GoToAsync("..");
        // Navigate back to the previous page
    }

    private async Task<bool> ValidateForm()
    {
        var context = new ValidationContext(AdoptionDetails, serviceProvider: null, items: null);
        
        var results = new List<ValidationResult>();
        var resultAddress = new List<ValidationResult>();

        // Manually trigger the validation engine
        bool isValid = Validator.TryValidateObject(AdoptionDetails, context, results, validateAllProperties: true);

        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Validation Errors", string.Join("\n", results.Select(r => r.ErrorMessage)), "OK");
            return false;
        }
        return true;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Readonly of existing adoption form scenario
        if (query.TryGetValue("AdoptionForm", out var formValue) && formValue is AdoptionForm form)
        {
            AdoptionDetails = form;
            PetDetails = _petService.GetPetByIdAsync(form.PetId).Result;
            IsReadonly = true;
        }
    
        // Create adoption form scenario
        if (query.TryGetValue("Pet", out var petValue) && petValue is Pet pet)
        {
            AdoptionDetails = new AdoptionForm
            {
                PetId = pet.ID
            };
            PetDetails = pet;
        }
    }
}
