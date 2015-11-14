using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class LocationSource
    {
        public char Icon { get; set; }

        public static LocationSource Geo = new LocationSource()
        {
            Icon = '\xE81D'
        };

        public static LocationSource Calendar = new LocationSource()
        {
            Icon = '\xE787'
        };

        public static LocationSource Recent = new LocationSource()
        {
            Icon = '\xE734'
        };

        public static LocationSource Search = new LocationSource()
        {
            Icon = '\xE11A'
        };
    }
}
