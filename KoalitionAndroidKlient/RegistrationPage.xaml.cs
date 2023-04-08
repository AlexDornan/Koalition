using System.Net.Http.Headers;
using System.Text.Json;

public partial class RegistrationPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://yourapi.com/";

    public RegistrationPage()
    {
        InitializeComponent();
        Title = "Register";
    }

    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;
        var email = EmailEntry.Text;

        var content = new StringContent(JsonSerializer.Serialize(new { username, password, email }));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync(BaseUrl + "api/user", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Success", "Your account has been created!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "An error occurred. Please try again.", "OK");
        }
    }
}
