using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class DBLocation
    {
        [PrimaryKey]
        public int CityID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        static DBLocation FromLocation(Location location)
        {
            return new DBLocation()
            {
                CityID = location.CityID.Value,
                Name = location.Name,
                Lat = location.Position.lat,
                Lon = location.Position.lon,
                Country = location.Country
            };
        }

        public Location ToLocation()
        {
            return new Location()
            {
                CityID = this.CityID,
                Name = this.Name,
                Position = new Geoposition()
                {
                    lat = this.Lat,
                    lon = this.Lon
                },
                Country = this.Country
            };
        }
    }
}
