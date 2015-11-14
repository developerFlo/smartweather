using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Smartweather.Sevices
{
    public class NavigationService : INavigationService
    {
        private bool _allowBack = false;

        public void GoTo(Type page)
        {
            ((Frame)Window.Current.Content).Navigate(page, null);
        }

        public void RegisterPage(bool allowBack)
        {
            if (allowBack != _allowBack)
            {
                _allowBack = allowBack;
                if (allowBack)
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
                }
                else
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    SystemNavigationManager.GetForCurrentView().BackRequested -= BackRequested;
                }
            }
        }

        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            GoBack();
        }

        public void GoBack()
        {
            Frame rootFrame = (Frame)Window.Current.Content;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
    }
}
