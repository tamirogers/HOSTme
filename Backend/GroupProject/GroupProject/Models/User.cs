using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Zip { get; set; }
        public string ContactPhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }


    }
}