using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weatherAPI.Models
{
    public class Location
    {
        public string title { get; set; }
        public string location_type { get; set; }
        public int woeid { get; set; }
        public string latt_long { get; set; }
    }
}
