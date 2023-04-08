using KoalitionAndroidClient.ViewModels.Startup;

namespace KoalitionAndroidClient.Views.Startup;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}