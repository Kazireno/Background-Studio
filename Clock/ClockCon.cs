using PluginAPI;
using System.Windows;

namespace Clock
{
    public class ClockCon : Plugin
    {
        public string Name => "Clock";
        public string owner => "Background Studio";
        public string profile => "Alarm clock service is available";
        public string version => "1.0";

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
        public bool Time_Enable = true;
        public string Time_RGB = "0,0,0,0";
        public bool Week_Enable = true;
        public string Week_RGB = "0,0,0,0";
        public bool Year_Enable = true;
        public string Year_RGB = "0,0,0,0";

        ClockWindow ClockWindow = new ClockWindow();
        IntPtr DefaultWindowHandler = IntPtr.Zero;
        Window window = null;

        public void getConfig(List<string> config)
        {
            Coordinate = config[0];
            Size = Convert.ToInt32(config[1]);
            Time_Enable = Tools.StringToBool(config[2]);
            Time_RGB = config[3];
            Week_Enable = Tools.StringToBool(config[4]);
            Week_RGB = config[5];
            Year_Enable= Tools.StringToBool(config[6]);
            Year_RGB = config[7];
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> list = new List<Tools.ConfigType>
            {
                Tools.ConfigType.Coordinate,
                Tools.ConfigType.Percentage,
                Tools.ConfigType.Bool,
                Tools.ConfigType.RGB,
                Tools.ConfigType.Bool,
                Tools.ConfigType.RGB,
                Tools.ConfigType.Bool,
                Tools.ConfigType.RGB
            };
            return list;
        }

        public void getDefaultWindow(Window? window)
        {
            this.window = window;
            return;
        }

        public void getDefaultWindowHandle(IntPtr intPtr)
        {
            DefaultWindowHandler = intPtr;
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
            ClockWindow.Close();
            GC.Collect();
        }

        public bool Start()
        {
            ClockWindow.Show();
            Tools.TackWindowOnBackgroun(ClockWindow);
            ClockWindow.getConfig(Coordinate, Size, Time_Enable, Time_RGB, Week_Enable, Week_RGB, Year_Enable, Year_RGB);
            return true;
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>
            {
                "Coordinate$" + Coordinate,
                "Size$" + Size,
                "TimeShow$" + Tools.BoolToString(Time_Enable),
                "TimeColor$" + Time_RGB,
                "WeekShow$" + Tools.BoolToString(Week_Enable),
                "WeekColor$" + Week_RGB,
                "YearShow$" + Tools.BoolToString(Year_Enable),
                "YearColor$" + Year_RGB
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