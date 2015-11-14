using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Smartweather.Views
{
    public partial class NearbyPage:Page
    {
        public NearbyPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ServiceManager.NavigationService.RegisterPage(true);
            base.OnNavigatedTo(e);
        }
    }
}
