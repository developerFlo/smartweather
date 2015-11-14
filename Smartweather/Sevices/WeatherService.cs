using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartweather.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Smartweather.Sevices
{
    public class WeatherService : IWeatherService
    {
        static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        static readonly Uri BASE_URL = new Uri("http://api.openweathermap.org/data/2.5/");
        const string API_KEY = "cdff7a81fad7226b6e3813f6b5a98620";

        private string GetLangForApi()
        {
            return Windows.Globalization.ApplicationLanguages.Languages.First().Split('-')[0];
        }

        public Task<IList<DayWeather>> GetUpcomingWeatherForLocation(Location location)
        {
            if (location.CityID.HasValue)
            {
                return GetUpcomingWeatherForCityId(location.CityID.Value);
            }
            else
            {
                return GetUpcomingWeatherForPosition(location.Position);
            }
        }

        private Task<IList<DayWeather>> GetUpcomingWeatherForCityId(int cityId)
        {
            Uri serviceUrl = new Uri(BASE_URL, $"forecast/daily?id={cityId}&units=metric&lang={GetLangForApi()}&appid={API_KEY}");
            return GetCityWeatherData(serviceUrl);
        }

        private Task<IList<DayWeather>> GetUpcomingWeatherForPosition(Geoposition position)
        {
            Uri serviceUrl = new Uri(BASE_URL, $"forecast/daily?lat={position.lat}&lon={position.lon}&units=metric&lang={GetLangForApi()}&appid={API_KEY}");
            return GetCityWeatherData(serviceUrl);
        }

        private async Task<IList<DayWeather>> GetCityWeatherData(Uri serviceUrl)
        {
            Debug.WriteLine($"Start download weather from: {serviceUrl.AbsoluteUri}");
            string jsonStr = await ServiceManager.DownloadService.Load(serviceUrl);

            IList<DayWeather> weather = new List<DayWeather>();
            JObject json = JObject.Parse(jsonStr);
            Location location = new Location()
            {
                CityID = json["city"]["id"].Value<int?>(),
                Name = json["city"]["name"].Value<string>(),
                Position = new Geoposition()
                {
                    lat = json["city"]["coord"]["lat"].Value<double>(),
                    lon = json["city"]["coord"]["lon"].Value<double>()
                }
            };
            foreach (var day in json["list"])
            {
                weather.Add(new DayWeather()
                {
                    Location = location,
                    Day = FromIntervalSince1970(day["dt"].Value<double>()),
                    Code = day["weather"][0]["id"].Value<int>().ToString(),
                    Name = day["weather"][0]["main"].Value<string>(),
                    Description = day["weather"][0]["description"].Value<string>(),
                    Temp = day["temp"]["day"].Value<double>()
                });
            }

            return weather;
        }

        public async Task<IList<DayWeather>> GetWeatherAroundPosition(Geoposition position, int maxCnt)
        {
            Uri serviceUrl = new Uri(BASE_URL, $"find?lat={position.lat}&lon={position.lon}&cnt={maxCnt}&units=metric&lang={GetLangForApi()}&appid={API_KEY}");
            Debug.WriteLine($"Start download weather from: {serviceUrl.AbsoluteUri}");
            string jsonStr = await ServiceManager.DownloadService.Load(serviceUrl);

            JObject json = JObject.Parse(jsonStr);
            HashSet<string> incluedCityNames = new HashSet<string>();
            IList<DayWeather> weather = new List<DayWeather>();

            foreach (var city in json["list"])
            {
                Location location = new Location()
                {
                    CityID = city["id"].Value<int?>(),
                    Name = city["name"].Value<string>(),
                    Position = new Geoposition()
                    {
                        lat = city["coord"]["lat"].Value<double>(),
                        lon = city["coord"]["lon"].Value<double>()
                    }
                };
                if (!incluedCityNames.Contains(location.Name))
                {
                    weather.Add(new DayWeather()
                    {
                        Location = location,
                        Day = FromIntervalSince1970(city["dt"].Value<double>()),
                        Code = city["weather"][0]["id"].Value<int>().ToString(),
                        Name = city["weather"][0]["main"].Value<string>(),
                        Description = city["weather"][0]["description"].Value<string>(),
                        Temp = city["main"]["temp"].Value<double>()
                    });
                    incluedCityNames.Add(location.Name);
                }
            }

            return weather;
        }

        private DateTime FromIntervalSince1970(double intervalSince1970)
        {
            DateTime d = UNIX_EPOCH + TimeSpan.FromSeconds(intervalSince1970);
            return new DateTime(d.Year, d.Month, d.Day);
        }
    }
}
