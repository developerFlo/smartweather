using Smartweather.Models;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Smartweather.ViewModels
{
    class NearbyViewModel:BaseViewModel
    {
        ObservableCollection<DayWeather> _locationsWeather;
        ObservableCollection<WeatherGroupWrapper> _weatherGroups;
        WeatherGroupWrapper _currentWeatherGroup;

        public NearbyViewModel()
        {
            _weatherGroups = new ObservableCollection<WeatherGroupWrapper>(WeatherGroupWrapper.GetAllGroups());
        }

        private async void StartLoadWeather()
        {
            try {
                Geoposition pos = await ServiceManager.GeoService.GetCurrentPosition();
                var weather = await ServiceManager.WeahterService.GetWeatherAroundPosition(pos, 40, CurrentWeatherGroup.Group);
                foreach(var w in weather)
                {
                    await Task.Delay(20);
                    LocationsWeather.Add(w);
                }
            }catch(GeoServiceException ex)
            {
                await (new MessageDialog(ex.Message)).ShowAsync();
            }
        }

        #region Properties

        public ObservableCollection<DayWeather> LocationsWeather
        {
            get
            {
                if(_locationsWeather == null)
                {
                    _locationsWeather = new ObservableCollection<DayWeather>();
                    StartLoadWeather();
                }
                return _locationsWeather;
            }
            private set
            {
                if(_locationsWeather != value)
                {
                    _locationsWeather = value;
                    RaisePropertyChanged("LocationsWeather");
                }
            }
        }

        public ObservableCollection<WeatherGroupWrapper> WeatherGroups
        {
            get { return _weatherGroups; }
        }

        public WeatherGroupWrapper CurrentWeatherGroup
        {
            get {
                if(_currentWeatherGroup == null)
                {
                    _currentWeatherGroup = _weatherGroups.Where(grp => grp.Group == WeatherGroup.Sunny).Single();
                }
                return _currentWeatherGroup;
            }
            set
            {
                if(_currentWeatherGroup != value)
                {
                    _currentWeatherGroup = value;
                    RaisePropertyChanged("CurrentWeatherGroup");
                }
            }
        }

        #endregion
    }
}
