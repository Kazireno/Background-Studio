using BackgorundStudio_build2024._3._1_alpha.Module;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
using PluginAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Xml.Linq;

namespace BackgorundStudio_build2024._3._1_alpha.API
{
    /// <summary>
    /// 插件加载器
    /// </summary>
    public class PluginLauncher
    {
        private string MainCoreVersion;
        private bool IsDebug;
        private string directory = Directory.GetCurrentDirectory();
        private Log Log;
        public int PluginQuantity = 0;

        IEnumerable<Plugin> plugins;

        private string[] PluginPaths = { "" };

        /// <summary>
        /// 初始化插件加载器
        /// </summary>
        /// <param name="mainCoreVersion">主核心版本</param>
        /// <param name="logFileName">日志文件名称</param>
        /// <param name="isDebug">调试模式</param>
        public PluginLauncher(string mainCoreVersion, Log log, bool isDebug)
        {
            MainCoreVersion = mainCoreVersion;
            IsDebug = isDebug;
            Log = log;
            APIInit();
        }

        /// <summary>
        /// 开始初始化插件加载器
        /// </summary>
        public void APIInit()
        {
            Log.NewLog(LogString.LogLevel.Info, LogString.LogType.API, "Start Init Plugin API");
            getPluginPaths();
            plugins = getPlugins();
            getPluginInfo();
        }

        /// <summary>
        /// 获取插件路径
        /// </summary>
        public void getPluginPaths()
        {
            Log.NewLog(LogString.LogLevel.Info, LogString.LogType.API, "Start Get Plugin Paths");

            directory = directory + "/Plugins/";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var files = Directory.GetFiles(directory, "*.dll");
            PluginQuantity = files.Length;
            PluginPaths = new string[PluginQuantity];
            PluginPaths = files;

            if (PluginQuantity == 0)
            {
                Log.NewLog(LogString.LogLevel.Warn, LogString.LogType.API, "Can't Get Any Dll File in Plugins Directory ! Please Check Your Software !");
                throw new ApplicationException("Can't Find Any Plugins");   //抛出异常
            }

            foreach (var file in files)
            {
                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.API, "Find DLL in :" + file);
            }
        }

        /// <summary>
        /// 加载插件及插件命令
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Plugin> getPlugins()
        {
            IEnumerable<Plugin> plugins = PluginPaths.SelectMany(PluginPaths =>
            {
                Assembly pluginAssembly = LoadPlugin(PluginPaths);
                return CreateCommands(pluginAssembly);
            }).ToList();

            return plugins;
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="pluginLocation">插件路径</param>
        /// <returns></returns>
        static Assembly LoadPlugin(string pluginLocation)
        {
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(pluginLocation));
        }

        /// <summary>
        /// 加载插件命令
        /// </summary>
        /// <param name="assembly">插件域</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">异常：未发现可用的插件命令</exception>
        static IEnumerable<Plugin> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(Plugin).IsAssignableFrom(type))
                {
                    Plugin result = Activator.CreateInstance(type) as Plugin;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            }
        }

        /// <summary>
        /// 获取插件信息
        /// </summary>
        public void getPluginInfo()
        {
            foreach (Plugin plugin in plugins)
            {
                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.API, "Plugin Name : " + plugin.Name + " Owner : " + plugin.owner + " Window Type = " + plugin.WindowstType.ToString());
            }
            Log.NewLog(LogString.LogLevel.Info, LogString.LogType.API, "Get Plugin : " + PluginQuantity);
        }

        /// <summary>
        /// 获取插件列表
        /// </summary>
        /// <returns></returns>
        public string[] getPluginNames()
        {
            string[] list = { "PluginName" };

            foreach (Plugin plugin in plugins)
            {
                List<string> v = list.ToList();
                v.Add(plugin.Name);
                list = v.ToArray();
            }
            return list;
        }

        /// <summary>
        /// 获取插件域
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Plugin> getPluginAssemby()
        {
            return plugins;
        }

        /// <summary>
        /// 调用插件Start()事件
        /// </summary>
        public void StartPlugin()
        {
            foreach (Plugin plugin in plugins)
            {
                plugin.Start();
            }
        }

        /// <summary>
        /// 调用插件Init()事件
        /// </summary>
        public void InitPlugin()
        {
            foreach (Plugin plugin in plugins)
            {
                plugin.Init();
            }
        }

        /// <summary>
        /// 调用插件Launch()事件
        /// </summary>
        public void LaunchPlugin()
        {
            foreach (Plugin plugin in plugins)
            {
                plugin.Launch();
            }
        }

        /// <summary>
        /// 启用插件
        /// </summary>
        /// <param name="PluginName">插件名</param>
        public void EnablePlugin(string PluginName)
        {
            foreach (Plugin plugin in plugins)
            {
                if (plugin.Name == PluginName) plugin.Enable = true;
            }
        }

        /// <summary>
        /// 禁用插件
        /// </summary>
        /// <param name="PluginName">插件名</param>
        public void DisablePlugin(string PluginName)
        {
            foreach (Plugin plugin in plugins)
            {
                if (plugin.Name == PluginName) plugin.Enable = false;
            }
        }

        /// <summary>
        /// 禁用所有插件
        /// </summary>
        public void DisableAllPlugin()
        {
            foreach (Plugin plugin in plugins) { plugin.Enable = false; }
        }

        /// <summary>
        /// 获取指定列表的插件配置
        /// </summary>
        /// <param name="Name">插件列表</param>
        /// <returns></returns>
        public List<List<string>> GetListConfig(List<string> Name)
        {
            List<List<string>> Config = new List<List<string>>();
            foreach (string name in Name)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == name) Config.Add(plugin.submitConfig());
                }
            }
            return Config;
        }

        /// <summary>
        /// 启动底层图层插件
        /// </summary>
        /// <param name="Name">插件列表</param>
        public void StartDefaultWindow(List<string> Name)
        {
            foreach (string name in Name)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.WindowstType == 0)
                    {
                        plugin.Init();
                        plugin.Launch();
                        plugin.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 启动其他图层插件
        /// </summary>
        /// <param name="Name">插件列表</param>
        public void StartOhterWindow(List<string> Name)
        {
            foreach (string name in Name)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.WindowstType != 0 && plugin.Name == name)
                    {
                        plugin.Init();
                        plugin.Launch();
                        plugin.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 启用指定列表的插件
        /// </summary>
        /// <param name="List">列表</param>
        public void EnablePluginList(List<string> List)
        {
            foreach (string name in List)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == name) plugin.Enable = true;
                }
            }
        }

        /// <summary>
        /// 向指定列表的插件提交配置信息
        /// </summary>
        /// <param name="List"></param>
        /// <param name="Config"></param>
        public void PluginSubmitConfig(List<string> List, List<List<string>> Config)
        {
            foreach (string name in List)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == name)
                    {
                        List<string> config = new List<string>();
                        foreach (string key in Config[List.IndexOf(name)])
                        {
                            config.Add(PresetConfigString.GetConfigDescription(key));
                        }
                        plugin.getConfig(config);
                    }
                }
            }

        }

        /// <summary>
        /// 重置所有插件
        /// </summary>
        public void ResetAllPlugin()
        {
            foreach (Plugin plugin in plugins)
            {
                plugin.Reset();
            }
        }

        /// <summary>
        /// 获取所有基层图层插件名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllWindowPlugin()
        {
            List<string> list = new List<string>();
            foreach (Plugin plugin in plugins)
            {
                if (plugin.WindowstType == 0) list.Add(plugin.Name);
            }
            return list;
        }

        /// <summary>
        /// 获取所有非基层图层插件名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllOtherPlugin()
        {
            List<string> list = new List<string>();
            foreach (Plugin plugin in plugins)
            {
                if (plugin.WindowstType != 0) list.Add(plugin.Name);
            }
            return list;
        }

        /// <summary>
        /// 获取指定插件的配置类型
        /// </summary>
        /// <param name="Name">插件名</param>
        /// <returns></returns>
        public List<PluginAPI.Tools.ConfigType>? GetPluginConfigType(string Name)
        {
            foreach (Plugin plugin in plugins)
            {
                if (plugin.Name == Name) return plugin.GetConfigTypes();
            }
            return null;
        }

        /// <summary>
        /// 获取指定插件的配置信息
        /// </summary>
        /// <param name="Name">插件名</param>
        /// <returns></returns>
        public List<string> GetPluginConfig(string Name)
        {
            List<string> Con = new List<string>();
            foreach (Plugin plugin in plugins)
            {
                if (plugin.Name == Name)
                {
                    Con= plugin.submitConfig();
                }
            }
            return Con;
        }

        /// <summary>
        /// 获取插件列表里基层窗口句柄
        /// </summary>
        /// <param name="Name">插件列表</param>
        /// <returns></returns>
        public IntPtr? GetPluginHandle(List<string> Name) 
        {
            IntPtr? intPtr = IntPtr.Zero;
            foreach (string name in Name)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == name && plugin.WindowstType == 0) intPtr = plugin.submitWindowsHandle();
                }
            }
            return intPtr;
        }

        /// <summary>
        /// 提交基层窗口句柄
        /// </summary>
        /// <param name="intPtr">基层窗口句柄</param>
        public void GivePluginHandle(IntPtr? intPtr) 
        {
            foreach (Plugin plugin in plugins) 
            {
                plugin.getDefaultWindowHandle((IntPtr)intPtr);
            }
        }

        /// <summary>
        /// 获取基层控件窗口
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Window GetPluginWindow(List<string> Name) 
        {
            Window window = null;
            foreach (string name in Name)
            {
                foreach (Plugin plugin in plugins)
                {
                    if (plugin.Name == name && plugin.WindowstType == 0) window = plugin.submitWindow();
                }
            }
            return window;
        }

        /// <summary>
        /// 提交基层窗口
        /// </summary>
        /// <param name="window">窗口</param>
        public void GivePluginWindow(Window window) 
        {
            foreach (Plugin plugin in plugins)
            {
                plugin.getDefaultWindow(window);
            }
        }
    }
}
