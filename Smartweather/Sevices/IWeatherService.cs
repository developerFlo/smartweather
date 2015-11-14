using Smartweather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface IWeatherService
    {
        Task<IList<DayWeather>> GetUpcomingWeatherForLocation(Location location);
        Task<IList<DayWeather>> GetWeatherAroundPosition(Geoposition position, int maxCnt, WeatherGroup restrictToGroup);
    }
    
    public enum WeatherGroup:uint
    {
        Sunny,
        Rainy,
        Cloudy,
        Snowy,
        All
    }
}
