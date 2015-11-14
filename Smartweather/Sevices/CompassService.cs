using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartweather.Models;
using Windows.Foundation;
using Windows.Devices.Sensors;

namespace Smartweather.Sevices
{
    public class CompassService : ICompassService
    {
        public void RegisterCompass(Action<CompassState> compassChangedHandler)
        {
            var compass = Compass.GetDefault();
            var orientation = SimpleOrientationSensor.GetDefault();
            if (orientation != null && compass != null) {
                compass.ReadingChanged += new TypedEventHandler<Compass, CompassReadingChangedEventArgs>((s, e) =>
                 {
                     CompassState state = new CompassState();
                     state.Active =
                        orientation.GetCurrentOrientation() == SimpleOrientation.Faceup
                        && e.Reading.HeadingTrueNorth != null;
                     state.Ratio = e.Reading.HeadingTrueNorth ?? 0;
                     compassChangedHandler(state);
                 });
            }//else Sensoren von Gerät nicht unterstützt
        }
    }
}
