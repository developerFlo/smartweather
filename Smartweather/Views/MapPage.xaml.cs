using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Smartweather.Views
{
    public partial class MapPage:Page
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ServiceManager.NavigationService.RegisterPage(false);
            base.OnNavigatedTo(e);
        }
    }
}
