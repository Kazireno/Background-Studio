using BackgorundStudio_build2024._3._1_alpha.API;
using BackgorundStudio_build2024._3._1_alpha.ConfigWindows;
using BackgorundStudio_build2024._3._1_alpha.MainCore;
using BackgorundStudio_build2024._3._1_alpha.Module;
using BackgorundStudio_build2024._3._1_alpha.Tools.Files;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BackgorundStudio_build2024._3._1_alpha
{
    /// <summary>
    /// Main Core
    /// </summary>
    public partial class MainWindow : Window
    {
        MainXMLConfig xmlConfig;
        Log log;
        PresetLauncher presetLauncher;
        PluginLauncher pluginLauncher;
        Preset preset;
        PresetItemsXMLConfig itemsXMLConfig;

        string GetOtherPluginList = "";

        string GetHandler = "";

        public MainWindow(MainXMLConfig mainXMLConfig, PresetLauncher presetLauncher, PluginLauncher pluginLauncher, PresetItemsXMLConfig presetItemsXMLConfig, Log log)
        {
            InitializeComponent();
            this.xmlConfig = mainXMLConfig;
            this.log = log;
            this.presetLauncher = presetLauncher;
            this.pluginLauncher = pluginLauncher;
            this.itemsXMLConfig = presetItemsXMLConfig;
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPresetList();
            LoadWindowPluginList();
            LoadOtherPluginList();

            string presetName = xmlConfig.GetPresetName();
            foreach (var item in PresetList.Items)
            {
                if (item.ToString() == presetName) PresetList.SelectedItem = item;
            }
        }

        /// <summary>
        /// 窗口被激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            tackBackWindow();
        }

        /// <summary>
        /// 窗口失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Deactivated(object sender, EventArgs e)
        {
            tackWindowsTransparent();
        }

        /// <summary>
        /// 窗口MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }

        /// <summary>
        /// 窗口MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point a = Mouse.GetPosition(this);
                if (a.Y <= 30)
                {
                    tackWindowsTransparent();
                    this.DragMove();
                }
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                tackBackWindow();
            }
        }

        /// <summary>
        /// 使窗口变透明
        /// </summary>
        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            PresetList.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            WindowPluginList.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            OtherPluginList.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            ConfigList.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Create_Button.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Delete_Button.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        /// <summary>
        /// 使窗口变回原样
        /// </summary>
        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            PresetList.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            WindowPluginList.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            OtherPluginList.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            ConfigList.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Create_Button.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Delete_Button.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }

        /// <summary>
        /// 关闭键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 获取预设列表
        /// </summary>
        private void LoadPresetList()
        {
            List<string> presetList = xmlConfig.GetAllPreset();
            this.PresetList.Items.Clear();
            foreach (string preset in presetList)
            {
                PresetList.Items.Add(preset);
            }
        }

        /// <summary>
        /// 获取基层图层插件列表
        /// </summary>
        private void LoadWindowPluginList()
        {
            List<string> list = pluginLauncher.GetAllWindowPlugin();
            this.WindowPluginList.Items.Clear();
            foreach (string plugin in list)
            {
                WindowPluginList.Items.Add(plugin);
            }
        }

        /// <summary>
        /// 获取其他插件列表
        /// </summary>
        private void LoadOtherPluginList()
        {
            List<string> list = pluginLauncher.GetAllOtherPlugin();

            this.OtherPluginList.Items.Clear();
            foreach (string plugin in list)
            {
                OtherPluginList.Items.Add(new CheckBoxBind(plugin, false));
            }

        }

        /// <summary>
        /// 预设列表选择项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresetList.SelectedItem == null) return;
            presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
            xmlConfig.ChangeDefaultPreset(PresetList.SelectedItem.ToString());
            ConfigList.Items.Clear();
            WindowPluginList.SelectedItem = null;
            OtherPluginList.SelectedItem = null;

            List<CheckBoxBind> OtherListItem = new List<CheckBoxBind>();
            foreach (CheckBoxBind item in OtherPluginList.Items) OtherListItem.Add(item);
            foreach (CheckBoxBind item in OtherListItem)
            {
                if (itemsXMLConfig.GetPresetEnablePluginList(PresetList.SelectedItem.ToString()).Contains(item.Name))
                    item.Enable = true;
                else item.Enable = false;
            }

            OtherPluginList.Items.Clear();
            foreach (CheckBoxBind item in OtherListItem) OtherPluginList.Items.Add(item);

            GC.Collect();
        }

        /// <summary>
        /// 基层图层插件列表选择项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowPluginList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresetList.SelectedItem == null) return;
            if (WindowPluginList.SelectedItem == null) return;
            OtherPluginList.SelectedItem = null;
            ConfigList.Items.Clear();
            List<string>? config = pluginLauncher.GetPluginConfig(WindowPluginList.SelectedItem.ToString());
            List<PluginAPI.Tools.ConfigType> configType = pluginLauncher.GetPluginConfigType(WindowPluginList.SelectedItem.ToString());
            if (config == null) { return; }

            if (!itemsXMLConfig.CheckPluginInPreset(PresetList.SelectedItem.ToString(), WindowPluginList.SelectedItem.ToString()))
                itemsXMLConfig.ChangePresetWindowPlugin(PresetList.SelectedItem.ToString(), WindowPluginList.SelectedItem.ToString(), config);

            foreach (string con in config)
            {
                ConfigList.Items.Add(new ButtonBind(PresetConfigString.GetConfigName(con), PresetConfigString.GetConfigDescription(con), configType[config.IndexOf(con)]));
            }
            OtherPluginList.SelectedItem = null;
            presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
        }

        /// <summary>
        /// 其他插件列表选择项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherPluginList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresetList.SelectedItem == null) return;
            if (OtherPluginList.SelectedItem == null) return;
            WindowPluginList.SelectedItem = null;
            ConfigList.Items.Clear();
            GetOtherPluginList = OtherPluginList.SelectedItem.ToString();
            List<string>? config = pluginLauncher.GetPluginConfig(OtherPluginList.SelectedItem.ToString());
            List<PluginAPI.Tools.ConfigType> configType = pluginLauncher.GetPluginConfigType(OtherPluginList.SelectedItem.ToString());
            if (config == null) { return; }

            foreach (string con in config)
            {
                ConfigList.Items.Add(new ButtonBind(PresetConfigString.GetConfigName(con), PresetConfigString.GetConfigDescription(con), configType[config.IndexOf(con)]));
            }
            OtherPluginList.SelectedItem = null;
        }

        /// <summary>
        /// 配置列表按键被按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var data = btn.DataContext as ButtonBind;
            List<ButtonBind> items = new List<ButtonBind>();

            try
            {
                switch (data.ConfigType)
                {
                    case PluginAPI.Tools.ConfigType.String:
                        StringConfigWindow stringConfigWindow = new StringConfigWindow(data.Config);
                        stringConfigWindow.getTextHandler = str;
                        stringConfigWindow.Owner = this;
                        stringConfigWindow.ShowDialog();
                        if (GetHandler == "") break;
                        foreach (ButtonBind item in ConfigList.Items)
                        {
                            if (item.Name == data.Name)
                            {
                                item.Config = GetHandler;
                            }
                            items.Add(item);
                        }
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.Path:
                        Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                        dialog.DefaultExt = ".mp4";
                        dialog.Filter = @"视频媒体(*.mp4,*.avi)|*.mp4;*.avi|图片(*.jpg,*.png,*.gif,*.bmp)|*.jpg;*.png;*.gif;*.bmp|音频(*.mp3.*.wav)|*.mp3;*.wav";
                        Nullable<bool> result = dialog.ShowDialog();
                        if (result == true)
                        {
                            string Pagh = dialog.FileName;
                            foreach (ButtonBind item in ConfigList.Items)
                            {
                                if (item.Name == data.Name)
                                {
                                    item.Config = Pagh;
                                }
                                items.Add(item);
                            }
                            ConfigList.Items.Clear();
                            foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                            itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        }
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.Percentage:
                        PercentageConfigWindow percentageConfigWindow = new PercentageConfigWindow(Convert.ToInt32(data.Config));
                        percentageConfigWindow.getTextHandler = str;
                        percentageConfigWindow.Owner = this;
                        percentageConfigWindow.ShowDialog();
                        if (GetHandler == "") break;
                        foreach (ButtonBind item in ConfigList.Items)
                        {
                            if (item.Name == data.Name)
                            {
                                item.Config = GetHandler;
                            }
                            items.Add(item);
                        }
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.Resolution:
                        ResolutionConfigWindow resolutionConfigWindow = new ResolutionConfigWindow(data.Config);
                        resolutionConfigWindow.getTextHandler = str;
                        resolutionConfigWindow.Owner = this;
                        resolutionConfigWindow.ShowDialog();
                        if (GetHandler == "") break;
                        foreach (ButtonBind item in ConfigList.Items)
                        {
                            if (item.Name == data.Name)
                            {
                                item.Config = GetHandler;
                            }
                            items.Add(item);
                        }
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.Coordinate:
                        CoordinateConfigWindow coordinateConfigWindow = new CoordinateConfigWindow(data.Config);
                        coordinateConfigWindow.getTextHandler = str;
                        coordinateConfigWindow.Owner = this;
                        coordinateConfigWindow.ShowDialog();
                        if (GetHandler == "") break;
                        foreach (ButtonBind item in ConfigList.Items)
                        {
                            if (item.Name == data.Name)
                            {
                                item.Config = GetHandler;
                            }
                            items.Add(item);
                        }
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.Bool:

                        List<ButtonBind> Items = new List<ButtonBind>();
                        foreach (ButtonBind item in ConfigList.Items) Items.Add(item);
                        foreach (ButtonBind item in Items)
                            if (item.Name == data.Name)
                                if (data.Config == "True") item.Config = "False";
                                else if (data.Config == "False") item.Config = "True";
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in Items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GC.Collect();
                        break;
                    case PluginAPI.Tools.ConfigType.RGB:
                        RGBConfigWindow rGBConfigWindow = new RGBConfigWindow(data.Config);
                        rGBConfigWindow.getTextHandler = str;
                        rGBConfigWindow.Owner = this;
                        rGBConfigWindow.ShowDialog();
                        if (GetHandler == "") break;
                        foreach (ButtonBind item in ConfigList.Items)
                        {
                            if (item.Name == data.Name)
                            {
                                item.Config = GetHandler;
                            }
                            items.Add(item);
                        }
                        ConfigList.Items.Clear();
                        foreach (ButtonBind item in items) ConfigList.Items.Add(item);
                        itemsXMLConfig.UpdatePresetConfig(PresetList.SelectedItem.ToString(), getNowSelectPlugin(), getListConfig());
                        presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
                        GetHandler = "";
                        items = new List<ButtonBind>();
                        GC.Collect();
                        break;
                    default: break;
                }
            }
            catch (Exception ex) { }
        }

        //获取委托值
        public void str(string value) { GetHandler = value; }
        public void str(int value) { GetHandler = value.ToString(); }

        /// <summary>
        /// 创建新预设按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            CreatePreset createPreset = new CreatePreset();
            createPreset.getTextHandler = str;
            createPreset.ShowDialog();
            preset = new Preset("", new List<string>(), new List<List<string>>());
            preset.Name = GetHandler;
            itemsXMLConfig.WritePresetConfig(preset);
            GetHandler = "";
            LoadPresetList();
        }

        /// <summary>
        /// 删除预设
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PresetList.SelectedItem != null) itemsXMLConfig.DelXML(PresetList.SelectedItem.ToString());
            LoadPresetList();
        }

        /// <summary>
        /// 获取当前配置列表的配置信息
        /// </summary>
        /// <returns></returns>
        private List<string> getListConfig()
        {
            List<string> configs = new List<string>();
            foreach (ButtonBind item in ConfigList.Items)
            {
                configs.Add(PresetConfigString.CombinationPluginConfig(item.Name, item.Config));
            }
            return configs;
        }

        /// <summary>
        /// 获取现在被选择的插件列表
        /// </summary>
        /// <returns></returns>
        private string getNowSelectPlugin()
        {
            if (WindowPluginList.SelectedItem != null) return WindowPluginList.SelectedItem.ToString();
            else return GetOtherPluginList;
        }

        /// <summary>
        /// 其他插件列表复选框按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherPlugin_Click(object sender, RoutedEventArgs e)
        {
            var chb = sender as CheckBox;
            var date = chb.DataContext as CheckBoxBind;

            if ((bool)chb.IsChecked)
            {
                pluginLauncher.EnablePlugin(date.Name);
                itemsXMLConfig.AddPluginToPreset(PresetList.SelectedItem.ToString(), date.Name);
            }
            else
            {
                pluginLauncher.DisablePlugin(date.Name);
                itemsXMLConfig.DeletePluginInPreset(PresetList.SelectedItem.ToString(), date.Name);
            }
            presetLauncher.PresetLaunch(PresetList.SelectedItem.ToString());
        }
    }
}
