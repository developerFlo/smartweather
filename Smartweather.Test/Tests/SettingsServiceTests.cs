using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Smartweather.Models;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Smartweather.Test.Tests
{
    [TestClass]
    public class SettingsServiceTests
    {
        SettingsService s;

        [TestInitialize]
        public void Initialize()
        {
            ApplicationData.Current.LocalSettings.Values.Clear();
            s = new SettingsService();
        }

        //Recent Cities------------------
        [TestMethod]
        public void TestGetRecentCitiesEmpty()
        {
            Assert.AreEqual(0, s.GetRecentCities().Count);
        }

        [TestMethod]
        public void TestAddRecentCity()
        {
            s.AddRecentCity(5);
            var cities = s.GetRecentCities();
            Assert.AreEqual(1, cities.Count);
            Assert.AreEqual(5, cities[0]);
        }

        [TestMethod]
        public void TestAddRecentCityDuplicate()
        {
            s.AddRecentCity(5);
            s.AddRecentCity(5);
            var cities = s.GetRecentCities();
            Assert.AreEqual(1, cities.Count);
            Assert.AreEqual(5, cities[0]);
        }

        [TestMethod]
        public void TestRemoveRecentCityEmpty()
        {
            s.RemoveRecentCity(5);
            var cities = s.GetRecentCities();
            Assert.AreEqual(0, cities.Count);
        }

        [TestMethod]
        public void TestRemoveRecentCityExisting()
        {
            s.AddRecentCity(5);
            s.RemoveRecentCity(5);
            var cities = s.GetRecentCities();
            Assert.AreEqual(0, cities.Count);
        }

        //Current City---------------------------
        [TestMethod]
        public void TestGetCurrentCityEmpty()
        {
            Assert.IsNull(s.GetCurrentCityLocation());
        }

        [TestMethod]
        public void TestSetCurrentCityEmpty()
        {
            s.SetCurrentCityLocation(null);
            Assert.IsNull(s.GetCurrentCityLocation());
        }

        [TestMethod]
        public void TestSetCurrentCityVal()
        {
            var location = new Location() {
                Name = "myCity",
                CityID = 1099,
                Position = new Geoposition() {
                    lat = 10.11,
                    lon = 11.12
                }
            };
            s.SetCurrentCityLocation(location);
            var saved = s.GetCurrentCityLocation();
            Assert.AreEqual("myCity", saved.Name);
            Assert.AreEqual(1099, saved.CityID);
            Assert.AreEqual(10.11, saved.Position.lat, 0.0001);
            Assert.AreEqual(11.12, saved.Position.lon, 0.0001);
        }

        [TestMethod]
        public void TestSetCurrentCityValEmpty()
        {
            var location = new Location()
            {
                Name = "myCity",
                CityID = 1009,
                Position = new Geoposition()
                {
                    lat = 10.11,
                    lon = 11.12
                }
            };
            s.SetCurrentCityLocation(location);
            s.SetCurrentCityLocation(null);
            Assert.IsNull(s.GetCurrentCityLocation());
        }

        //Nearby Weather--------------------------------
        [TestMethod]
        public void TestGetNearbyWeatherDefault()
        {
            Assert.AreEqual(WeatherGroup.Snowy, s.GetNearbyWeather(WeatherGroup.Snowy));
        }

        [TestMethod]
        public void TestSetNearbyWeatherVal()
        {
            s.SetNearbyWeather(WeatherGroup.Rainy);
            Assert.AreEqual(WeatherGroup.Rainy, s.GetNearbyWeather(WeatherGroup.Snowy));
        }


    }
}
