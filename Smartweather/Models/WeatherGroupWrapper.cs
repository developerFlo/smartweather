using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class WeatherGroupWrapper:INotifyPropertyChanged
    {
        WeatherGroup _group;
        char[] _icons = new char[0];
        string _name;
        Regex _regEx;
        bool _isChecked;

        public WeatherGroupWrapper(WeatherGroup group)
        {
            _group = group;
        }

        public WeatherGroup Group
        {
            get { return _group; }
        }

        public Regex RegEx
        {
            get { return _regEx; }
        }

        public char[] Icons
        {
            get { return _icons; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if(_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged("IsChecked");
                }
            }
        }

        public static IEnumerable<WeatherGroupWrapper> GetAllGroups()
        {
            var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var list = Enum.GetValues(typeof(WeatherGroup));
            foreach (var e in list)
            {
                WeatherGroupWrapper wg = new WeatherGroupWrapper((WeatherGroup)e);
                wg._name = res.GetString(e.ToString());
                switch ((WeatherGroup)e)
                {
                    case WeatherGroup.Sunny:
                        wg._icons = new char[] { '\xF113' };
                        wg._regEx = new Regex("^800$");
                        break;
                    case WeatherGroup.Cloudy:
                        wg._icons = new char[] { '\xF106' };
                        wg._regEx = new Regex("^80[1-4]$");
                        break;
                    case WeatherGroup.Rainy:
                        wg._icons = new char[] { '\xF107', '\xF105' };
                        wg._regEx = new Regex("^(5..)|(3..)$");
                        break;
                    case WeatherGroup.Snowy:
                        wg._icons = new char[] { '\xF10B', '\xF105' };
                        wg._regEx = new Regex("^6..$");
                        break;
                    case WeatherGroup.All:
                        wg._icons = new char[] { '\xF105', '\xF10C', '\xF101' };
                        wg._regEx = new Regex(".*");
                        break;
                }
                yield return wg;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum WeatherGroup : uint
    {
        Sunny,
        Rainy,
        Cloudy,
        Snowy,
        All
    }
}
