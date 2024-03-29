﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Models
{
    public class ChatMessageResponse
    {
        [JsonProperty("messageId")]
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
