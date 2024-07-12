using PluginAPI;
using System.Windows;
using System.Windows.Threading;

namespace Timer
{
    public class TimerCon : Plugin
    {
        public string Name => "Timer";
        public string owner => "Background Studio";
        public string profile => "Timer";
        public string version => "0.2";

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

        public bool EnableAlarm = false;
        public string Alarm_Clock = "00:00";
        public string Message = "";

        string h = "";
        string m = "";

        public void getConfig(List<string> config)
        {
            EnableAlarm = Tools.StringToBool(config[0]);
            Alarm_Clock = config[1];
            Message = config[2];
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> list = new List<Tools.ConfigType>
            {
                Tools.ConfigType.Bool,
                Tools.ConfigType.String,
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
            return;
        }

        public bool Init()
        {
            return true;
        }

        public bool Launch()
        {
            h = Alarm_Clock.Split(':')[0];
            m = Alarm_Clock.Split(":")[1];
            return true;
        }

        public void Reset()
        {
            return;
        }

        DispatcherTimer dayTimer = new DispatcherTimer();

        public bool Start()
        {
            if (!EnableAlarm) return true;
            dayTimer.Interval = TimeSpan.FromMilliseconds(500);
            dayTimer.Tick += new EventHandler(Starts);
            dayTimer.Start();
            return true;
        }

        public void Starts(object sender, EventArgs e)
        {
            if (h == DateTime.Now.ToString("HH") && m == System.DateTime.Now.ToString("mm"))
            {
                EnableAlarm = false;
                dayTimer.Stop();
                MessageBox.Show(Message, "闹钟");
            }
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>
            {
                "EnableAlarm$"+Tools.BoolToString(EnableAlarm),
                "AlarmClock$" + Alarm_Clock,
                "Message$" + Message
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