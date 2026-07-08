using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using System.ComponentModel.DataAnnotations;
using MAUI101.Maui.Services;
using System.Diagnostics;

namespace MAUI101.Maui.ViewModels;

public partial class AdoptionFormViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPetService _petService;
    private readonly IAdoptionFormService _adoptionFormService;
    
    [ObservableProperty]
    private bool _isLoading;

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
    private bool _isFormEnabled = true;


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

        try
        { 
            // Save to local storage
            await _adoptionFormService.AddNewAdoptionForm(AdoptionDetails);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to save adoption form", "OK");
            Debug.WriteLine($"Failed to save adoption form. Exception: {ex.Message}");
            return;
        }

        await Shell.Current.DisplayAlert("Form submitted successfully", "", "OK");

        // Navigate back to the previous page
        await Shell.Current.GoToAsync("..");
    }

    private async Task<bool> ValidateForm()
    {
        var context = new ValidationContext(AdoptionDetails, serviceProvider: null, items: null);
        
        var results = new List<ValidationResult>();

        // Manually trigger the validation engine
        bool isValid = Validator.TryValidateObject(AdoptionDetails, context, results, validateAllProperties: true);

        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Validation Errors", string.Join("\n", results.Select(r => r.ErrorMessage)), "OK");
            return false;
        }
        return true;
    }

    [RelayCommand]
    private async Task LoadPetDataAsync()
    {
        if (IsLoading) return;

        if(PetDetails != null) {
            IsLoading = false;
            return;
        }

        try
        {
            IsLoading = true; // Shows spinner
            PetDetails = await _petService.GetPetByIdAsync(AdoptionDetails.PetId);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to get pet data for adoption form", "OK");
            Debug.WriteLine($"Failed to get pet data for adoption form. Exception: {ex.Message}");
            return;
        }
        finally
        {
            IsLoading = false; // Hides spinner
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Readonly of existing adoption form scenario
        if (query.TryGetValue("AdoptionForm", out var formValue) && formValue is AdoptionForm form)
        {
            AdoptionDetails = form;
            IsFormEnabled = false;
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
