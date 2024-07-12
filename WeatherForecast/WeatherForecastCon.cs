using PluginAPI;
using System.Windows;

namespace WeatherForecast
{
    public class WeatherForecastCon : Plugin
    {
        public string Name => "WeatherForecast";
        public string owner => "BackgroundStudio";
        public string profile => "WeatherForecast";
        public string version => "2.9";

        private bool _enable = false;
        private short _alpha = 100;

        public bool Enable
        {
            get => _enable;
            set { _enable = value; }
        }
        public short Alpha
        {
            get => _alpha;
            set { _alpha = value; }
        }

        public int WindowstType => 1;

        public string Coordinate = "";
        public int Size;
        public string RGB = "0,0,0,0";
        public string Cify = "北京";

        Weather weather = new Weather();
        IntPtr intPtr = IntPtr.Zero;

        public void getConfig(List<string> config)
        {
            Coordinate = config[0];
            Size = Convert.ToInt32(config[1]);
            RGB = config[2];
            Cify = config[3];
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> list = new List<Tools.ConfigType>
            {
                Tools.ConfigType.Coordinate,
                Tools.ConfigType.Percentage,
                Tools.ConfigType.RGB,
                Tools.ConfigType.String
            };
            return list;
        }

        public void getDefaultWindow(Window? window)
        {
            return;
        }

        public void getDefaultWindowHandle(IntPtr intPtr)
        {
            this.intPtr = intPtr;
        }

        public bool Init()
        {
            return true;
        }

        public bool Launch()
        {
            return true;
        }

        public void Reset()
        {
            weather.Hide();
            return;
        }

        public bool Start()
        {
            weather.Show();
            Tools.TackWindowOnBackgroun(weather);
            weather.getConfig(Coordinate, Size, RGB, Cify);
            return true;
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>
            {
                "Coordinate$" + Coordinate,
                "Size$" + Size,
                "Color$" + RGB,
                "City$" + Cify
            };
            return config;
        }

        public Window? submitWindow()
        {
            return null;
        }

        public IntPtr? submitWindowsHandle()
        {
            return null;
        }
    }
}