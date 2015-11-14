using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Smartweather.Sevices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Smartweather.Test.Tests
{
    [TestClass]
    public class DatabaseServiceTests
    {
        DatabaseService s;

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext ctx)
        {
            DatabaseService s = new DatabaseService();
            s.Initialize();
            await s.UpdateCitiesList();
        }

        [TestInitialize]
        public void Initialize()
        {
            s = new DatabaseService();
            s.Initialize();
        }

        [TestMethod]
        public async Task TestUpdateCitiesList()
        {
            await s.UpdateCitiesList();
        }

        [TestMethod]
        public void TestGetLocationFromCityName()
        {
            var l = s.GetLocationFromCityName("Kapfenberg");
            Assert.IsNotNull(l);
            //Standardmäßig 7872257, könnte aber auch 2774773 sein, da beide den Namen Kapfenberg haben
            Assert.AreEqual(2774773, l.CityID);
            Assert.AreEqual("Kapfenberg", l.Name);
            Assert.AreEqual(47.44458, l.Position.lat, 0.0001);
            Assert.AreEqual(15.29331, l.Position.lon, 0.0001);
        }

        [TestMethod]
        public void TestGetLocationFromCityNameNull()
        {
            var l = s.GetLocationFromCityName(null);
            Assert.IsNull(l);
        }

        [TestMethod]
        public void TestGetLocationFromCityNameUnknown()
        {
            var l = s.GetLocationFromCityName("HansGuckInDieLuft");
            Assert.IsNull(l);
        }

        [TestMethod]
        public void TestGetLocationForCityIDs()
        {
            var locations = s.GetLocationsForCityIDs(new int[]{ 7872257, 2643743, 2778067, 7873556 });
            Assert.IsNotNull(locations);
            Assert.AreEqual(4, locations.Count);
            Assert.AreEqual("Birkfeld", locations[0].Name);
            Assert.AreEqual("Graz", locations[1].Name);
            Assert.AreEqual("Kapfenberg", locations[2].Name);
            Assert.AreEqual("London", locations[3].Name);
        }

        [TestMethod]
        public void TestGetLocationForCityIDsNull()
        {
            var locations = s.GetLocationsForCityIDs(null);
            Assert.AreEqual(0, locations.Count);
        }

        [TestMethod]
        public void TestGetLocationForCityIDsEmpty()
        {
            var locations = s.GetLocationsForCityIDs(new List<int>());
            Assert.AreEqual(0, locations.Count);
        }
    }
}
