using System.Net.Http.Json;
using KoalitionServer.Requests.UserRequests;

namespace KoalitionAndoidClient
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            var regRequest = new RegistrationRequest
            {
                Login = LoginEntry.Text,
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            var client = new HttpClient();
            var response = await client.PostAsJsonAsync("https://your-api-url.com/register", regRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Save token or navigate to authenticated page
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", error, "OK");
            }
        }
    }
}



