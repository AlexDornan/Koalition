using KoalitionAndroidClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
         {
            using(var client = new HttpClient())
            {
                string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

                var response = await client.PostAsync("http://10.0.2.2:5127/api/Users/Login",
                    new StringContent(loginRequestStr, Encoding.UTF8,
                    "application/json"));


                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(json);
                }
                else { return null; }

            }
        }

        public async Task<List<GroupChatResponse>> GetGroupChats()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
                var response = await client.GetAsync("http://10.0.2.2:5127/api/GroupChat/getChats");

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
