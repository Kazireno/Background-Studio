using BackgorundStudio_build2024._3._1_alpha.API;
using BackgorundStudio_build2024._3._1_alpha.MainCore;
using BackgorundStudio_build2024._3._1_alpha.Module;
using BackgorundStudio_build2024._3._1_alpha.Tools.Files;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Diagnostics;
using System.Windows;

namespace BackgorundStudio_build2024._3._1_alpha
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _taskbar;
        MainWindow mainWindow;

        public static string version = "0.0.2.alpha";
        public static bool Debug = true;
        public string PresetName;

        Log log = new Log(Debug);
        PluginLauncher pluginLauncher;
        MainXMLConfig xmlConfig;
        PresetItemsXMLConfig presetItemsXMLConfig;
        PresetLauncher presetLauncher;

        protected override void OnStartup(StartupEventArgs e)
        {
            _taskbar = (TaskbarIcon)FindResource("Taskbar");
            base.OnStartup(e);
            Init();
        }

        private void OpenOptForm_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            Environment.Exit(0);
        }

        private void Init()
        {
            log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Hello World !");
            log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Core Version : " + version);
            if (Debug) { log.NewLog(LogString.LogLevel.Debug, LogString.LogType.Main, "Debug Mode Is Enable"); }

            log.NewLog(LogString.LogLevel.Debug, LogString.LogType.Main, "Start Load Tools Class");

            //加载工具类
            xmlConfig = new MainXMLConfig(version, log, Debug);
            PresetName = xmlConfig.GetPresetName();
            pluginLauncher = new PluginLauncher(version, log, Debug);
            presetItemsXMLConfig = new PresetItemsXMLConfig(log, Debug, pluginLauncher.getPluginAssemby(), pluginLauncher);
            presetLauncher = new PresetLauncher(log, pluginLauncher, presetItemsXMLConfig, Debug, xmlConfig.GetPresetName());

            mainWindow = new MainWindow(xmlConfig, presetLauncher, pluginLauncher,presetItemsXMLConfig, log);

            //启动进程
            presetLauncher.PresetLaunch();
        }
    }
}
