using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using System.ComponentModel.DataAnnotations;

namespace MAUI101.Maui.ViewModels;

public partial class AdoptionDetailsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private AdoptionDetails _adoptionDetails;


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

        // Process data
        // save to local storage
        string message = $"Name: {AdoptionDetails.FirstName} {AdoptionDetails.LastName}\nEmail: {AdoptionDetails.Email}";
        await Shell.Current.DisplayAlert("Form Submitted", message, "Success");

        
        // Navigate back to the previous page
    }

    private async Task<bool> ValidateForm()
    {
        var context = new ValidationContext(AdoptionDetails, serviceProvider: null, items: null);
        var addressContext = new ValidationContext(AdoptionDetails.Address, serviceProvider: null, items: null);
        
        var results = new List<ValidationResult>();
        var resultAddress = new List<ValidationResult>();

        // Manually trigger the validation engine
        bool isValid = Validator.TryValidateObject(AdoptionDetails, context, results, validateAllProperties: true);

        bool isValidAddress = Validator.TryValidateObject(AdoptionDetails.Address, addressContext, resultAddress, validateAllProperties: true);

        if (!isValid || !isValidAddress)
        {
            await Shell.Current.DisplayAlert("Please fix the following validation errors:", "\n " + string.Join("\n ", results.Select(r => r.ErrorMessage)) 
                + "\n " + string.Join("\n ", resultAddress.Select(r => r.ErrorMessage)), "OK");
            return false;
        }
        return true;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Pet", out var value) && value is Pet pet)
        {
            // The data is successfully mapped here right as the page loads
            AdoptionDetails = new AdoptionDetails
            {
                Pet = pet
            };
        }
    }
}
