using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartweather.Models;
using Windows.Devices.Geolocation;

namespace Smartweather.Sevices
{
    public class GeoService : IGeoService
    {
        public async Task<Models.Geoposition> GetCurrentPosition()
        {
            GeolocationAccessStatus state = await Geolocator.RequestAccessAsync();
            switch (state)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geoLoc = new Geolocator();
                    var position = await geoLoc.GetGeopositionAsync();
                    Models.Geoposition pos;
                    pos.lat = position.Coordinate.Point.Position.Latitude;
                    pos.lon = position.Coordinate.Point.Position.Longitude;
                    return pos;
                case GeolocationAccessStatus.Denied:
                    throw new GeoServiceException(GeoServiceException.Type.AccessDenied);
                default:
                    throw new GeoServiceException(GeoServiceException.Type.AccessUnspecified);
            }

        }
    }
}
