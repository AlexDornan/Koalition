using KoalitionAndroidClient.Models;
using KoalitionAndroidClient.Services;
using KoalitionAndroidClient.Views.Menu;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _password;

        private readonly ILoginService _loginService;
        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }
        #region Commands
        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= new Command(Logining);
        async void Logining()
        {
            Debug.WriteLine("Logining method started");

            if (!string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password))
            {
                Debug.WriteLine("Login and password are not null or whitespace");

                // calling api 
                var response = await _loginService.Authenticate(new LoginRequest
                {
                    Login = Login,
                    Password = Password
                });

                if (response != null)
                {
                    Debug.WriteLine("Authentication succeeded");

                    response.UserDetails.Login = Login;

                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    string userDetailStr = JsonConvert.SerializeObject(response.UserDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = response.UserDetails;
                    App.Token = response.Token;
                    await Shell.Current.GoToAsync($"//{nameof(MenuPage)}");
                }
                else
                {
                    Debug.WriteLine("Authentication failed");

                    await AppShell.Current.DisplayAlert("Invalid Login Or Password", "Invalid Login or Password", "OK");
                }
            }
            else
            {
                Debug.WriteLine("Login or password are null or whitespace");
            }
        }
        #endregion
    }
}
