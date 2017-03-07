using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class SearchRoom
    {
        public string City { get; set; }
        public string Zip { get; set; }
        public int MaxPrice { get; set; }
        public int GuestLimit { get; set; }
        public string KeyWord { get; set; }
        public bool Private { get; set; }




    }
}