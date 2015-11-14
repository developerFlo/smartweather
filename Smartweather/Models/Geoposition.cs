using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    public struct Geoposition
    {
        public double lon;
        public double lat;

        public static readonly Geoposition EMPTY = new Geoposition() { lat = 0, lon = 0 };
    }
}
