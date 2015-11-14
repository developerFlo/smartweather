using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public static class ServiceManager
    {
        public static ICalendarService CalendarService = new CalendarService();
        public static ICompassService CompassService = new CompassService();
        public static IDownloadService DownloadService = new DownloadService();
        public static IGeoService GeoService = new GeoService();
        public static IWeatherService WeahterService = new WeatherService();
        public static ISettingsService SettingsService = new SettingsService();
        public static IDatabaseService DatabaseService = new DatabaseService();
        public static INavigationService NavigationService = new NavigationService();
    }
}
