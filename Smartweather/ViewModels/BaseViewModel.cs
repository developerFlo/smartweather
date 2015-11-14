using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Smartweather.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected ResourceLoader _res;

        public BaseViewModel()
        {
            _res = ResourceLoader.GetForCurrentView();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
