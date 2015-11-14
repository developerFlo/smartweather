using Smartweather.Common;
using Smartweather.Models;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Smartweather.ViewModels
{
    class SelectLocationViewModel : BaseViewModel
    {
        private ObservableCollection<LocationWithSource> _locations;
        private LocationWithSource _currentLocation;
        private ObservableCollection<WeatherGroupWrapper> _weatherGroups;
        private WeatherGroupWrapper _currentWeatherGroup;
        private string _searchString = "";
        private bool _isSearchActive = false;
        private bool _isSearching = false;
        private ICommand _searchCommand;
        private ICommand _openCommand;

        async void LoadLocations()
        {
            if (IsSearchActive) return;
            //Aktuelle Position
            Locations.Add(new LocationWithSource()
            {
                Location = new Location()
                {
                    Name = _res.GetString("CurrentPosition"),
                    Position = Geoposition.EMPTY
                },
                Source = LocationSource.Geo
            });


            //Zuletzt gewählte Positionen
            var recentLocations = ServiceManager.DatabaseService.GetLocationsForCityIDs(
                                    ServiceManager.SettingsService.GetRecentCities());

            foreach (var l in recentLocations)
            {
                await Task.Delay(20);
                if (IsSearchActive) return;
                Locations.Add(new LocationWithSource()
                {
                    Location = l,
                    Source = LocationSource.Recent
                });
            }

            //Orte von Kalender
            var calendarLocations = (await ServiceManager.CalendarService.GetCalendarEntryLocations(20))
                .Select(ls => ServiceManager.DatabaseService.GetLocationFromCityName(ls))
                .Where(l => l != null);
            foreach (var l in calendarLocations)
            {
                await Task.Delay(20);
                if (IsSearchActive) return;
                Locations.Add(new LocationWithSource()
                {
                    Location = l,
                    Source = LocationSource.Calendar
                });
            }
        }

        private async Task Search()
        {
            if (!_isSearching)
            {
                _isSearching = true;
                IsSearchActive = true;
                Locations.Clear();

                var searchLocations = ServiceManager.DatabaseService.FindLocations(_searchString, 0, 30);
                foreach (var l in searchLocations)
                {
                    await Task.Delay(20);
                    Locations.Add(new LocationWithSource()
                    {
                        Location = l,
                        Source = LocationSource.Search
                    });
                }
                _isSearching = false;
            }
        }

        private void Open()
        {
            if(_currentLocation != null)
            {
                if(_currentLocation.Location.CityID.HasValue)
                {
                    ServiceManager.SettingsService.AddRecentCity(_currentLocation.Location.CityID.Value);
                }
                if (_currentLocation.Source == LocationSource.Geo)
                    ServiceManager.SettingsService.SetCurrentCityLocation(null);
                else
                    ServiceManager.SettingsService.SetCurrentCityLocation(_currentLocation.Location);

                ServiceManager.NavigationService.GoBack();
            }
        }

        #region Properties
        public ObservableCollection<LocationWithSource> Locations
        {
            get {
                if(_locations == null)
                {
                    _locations = new ObservableCollection<LocationWithSource>();
                    LoadLocations();
                }
                return _locations;
            }
            private set
            {
                if(_locations != value)
                {
                    _locations = value;
                    RaisePropertyChanged("Locations");
                }
            }
        }

        public LocationWithSource CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                if(_currentLocation != value)
                {
                    _currentLocation = value;
                    RaisePropertyChanged("CurrentLocation");
                }
            }
        }

        public ObservableCollection<WeatherGroupWrapper> WeatherGroups
        {
            get
            {
                if(_weatherGroups == null)
                {
                    _weatherGroups = new ObservableCollection<WeatherGroupWrapper>(WeatherGroupWrapper.GetAllGroups());
                }
                return _weatherGroups;
            }
            private set
            {
                if(_weatherGroups != value)
                {
                    _weatherGroups = value;
                    RaisePropertyChanged("Locations");
                }
            }
        }

        public WeatherGroupWrapper CurrentWeatherGroup
        {
            get { return _currentWeatherGroup; }
            set
            {
                if(_currentWeatherGroup != value)
                {
                    _currentWeatherGroup = value;
                    RaisePropertyChanged("CurrentWeatherGroup");
                }
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if(_searchString != value)
                {
                    _searchString = value;
                    RaisePropertyChanged("SearchString");
                }
            }
        }

        public bool IsSearchActive
        {
            get { return _isSearchActive; }
            set
            {
                if(_isSearchActive != value)
                {
                    _isSearchActive = value;
                    RaisePropertyChanged("IsSearchActive");
                }
            }
        }

        public ICommand SearchCommand
        {
            get { return _searchCommand??(_searchCommand = new DelegateCommand(async t => await Search())); }
        }

        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new DelegateCommand(t => Open())); }
        }
        #endregion
    }
}
