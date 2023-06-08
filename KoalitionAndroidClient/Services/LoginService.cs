using KoalitionAndroidClient.Helpers;
using KoalitionAndroidClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace KoalitionAndroidClient.Services
{
    public class LoginService : ILoginService
    {

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            using (var client = new HttpClient())
            {
                string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

                var response = await client.PostAsync($"{ApiPlatformUrlHelper.GetPlatformApiUrl()}/api/Users/Login",
                    new StringContent(loginRequestStr, Encoding.UTF8, "application/json"));

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(json);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<GroupChatResponse>> GetGroupChats()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
                var response = await client.GetAsync($"{ApiPlatformUrlHelper.GetPlatformApiUrl()}/api/GroupChat/getChats");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<GroupChatResponse>>(json);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
