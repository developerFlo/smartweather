using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Smartweather.Models
{
    public class DayWeather:INotifyPropertyChanged
    {
        private bool _isCurrent = false;

        public Location Location { get; set; }
        public DateTime Day { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }

        public string Code { get; set; }

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                if(_isCurrent != value)
                {
                    _isCurrent = value;
                    RaisePropertyChanged("IsCurrent");
                }
            }
        }

        public WeatherIcon Icon
        {
            get
            {
                if (string.IsNullOrEmpty(this.Code)) return null;
                else return WeatherIcon.IconSet.Where(i => i.RegEx.IsMatch(this.Code)).FirstOrDefault();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
