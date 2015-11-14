using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Smartweather.Models
{
    public class WeatherIconPart
    {
        static readonly Color DEFAULT_COLOR = Colors.Gray;

        char _iconChar;
        Color? _prefColor;

        public WeatherIconPart(char iconChar, Color? prefHexColor)
        {
            _iconChar = iconChar;
            _prefColor = prefHexColor;
        }

        public char IconChar
        {
            get { return _iconChar; }
        }

        public string IconString
        {
            get { return _iconChar.ToString(); }
        }

        public Color Color
        {
            get
            {
                return _prefColor ?? DEFAULT_COLOR;
            }
        }
    }
}
