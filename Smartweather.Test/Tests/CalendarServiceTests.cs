using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Smartweather.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Test.Tests
{
    [TestClass]
    public class CalendarServiceTests
    {
        CalendarService s;

        [TestInitialize]
        public void Initialize()
        {
            s = new CalendarService();
        }

        [TestMethod]
        public async Task TestGetCalendarEntryLocations()
        {
            var locations = await s.GetCalendarEntryLocations(20);
            Assert.AreNotEqual(0,locations.Count);
        }
    }
}
