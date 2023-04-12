using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Users
{
    public class User
    {
        public int userId { get; set; }
        public string login { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; }
        public string password { get; set; }
    }
}
