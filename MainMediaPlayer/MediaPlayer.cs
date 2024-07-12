using PluginAPI;
using System.IO;
using System.Windows;
using System.Windows.Interop;

namespace MainMediaPlayer
{
    public class MediaPlayer : Plugin
    {
        public string Name => "DefaultMediaPlayer";
        public string owner => "Background Studio";
        public string profile => "Background Studio Default Media Player";
        public string version => "1.0";
        public int WindowstType => 0;
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

        public string? fileType = "";
        public string? filePath = "";
        public int MusicValue = 100;

        MediaPlayerWindow mainWindow = new MediaPlayerWindow();


        public void getConfig(List<string> config)
        {
            filePath = config[0];
            MusicValue = Convert.ToInt32(config[1]);
        }

        public List<string>? submitConfig()
        {
            List<string> config = new List<string>();
            config.Add("FilePath$" + filePath);
            config.Add("MusicVolume$" + MusicValue.ToString());
            return config;
        }

        public List<Tools.ConfigType> GetConfigTypes()
        {
            List<Tools.ConfigType> list = new List<Tools.ConfigType>
            {
                Tools.ConfigType.Path,
                Tools.ConfigType.Percentage
            };
            return list;
        }

        public void getDefaultWindow(Window window)
        {
            return;
        }

        public void getDefaultWindowHandle(IntPtr intPtr)
        {
            return;
        }

        public bool Init()
        {
            fileType = filePath.Substring(filePath.LastIndexOf(".") + 1);
            return true;
        }

        public bool Launch()
        {
            mainWindow.getConfig(fileType, filePath, MusicValue);
            return true;
        }

        public bool Start()
        {
            mainWindow.Show();
            Tools.TackWindowOnBackgroun(mainWindow);
            return true;
        }

        public Window? submitWindow()
        {
            return mainWindow;
        }

        public IntPtr? submitWindowsHandle()
        {
            return new WindowInteropHelper(mainWindow).Handle;
        }

        public void Reset()
        {
            mainWindow.Close();
            mainWindow = new MediaPlayerWindow();
        }
    }
}