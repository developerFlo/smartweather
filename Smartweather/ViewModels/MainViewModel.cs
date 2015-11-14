using Smartweather.Common;
using Smartweather.Models;
using Smartweather.Sevices;
using Smartweather.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Smartweather.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        ObservableCollection<DayWeather> _daysWeather;
        DayWeather _currentDayWeather;
        ICommand _goToSelectLocationCommand;
        ICommand _goToNearbyCommand;
        ICommand _goToAboutCommand;

        private async void StartLoadWeather()
        {

            Location loc = ServiceManager.SettingsService.GetCurrentCityLocation();
            if(loc == null)
            {
                Geoposition pos = Geoposition.EMPTY;
                try
                {
                    pos = await ServiceManager.GeoService.GetCurrentPosition();
                    loc = new Location()
                    {
                        Position = pos
                    };
                }
                catch (GeoServiceException ex)
                {
                    await (new MessageDialog(ex.Message)).ShowAsync();

                    //Test Position - Kapfenberg
                    loc = new Location()
                    {
                        Position = new Geoposition()
                        {
                            lat = 47.453991,
                            lon = 15.27019
                        }
                    };
                }
            }
            if (loc != null)
            {
                DaysWeather = new ObservableCollection<DayWeather>(
                    await ServiceManager.WeahterService.GetUpcomingWeatherForLocation(loc)
                );
                CurrentDayWeather = DaysWeather.FirstOrDefault();
            }
        }

        private void NavigateTo(string page)
        {
            switch (page)
            {
                case "About":
                    ServiceManager.NavigationService.GoTo(typeof(AboutPage));
                    break;
                case "Map":
                    ServiceManager.NavigationService.GoTo(typeof(MapPage));
                    break;
                case "Nearby":
                    ServiceManager.NavigationService.GoTo(typeof(NearbyPage));
                    break;
                case "SelectLocation":
                    ServiceManager.NavigationService.GoTo(typeof(SelectLocationPage));
                    break;
            }
        }

        #region Properties

        public ObservableCollection<DayWeather> DaysWeather
        {
            get
            {
                if(_daysWeather == null)
                {
                    _daysWeather = new ObservableCollection<DayWeather>();
                    StartLoadWeather();
                }
                return _daysWeather;
            }
            private set
            {
                if(_daysWeather != value)
                {
                    _daysWeather = value;
                    RaisePropertyChanged("DaysWeather");
                }
            }
        }

        public DayWeather CurrentDayWeather
        {
            get { return _currentDayWeather; }
            set
            {
                if(_currentDayWeather != value)
                {
                    if (_currentDayWeather != null) _currentDayWeather.IsCurrent = false;
                    _currentDayWeather = value;
                    if (_currentDayWeather != null) _currentDayWeather.IsCurrent = true;
                    RaisePropertyChanged("CurrentDayWeather");
                }
            }
        }

        public ICommand GoToSelectLocationCommand
        {
            get { return _goToSelectLocationCommand ?? (_goToSelectLocationCommand = new DelegateCommand(t => NavigateTo("SelectLocation"))); }
        }

        public ICommand GoToNearbyCommand
        {
            get { return _goToNearbyCommand ?? (_goToNearbyCommand = new DelegateCommand(t => NavigateTo("Nearby"))); }
        }

        public ICommand GoToAboutCommand
        {
            get { return _goToAboutCommand ?? (_goToAboutCommand = new DelegateCommand(t => NavigateTo("About"))); }
        }

        #endregion
    }
}
