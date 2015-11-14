using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartweather.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using SQLite.Net;

namespace Smartweather.Sevices
{
    public class DatabaseService : IDatabaseService
    {
        private string _dbPath;

        public void Initialize()
        {
            _dbPath = Path.Combine(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path, "smartweather.db");

            // Initialize the database if necessary
            bool weatherLocationsStored = false;
            using (var db = GetNewConnection())
            {
                // Create the tables if they don't exist 
                db.CreateTable<DBLocation>();
                weatherLocationsStored = db.Table<DBLocation>().Count() > 0;
            }

            //Wetterdaten im Hintergrund herunterladen falls nicht bereits vorhanden
            if (!weatherLocationsStored)
                Task.Factory.StartNew(UpdateCitiesList);
        }

        private SQLiteConnection GetNewConnection()
        {
            return new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), _dbPath);
        }

        public Location GetLocationFromCityName(string location)
        {
            using (var db = GetNewConnection())
            {
                return (from l in db.Table<DBLocation>()
                        where l.Name == location
                        select l.ToLocation()).FirstOrDefault();
            }
        }

        public IList<Location> GetLocationsForCityIDs(IEnumerable<int> cityIDs)
        {
            if (cityIDs == null || cityIDs.Count() == 0) return new List<Location>();

            using (var db = GetNewConnection())
            {
                return (from l in db.Table<DBLocation>()
                        where cityIDs.Contains(l.CityID)
                        orderby l.Name
                        select l.ToLocation()).ToList();
                //return db.Query<DBLocation>("id in (" + string.Join(",", cityIDs) + ")")
                //    .Select(l => l.ToLocation())
                //    .ToList();
            }
        }

        public async Task UpdateCitiesList()
        {
            string jsonStr = await ServiceManager.DownloadService.Load(
                new Uri("https://pflanzen.blob.core.windows.net/smartweather/city.list.json"));
                //new Uri("http://localhost/city.list.json"));
            JArray json = JArray.Parse(jsonStr);
            IList<DBLocation> locations = new List<DBLocation>();
            foreach (var city in json)
            {
                locations.Add(new DBLocation()
                {
                    CityID = city["_id"].Value<int>(),
                    Name = city["name"].Value<string>(),
                    Country = city["country"].Value<string>(),
                    Lat = city["coord"]["lat"].Value<double>(),
                    Lon = city["coord"]["lon"].Value<double>()
                });
            }
            using (var db = GetNewConnection())
            {
                db.DeleteAll<DBLocation>();
                db.InsertAll(locations);
            }
        }

        public IList<Location> FindLocations(string searchString, int skip, int maxCount)
        {
            using (var db = GetNewConnection())
            {
                return (from l in db.Table<DBLocation>()
                        where l.Name.Contains(searchString)
                        orderby l.Name
                        select l.ToLocation()).Skip(skip).Take(maxCount).ToList();
            }
        }
    }
}
