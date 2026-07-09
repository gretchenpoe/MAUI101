using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI101.Maui.Models;
using MAUI101.Maui.Services;
using System.Diagnostics;

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
                User? user = await _userService.GetUserByUserName(UserName);
                if (user == null)
                {
                    // User does not exist, create new one
                    try 
                    { 
                        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                        {
                            await currentPage.DisplayAlertAsync("Error", "Please fill out all fields.", "OK");
                            return;
                        }
                        await _userService.AddNewUser(UserName, Password);
                        await currentPage.DisplayAlertAsync("Success", "Created a new user.", "OK");

                        // Navigate to the main application
                        Application.Current.Windows.FirstOrDefault().Page = new AppShell();
                    }
                    catch (Exception ex)
                    {
                        await currentPage.DisplayAlertAsync("Error", "Failed to create new user.", "OK");
                        Debug.WriteLine($"Failed to create new user. Exception: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    var isValidUser = await _userService.VerifyUserPassword(UserName, Password);
                    if (isValidUser)
                    {
                        // Navigate to the main application
                       Application.Current.Windows.FirstOrDefault().Page = new AppShell();
                    }
                    else
                    {
                        // Show an error message or handle invalid login
                        await currentPage.DisplayAlertAsync("Login Failed", "Invalid username or password.", "OK");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await currentPage.DisplayAlertAsync("Error", "Failed to get user data", "OK");
                Debug.WriteLine($"Failed to get user data. Exception: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Hides spinner
            }
        }
      
    }
}