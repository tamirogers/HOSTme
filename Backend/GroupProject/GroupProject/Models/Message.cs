using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Message
    {
        //primary key
        public int MessageId { get; set; }
        public int ConversationId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }

        //
        public Conversation Conversation { get; set; }
    }
}