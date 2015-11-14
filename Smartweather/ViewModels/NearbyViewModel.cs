using Smartweather.Models;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Smartweather.ViewModels
{
    class NearbyViewModel:BaseViewModel
    {
        ObservableCollection<DayWeather> _locationsWeather;
        ObservableCollection<WeatherGroupWrapper> _weatherGroups;
        IList<DayWeather> _completeWeatherList;
        CancellationTokenSource _cts;
        bool _isWeatherLoading = false;
        int _loadingVersion = 0;

        public NearbyViewModel()
        {
            _cts = new CancellationTokenSource();

            _weatherGroups = new ObservableCollection<WeatherGroupWrapper>(WeatherGroupWrapper.GetAllGroups());
            foreach(WeatherGroupWrapper grp in _weatherGroups)
                grp.PropertyChanged += Group_PropertyChanged;
        }

        private async void Group_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsChecked")
            {
                await RefreshShownWeather();
            }
        }

        private async void StartLoadWeather()
        {
            string problem = null;
            Geoposition pos = Geoposition.EMPTY;
            //Ermittlug view Geolocation
            try
            {
                pos = await ServiceManager.GeoService.GetCurrentPosition();
            }
            catch (GeoServiceException ex)
            {
                problem = ex.Message;
            }

            //Fallback auf Settings-Einstellung
            if (pos.Equals(Geoposition.EMPTY))
            {
                var loc = ServiceManager.SettingsService.GetCurrentCityLocation();
                if (loc != null) pos = loc.Position;
            }

            if(pos.Equals(Geoposition.EMPTY))
            {
                //Keine Position ermittel und keine Einstellung in den Settings vorgenommen
                // -> Fehler zu Geo-Location ausgeben
                if(problem != null)
                    await (new MessageDialog(problem)).ShowAsync();
            }
            else
            {
                //Position vorhanden -> Umgebungswetter ermitteln
                _completeWeatherList = await ServiceManager.WeahterService.GetWeatherAroundPosition(pos, 40);
                await RefreshShownWeather();
            }
        }

        private async Task RefreshShownWeather()
        {
            if (_completeWeatherList == null || _completeWeatherList.Count == 0) return;

            int currentLoadingVersion = ++_loadingVersion;
            if (_isWeatherLoading)
            {
                _cts.Cancel();
                while(_isWeatherLoading)
                {
                    //Warten bis vorheriger Ladevorgang abgeschlossen ist
                    await Task.Delay(500);
                }
            }

            if (!_isWeatherLoading && currentLoadingVersion == _loadingVersion)
            {
                _isWeatherLoading = true;
                _cts = new CancellationTokenSource();

                LocationsWeather = new ObservableCollection<DayWeather>();
                Regex[] regex = WeatherGroups.Where(grp => grp.IsChecked).Select(grp => grp.RegEx).ToArray();
                foreach (var w in _completeWeatherList.Where(l => regex.Any(r => r.IsMatch(l.Code))))
                {
                    await Task.Delay(20, _cts.Token);
                    if (_cts.IsCancellationRequested) break;
                    LocationsWeather.Add(w);

                }
                _isWeatherLoading = false;
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

        #endregion
    }
}
