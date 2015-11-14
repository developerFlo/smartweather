using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartweather.Models
{
    class GeoServiceException : Exception
    {
        public Type ExceptionType
        {
            get;
            private set;
        }

        public GeoServiceException(Type exceptionType)
        {
            this.ExceptionType = exceptionType;
        }

        public enum Type
        {
            AccessDenied,
            AccessUnspecified
        }

        public override string Message
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            switch (ExceptionType)
            {
                case Type.AccessDenied:
                    return res.GetString("GeoExAccessDenied");
                case Type.AccessUnspecified:
                    return res.GetString("GeoExAccessUnspecified");
                default:
                    return res.GetString("GeoExUnknown");
            }
        }
    }
}
