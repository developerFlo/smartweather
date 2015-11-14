using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    public class Location
    {
        public Geoposition Position { get; set; }
        public string Name { get; set; }
        public int? CityID { get; set; }
        public string Country { get; set; }
    }
}
