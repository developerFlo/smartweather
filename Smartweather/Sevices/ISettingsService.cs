using Smartweather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface ISettingsService
    {
        //Zuletzt verwendete Orte
        IList<int> GetRecentCities();
        void AddRecentCity(int cityID);
        void RemoveRecentCity(int cityID);

        //Aktueller Ort zur Wetter-Vorhersage
        void SetCurrentCityLocation(Location location);
        Location GetCurrentCityLocation();


        //Zuletzt gewählte Wetter-Typ-Einschränkung für das Umgebungswetter
        void SetNearbyWeather(WeatherGroup weatherGroup);
        WeatherGroup GetNearbyWeather(WeatherGroup defaultWeatherGroup);
    }
}
