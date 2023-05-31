using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Models
{
    public class EditMessageRequest
    {
        public int GroupChatId { get; set; }
        public int MessageId { get; set; }
        public string Text { get; set; }
    }
}
