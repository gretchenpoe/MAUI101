using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using MAUI101.Maui.Services;
using MAUI101.Maui.Pages;

namespace MAUI101.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        private async Task ValidateUser()
        {
            if (IsLoading) return;

            var currentPage = Application.Current.Windows.FirstOrDefault().Page;

            if(currentPage == null)
                return;

            try
            {
                IsLoading = true; // Shows spinner
                User user = await _userService.GetUserByUserName(UserName);
                if (user == null)
                {
                    // User does not exist, create new one
                    try { 
                        await _userService.AddNewUser(UserName, Password);
                        Preferences.Default.Set("IsLoggedIn", true);
                        await currentPage.DisplayAlert("Success", "Created a new user.", "OK");
                        
                        // Navigate to the main application
                        currentPage = new AppShell();
                    }
                    catch (Exception ex)
                    {
                        await currentPage.DisplayAlert("Error", "Failed to create new user.", "OK");
                        IsLoading = false; // Hides spinner
                        return;
                    }
                }
                else
                {
                    var isValidUser = await _userService.VerifyUserPassword(UserName, Password);
                    if (isValidUser)
                    {
                        Preferences.Default.Set("IsLoggedIn", true);
                        // Navigate to the main application
                        currentPage = new AppShell();
                    }
                    else
                    {
                        // Show an error message or handle invalid login
                        await currentPage.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
                    }
                }
            }
            finally
            {
                IsLoading = false; // Hides spinner
            }
        }
      
    }
}