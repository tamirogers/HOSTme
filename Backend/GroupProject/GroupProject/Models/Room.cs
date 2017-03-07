using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Room
    {
        //Primary Key
        public int RoomId { get; set; }
        //Foreign Key
        public int UserId { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }
        public bool Private { get; set; }
        public int RoomNumber { get; set; }

        public int GuestLimit { get; set; }
     
        public User User { get; set; }
        //public virtual ICollection<Favorite> Favorites { get; set; }
    }
}