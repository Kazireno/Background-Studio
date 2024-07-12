using PluginAPI;
using System.Windows;

namespace AudioPlayer
{
    public class AudioCon : Plugin
    {
        public string Name => "AudioPlayer";
        public string owner => "Background Studio";
        public string profile => "AudioPlayer";
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

        private string Path = "";
        private int Vol = 100;

        AudioPlayer _player = new AudioPlayer();

        public int WindowstType => 2;

        public void getConfig(List<string> config)
        {
            Path = config[0];
            Vol = Convert.ToInt32(config[1]);
            return;
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> list = new List<Tools.ConfigType>
            {
                Tools.ConfigType.Path,
                Tools.ConfigType.Percentage,
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
            if (Path == "") return false;
            _player.mediaElement.Source = new Uri(Path, UriKind.Absolute);
            return true;
        }

        public bool Launch()
        {
            _player.Show();
            return true;
        }

        public void Reset()
        {
            _player.Close();
            _player = new AudioPlayer();
            return;
        }

        public bool Start()
        {
           _player.mediaElement.Play();
            return true;
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>
            {
                "Path$" + Path,
                "Vol$" + Vol.ToString(),
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