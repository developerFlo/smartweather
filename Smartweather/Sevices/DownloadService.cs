using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public class DownloadService : IDownloadService
    {
        public Task<string> Load(Uri url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url);
        }
    }
}
