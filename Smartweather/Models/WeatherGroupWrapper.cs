using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class WeatherGroupWrapper:INotifyPropertyChanged
    {
        WeatherGroup _group;
        char[] _icons = new char[] { '\xF113' };
        bool _isChecked;

        public WeatherGroupWrapper(WeatherGroup group)
        {
            _group = group;
        }

        public WeatherGroup Group
        {
            get { return _group; }
        }

        public string Icon
        {
            get { return _icons[0].ToString(); }
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
            var list = Enum.GetValues(typeof(WeatherGroup));
            foreach(var e in list)
                yield return new WeatherGroupWrapper((WeatherGroup)e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
