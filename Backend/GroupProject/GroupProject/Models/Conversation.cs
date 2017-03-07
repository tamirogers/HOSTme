using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Conversation
    {
        //Primary Key
        public int ConversationId { get; set; }

        //Foreign Keys    
        public int SenderUserId { get; set; }

        public int ReceiverUserId { get; set; }

        //Mapping
        [ForeignKey("SenderUserId")]
        [InverseProperty("Conversations")]
        public User Sender {get; set;}

        [ForeignKey("ReceiverUserId")]
        //[InverseProperty("ReceiverConversations")]
        public User Receiver { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}