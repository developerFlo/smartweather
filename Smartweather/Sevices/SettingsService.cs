using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartweather.Models;
using Windows.Storage;
using System.Diagnostics;

namespace Smartweather.Sevices
{
    public class SettingsService : ISettingsService
    {
        //RecentCities
        public void AddRecentCity(int cityID)
        {
            IList<int> recentCities = GetRecentCities();
            if (!recentCities.Contains(cityID))
            {
                recentCities.Add(cityID);
            }
            SaveRecentCities(recentCities);
        }

        public IList<int> GetRecentCities()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var recentCities = localSettings.Values["recentCities"];
            if(recentCities == null || !(recentCities is int[]))
            {
                return new List<int>();
            }
            else
            {
                return ((int[])recentCities).ToList();
            }
        }

        public void RemoveRecentCity(int cityID)
        {
            IList<int> recentCities = GetRecentCities();
            int itemIndex = recentCities.IndexOf(cityID);
            if (itemIndex >= 0)
            {
                recentCities.RemoveAt(itemIndex);
                SaveRecentCities(recentCities);
            }
        }

        private void SaveRecentCities(IEnumerable<int> recentCities)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (recentCities == null || recentCities.Count() == 0)
            {
                localSettings.Values.Remove("recentCities");
            }
            else
            {
                localSettings.Values["recentCities"] = recentCities.ToArray();
            }
        }

        //CurrentCity
        public Location GetCurrentCityLocation()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("currentCityId"))
            {
                return new Location()
                {
                    CityID = (int)localSettings.Values["currentCityId"],
                    Name = (string)localSettings.Values["currentCityName"],
                    Position = new Geoposition()
                    {
                        lat = (double)localSettings.Values["currentCityLat"],
                        lon = (double)localSettings.Values["currentCityLon"]
                    }
                };
            }
            else
            {
                return null;
            }
        }
        public void SetCurrentCityLocation(Location location)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (location != null) {

                if (!location.CityID.HasValue || location.Name == null)
                {
                    Debug.WriteLine("CurrentCityLocation not saved: Name or CityId not defined");
                    //Zum Speichern des Ortes muss dieser immer vollständig sein
                    //-> Ansonsten wird er das nächste mal einfach per GEO-Position ermittelt
                    return;
                }
                localSettings.Values["currentCityId"] = location.CityID.Value;
                localSettings.Values["currentCityName"] = location.Name;
                localSettings.Values["currentCityLat"] = location.Position.lat;
                localSettings.Values["currentCityLon"] = location.Position.lon;
            }
            else
            {
                localSettings.Values.Remove("currentCityId");
                localSettings.Values.Remove("currentCityName");
                localSettings.Values.Remove("currentCityLat");
                localSettings.Values.Remove("currentCityLon");
            }
        }

        //NearbyWeather

        public WeatherGroup GetNearbyWeather(WeatherGroup defaultWeatherGroup)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var nearbyWeather = localSettings.Values["nearbyWeather"];
            if(nearbyWeather == null)
            {
                return defaultWeatherGroup;
            }
            else
            {
                return (WeatherGroup)nearbyWeather;
            }
        }
        
        public void SetNearbyWeather(WeatherGroup weatherGroup)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["nearbyWeather"] = (uint)weatherGroup;
        }
    }
}
