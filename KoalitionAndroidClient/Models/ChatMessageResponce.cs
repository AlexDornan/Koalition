using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Models
{
    public class ChatMessageResponse
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
    }
}
