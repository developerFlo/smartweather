﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Sevices
{
    public interface IDownloadService
    {
        Task<string> Load(Uri url);
    }
}
