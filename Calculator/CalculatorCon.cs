using PluginAPI;
using System.Windows;

namespace Calculator
{
    public class CalculatorCon : Plugin
    {
        public string Name => "Calculator";
        public string owner => "Background Studio";
        public string profile => "Desktop Calculator";
        public string version => "0.1";

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
        public int WindowstType => 2;

        private string BackgroundColor = "0,0,0,0";
        private string WorkAreaColor = "0,0,0,0";
        private int Size = 50;
        private string Bind = "Alt";

        CalculatorWindow calculatorWindow = new CalculatorWindow();

        public void getConfig(List<string> config)
        {
            BackgroundColor = config[0];
            WorkAreaColor = config[1];
            Size = Convert.ToInt32(config[2]);
            Bind = config[3];
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> configTypes = new List<Tools.ConfigType>
            {
                Tools.ConfigType.RGB,
                Tools.ConfigType.RGB,
                Tools.ConfigType.Percentage,
                Tools.ConfigType.String
            };
            return configTypes;
        }

        public void getDefaultWindow(Window? window)
        {
            return;
        }

        public void getDefaultWindowHandle(IntPtr intPtr)
        {
            return;
        }

        public bool Init()
        {
            calculatorWindow.GetConfig(BackgroundColor, WorkAreaColor, Size, Bind);
            return true;
        }

        public bool Launch()
        {
            calculatorWindow.Show();
            return true;
        }

        public void Reset()
        {
            calculatorWindow.Close();
            calculatorWindow = new CalculatorWindow();
            return;
        }

        public bool Start()
        {
            calculatorWindow.Hide();
            return true;
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>
            {
                "BackgorundColor$" + BackgroundColor,
                "WorkAreaColor$"+WorkAreaColor,
                "Size$" + Size,
                "Bind$" + Bind
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