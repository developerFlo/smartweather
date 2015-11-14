using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface INavigationService
    {
        void RegisterPage(bool allowBack);
        void GoTo(Type page);
        void GoBack();
    }
}
