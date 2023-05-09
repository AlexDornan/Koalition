using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Models
{
    [JsonObject]
    public class GroupChatResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
