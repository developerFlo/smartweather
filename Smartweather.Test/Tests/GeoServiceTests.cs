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
    public class GeoServiceTests
    {
        GeoService s;

        [TestInitialize]
        public void Initialize()
        {
            s = new GeoService();
        }

        [TestMethod]
        public async Task TestGetCurrentPosition()
        {
            Geoposition p = await s.GetCurrentPosition();
            //lat 47.453991 - long 15.27019: Kapfenberg
            Assert.AreEqual(47.453991, p.lat, 1.0);
            Assert.AreEqual(15.27019, p.lon, 1.0);
        }
    }
}
