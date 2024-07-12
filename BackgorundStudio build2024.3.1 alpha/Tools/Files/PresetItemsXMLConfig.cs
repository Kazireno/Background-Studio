using BackgorundStudio_build2024._3._1_alpha.API;
using BackgorundStudio_build2024._3._1_alpha.Module;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
using PluginAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BackgorundStudio_build2024._3._1_alpha.Tools.Files
{
    /// <summary>
    /// 预设配置文件控制类
    /// </summary>
    public class PresetItemsXMLConfig
    {
        private bool Debug;
        private string directory = Directory.GetCurrentDirectory() + "/Saves/";

        public string[] PluginName;
        public string[] PluginConfig;

        PluginLauncher pluginLauncher;

        IEnumerable<Plugin> plugins;

        Log Log;

        /// <summary>
        ///  预设配置文件控制类
        /// </summary>
        /// <param name="log">日志类</param>
        /// <param name="debug">调试状态</param>
        /// <param name="PresetName">预设项目名</param>
        /// <param name="Plugins">插件集</param>
        public PresetItemsXMLConfig(Log log, bool debug, IEnumerable<Plugin> Plugins, PluginLauncher pluginLauncher)
        {
            this.Debug = debug;
            Log = log;
            plugins = Plugins;
            this.pluginLauncher = pluginLauncher;
        }

        /// <summary>
        /// 获取插件信息
        /// </summary>
        /// <param name="name">插件名</param>
        /// <param name="config">插件配置</param>
        public void getPluginInfo(string[] name, string[] config)
        {
            PluginName = name;
            PluginConfig = config;
        }

        /// <summary>
        /// 检查预设文件
        /// </summary>
        /// <param name="PresetName">预设名</param>
        public void checkPresetConfig(string PresetName)
        {
            Log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Start Get Preset Config");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                CreateDefaultPresetConfig();
            }

            if (!File.Exists(directory + PresetName + ".xml")) return;
        }

        /// <summary>
        /// 创建默认预设
        /// </summary>
        public void CreateDefaultPresetConfig()
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                XmlDeclaration xmlDecl = tDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                tDoc.AppendChild(xmlDecl);

                XmlElement Config = tDoc.CreateElement("Config");
                tDoc.AppendChild(Config);

                XmlElement Name = tDoc.CreateElement("Name");
                Name.InnerText = "DefaultPreset";
                Config.AppendChild(Name);



                tDoc.Save(directory + "DefaultPreset.xml");

                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Create Default Preset .");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Create Default Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }

        /// <summary>
        /// 更新预设信息
        /// </summary>
        /// <param name="PresetName">预设名</param>
        /// <returns></returns>
        public Preset GetPreset(string PresetName)
        {
            Preset preset = null;
            preset.Name = PresetName;
            preset.EnablePlugin = GetPresetEnablePluginList(PresetName);

            return preset;
        }

        /// <summary>
        /// 获取被启用的程序集
        /// </summary>
        /// <param name="EnableList">插件启用列表</param>
        /// <returns></returns>
        public IEnumerable<Plugin> GetEnablePlugin(List<string> EnableList)
        {
            IEnumerable<Plugin> plugins = new List<Plugin>();
            foreach (Plugin plugin in plugins)
            {
                foreach (string enablePlugin in EnableList)
                {
                    if (plugin.Name == enablePlugin) { plugins.Append(plugin); plugin.Enable = true; }
                }
            }
            return plugins;
        }

        /// <summary>
        /// 获取预设需要的插件列表
        /// </summary>
        /// <param name="PresetName">预设名</param>
        /// <returns></returns>
        public List<string> GetPresetEnablePluginList(string PresetName)
        {
            List<string> PluginList = new List<string>();

            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + PresetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");
                XmlNode Name = config.SelectSingleNode("Name");

                foreach (XmlNode node in config.ChildNodes)
                {
                    if (node.Name != "Name") PluginList.Add(node.Name);
                }
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Get Preset Plugin List !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
            return PluginList;
        }

        /// <summary>
        /// 获取预设插件配置信息
        /// </summary>
        /// <param name="PresetName">插件列表</param>
        /// <returns></returns>
        public List<List<string>> GetPresetPluginConfig(string PresetName)
        {
            List<List<string>> Config = new List<List<string>>();

            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + PresetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");

                foreach (XmlNode node in config.ChildNodes)
                {
                    if (node.Name != "Name")
                    {
                        List<string> PluginConfig = new List<string>();
                        foreach (XmlNode configs in node.ChildNodes)
                        {
                            if (configs.Name != "PluginName") PluginConfig.Add(PresetConfigString.CombinationPluginConfig(configs.Name, configs.InnerText));
                        }
                        Config.Add(PluginConfig);
                    }
                }
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Get Preset Plugin Config !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
            return Config;
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="preset">预设</param>
        public void WritePresetConfig(Preset preset)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                XmlDeclaration xmlDecl = tDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                tDoc.AppendChild(xmlDecl);

                XmlElement Config = tDoc.CreateElement("Config");
                tDoc.AppendChild(Config);

                XmlElement Name = tDoc.CreateElement("Name");
                Name.InnerText = preset.Name;
                Config.AppendChild(Name);

                foreach (string plugin in preset.EnablePlugin)
                {
                    XmlElement Plugin = tDoc.CreateElement(plugin);
                    Config.AppendChild(Plugin);

                    List<string> PluginConfig = preset.PluginConfig[preset.EnablePlugin.IndexOf(plugin)];
                    foreach (string pluginConfig in PluginConfig)
                    {
                        string ConfigName = PresetConfigString.GetConfigName(pluginConfig);
                        string ConfigDescription = PresetConfigString.GetConfigDescription(pluginConfig);

                        XmlElement Item = tDoc.CreateElement(ConfigName);
                        Item.InnerText = ConfigDescription;
                        Plugin.AppendChild(Item);
                    }
                }
                tDoc.Save(directory + preset.Name + ".xml");
                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Create Preset :" + preset.Name);
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Create Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }

        /// <summary>
        /// 更新预设配置
        /// </summary>
        /// <param name="presetName">预设名</param>
        /// <param name="pluginName">插件名</param>
        /// <param name="pluginConfig">插件配置</param>
        public void UpdatePresetConfig(string presetName, string pluginName, List<string> pluginConfig)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + presetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");

                if (config.SelectSingleNode(pluginName) != null) config.RemoveChild(config.SelectSingleNode(pluginName));

                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == pluginName)
                    {
                        XmlElement pug = tDoc.CreateElement(plugin.Name);
                        config.AppendChild(pug);

                        XmlElement Name = tDoc.CreateElement("PluginName");
                        Name.InnerText = plugin.Name;
                        pug.AppendChild(Name);

                        foreach (string con in pluginConfig)
                        {
                            string ConfigName = PresetConfigString.GetConfigName(con);
                            string ConfigDescription = PresetConfigString.GetConfigDescription(con);

                            XmlElement Item = tDoc.CreateElement(ConfigName);
                            Item.InnerText = ConfigDescription;
                            pug.AppendChild(Item);
                        }
                    }
                }

                tDoc.Save(directory + "/" + presetName + ".xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Update Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }

        /// <summary>
        /// 删除预设文件
        /// </summary>
        public void DelXML(string preset)
        {
            try
            {
                File.Delete(directory + preset + ".xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Delete Preset File !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());
            }
        }

        /// <summary>
        /// 改变预设基层窗口插件
        /// </summary>
        /// <param name="presetName">预设名</param>
        /// <param name="pluginName">插件名</param>
        /// <param name="pluginConfig">插件配置</param>
        public void ChangePresetWindowPlugin(string presetName, string pluginName, List<string> pluginConfig)
        {
            List<string> AllWindowPlugin = new List<string>();
            foreach (Plugin plugin in plugins)
            {
                if (plugin.WindowstType == 0) AllWindowPlugin.Add(plugin.Name);
            }

            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + presetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");

                foreach (string wPlugin in AllWindowPlugin)
                {
                    if (config.SelectSingleNode(wPlugin) != null) config.RemoveChild(config.SelectSingleNode(wPlugin));
                }

                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == pluginName)
                    {
                        XmlElement pug = tDoc.CreateElement(plugin.Name);
                        config.AppendChild(pug);

                        XmlElement Name = tDoc.CreateElement("PluginName");
                        Name.InnerText = plugin.Name;
                        pug.AppendChild(Name);

                        foreach (string con in pluginConfig)
                        {
                            string ConfigName = PresetConfigString.GetConfigName(con);
                            string ConfigDescription = PresetConfigString.GetConfigDescription(con);

                            XmlElement Item = tDoc.CreateElement(ConfigName);
                            Item.InnerText = ConfigDescription;
                            pug.AppendChild(Item);
                        }
                    }
                }

                tDoc.Save(directory + "/" + presetName + ".xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Update Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }

        /// <summary>
        /// 检查预设中是否使用目标插件
        /// </summary>
        /// <param name="presetName">预设名</param>
        /// <param name="pluginName">插件名</param>
        /// <returns></returns>
        public bool CheckPluginInPreset(string presetName, string pluginName)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + presetName + ".xml");

                XmlNode Config = tDoc.SelectSingleNode("Config");

                foreach (XmlNode item in Config.ChildNodes)
                {
                    if (item.Name == pluginName)
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Check Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
            return false;
        }

        /// <summary>
        /// 删除预设中对插件的引用
        /// </summary>
        /// <param name="presetName">预设名</param>
        /// <param name="pluginName">插件名</param>
        public void DeletePluginInPreset(string presetName, string pluginName)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + presetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");

                if (config.SelectSingleNode(pluginName) != null) config.RemoveChild(config.SelectSingleNode(pluginName));

                tDoc.Save(directory + "/" + presetName + ".xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Update Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }

        /// <summary>
        /// 向预设增加新的插件引用
        /// </summary>
        /// <param name="presetName">预设名</param>
        /// <param name="pluginName">插件名</param>
        public void AddPluginToPreset(string presetName, string pluginName)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(directory + "/" + presetName + ".xml");

                XmlNode config = tDoc.SelectSingleNode("Config");

                List<string> configs = pluginLauncher.GetPluginConfig(pluginName);

                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == pluginName)
                    {
                        XmlElement pug = tDoc.CreateElement(plugin.Name);
                        config.AppendChild(pug);

                        XmlElement Name = tDoc.CreateElement("PluginName");
                        Name.InnerText = plugin.Name;
                        pug.AppendChild(Name);

                        foreach (string con in configs)
                        {
                            string ConfigName = PresetConfigString.GetConfigName(con);
                            string ConfigDescription = PresetConfigString.GetConfigDescription(con);

                            XmlElement Item = tDoc.CreateElement(ConfigName);
                            Item.InnerText = ConfigDescription;
                            pug.AppendChild(Item);
                        }
                    }
                }
                tDoc.Save(directory + "/" + presetName + ".xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can't Update Preset !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.Message);
            }
        }
    }
}