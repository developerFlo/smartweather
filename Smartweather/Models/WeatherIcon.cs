using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;

namespace Smartweather.Models
{
    public class WeatherIcon
    {
        Regex _regEx;
        WeatherIconPart[] _iconParts;

        private WeatherIcon(string regExStr, WeatherIconPart[] iconParts)
        {
            _regEx = new Regex(regExStr);
            _iconParts = iconParts;
        }

        public Regex RegEx
        {
            get { return _regEx; }
        }

        public WeatherIconPart[] IconParts
        {
            get { return _iconParts; }
        }

        public static WeatherIcon[] IconSet = new WeatherIcon[]
        {
            new WeatherIcon("2..", new WeatherIconPart[]{new WeatherIconPart('\xF114',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("3[01]0", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF10A',Color.FromArgb(255,130,178,228)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("3[01][12]", new WeatherIconPart[]{new WeatherIconPart('\xF10A',Color.FromArgb(255,130,178,228)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("31[34]|32.", new WeatherIconPart[]{new WeatherIconPart('\xF10E',Color.FromArgb(255,70,129,195)), new WeatherIconPart('\xF109',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("50[2-4]", new WeatherIconPart[]{new WeatherIconPart('\xF107',Color.FromArgb(255,70,129,195)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("50[01]", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF107',Color.FromArgb(255,70,129,195)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("51.", new WeatherIconPart[]{new WeatherIconPart('\xF10C',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("520", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF104',Color.FromArgb(255,70,129,195)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("5[23][12]", new WeatherIconPart[]{new WeatherIconPart('\xF104',Color.FromArgb(255,70,129,195)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("600", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,0,0,0)), new WeatherIconPart('\xF10B',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("60[12]", new WeatherIconPart[]{new WeatherIconPart('\xF10B',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("61[126]", new WeatherIconPart[]{new WeatherIconPart('\xF10C',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("615", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF10C',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("620", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF103',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("62[12]", new WeatherIconPart[]{new WeatherIconPart('\xF103',Color.FromArgb(255,172,211,243)), new WeatherIconPart('\xF109',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("7..", new WeatherIconPart[]{new WeatherIconPart('\xF108',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("800|904|951", new WeatherIconPart[]{new WeatherIconPart('\xF113',Color.FromArgb(255,255,165,0))}),
            new WeatherIcon("80[123]", new WeatherIconPart[]{new WeatherIconPart('\xF101',Color.FromArgb(255,255,165,0)), new WeatherIconPart('\xF106',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("804", new WeatherIconPart[]{new WeatherIconPart('\xF106',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("90[0125]|95[2-9]|96.", new WeatherIconPart[]{new WeatherIconPart('\xF115',Color.FromArgb(255,204,204,204)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))}),
            new WeatherIcon("903", new WeatherIconPart[]{new WeatherIconPart('\xF102',Color.FromArgb(255,133,216,247)), new WeatherIconPart('\xF105',Color.FromArgb(255,204,204,204))})
        };
    }
}
