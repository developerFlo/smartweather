using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Smartweather.Models;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Test.Tests
{
    [TestClass]
    public class WeatherServiceTests
    {
        WeatherService s;

        [TestInitialize]
        public void Initialize()
        {
            s = new WeatherService();
        }

        [TestMethod]
        public async Task TestGetUpcomingWeatherForCity()
        {
            var location = new Location()
            {
                CityID = 2774773, //2774773: Kapfenberg
                Position = new Geoposition()
                {
                    lat = 11,
                    lon = 11
                }
            };
            var weather = await s.GetUpcomingWeatherForLocation(location);
            Assert.IsTrue(weather.Count > 0);

            DateTime iter = DateTime.Today;
            foreach (var w in weather)
            {
                Assert.AreEqual("Kapfenberg", w.Location.Name);
                Assert.AreEqual(iter, w.Day);

                iter = iter.AddDays(1);
            }
        }


        [TestMethod]
        public async Task TestGetUpcomingWeatherForPosition()
        {
            var location = new Location()
            {
                //lat 47.453991 - long 15.27019: Kapfenberg
                Position = new Geoposition()
                {
                    lat = 47.453991,
                    lon = 15.27019
                }
            };
            var weather = await s.GetUpcomingWeatherForLocation(location);
            Assert.IsTrue(weather.Count > 0);

            DateTime iter = DateTime.Today;
            foreach (var w in weather)
            {
                Assert.AreEqual("Kapfenberg", w.Location.Name);
                Assert.AreEqual(iter, w.Day);
                iter = iter.AddDays(1);
            }
        }



        [TestMethod]
        public async Task TestGetWeatherAroundPositionAll()
        {
            //lat 47.453991 - long 15.27019: Kapfenberg
            var position = new Geoposition()
            {
                lat = 47.453991,
                lon = 15.27019
            };
            var locations = await s.GetWeatherAroundPosition(position, 20, WeatherGroup.All);

            //14 = 20 lt. Parameter - doppelte Ortsnamen => Kann sich u.U. ändern
            Assert.AreEqual(14, locations.Count);
        }

        [TestMethod]
        public async Task TestGetWeatherAroundPositionCloudy()
        {
            //lat 47.453991 - long 15.27019: Kapfenberg
            var position = new Geoposition()
            {
                lat = 47.453991,
                lon = 15.27019
            };
            var locations = await s.GetWeatherAroundPosition(position, 20, WeatherGroup.Cloudy);

            //Test kann je nach Wetter fehlschlagen
            //14 = 20 lt. Parameter - doppelte Ortsnamen => Kann sich u.U. ändern
            Assert.IsTrue(locations.Count < 14, "Es kann auch sein, dass es um Kapfenberg komplett bewölkt ist.");
            Assert.IsTrue(locations.Count > 0, "Es kann auch sein, dass es um Kapfenberg nirgends bewölkt ist.");
        }
    }
}
