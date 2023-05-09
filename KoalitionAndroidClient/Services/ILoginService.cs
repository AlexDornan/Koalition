using KoalitionAndroidClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Authenticate(LoginRequest loginRequest);
        Task<List<GroupChatResponse>> GetGroupChats();
    }
}
