using Smartweather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface ICompassService
    {
        void RegisterCompass(Action<CompassState> compassChangedHandler);
    }
}
