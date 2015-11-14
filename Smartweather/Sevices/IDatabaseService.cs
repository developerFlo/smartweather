using Smartweather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface IDatabaseService
    {
        void Initialize();
        Location GetLocationFromCityName(string location);
        IList<Location> GetLocationsForCityIDs(IEnumerable<int> cityIDs);
        IList<Location> FindLocations(string searchString, int skip, int count);
    }
}
