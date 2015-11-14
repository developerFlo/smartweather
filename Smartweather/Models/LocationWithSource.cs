using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class LocationWithSource
    {
        public LocationSource Source
        {
            get; set;
        }

        public Location Location
        {
            get; set;
        }
    }
}
