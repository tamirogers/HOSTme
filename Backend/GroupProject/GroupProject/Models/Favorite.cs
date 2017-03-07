using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Favorite
    {
        //Compound Key
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
    }
}