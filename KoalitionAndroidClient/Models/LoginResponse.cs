using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserBasicInfo UserDetails { get; set; }
    }
}
