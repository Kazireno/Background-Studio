using BackgorundStudio_build2024._3._1_alpha.API;
using BackgorundStudio_build2024._3._1_alpha.Module;
using BackgorundStudio_build2024._3._1_alpha.Tools.Files;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using PluginAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgorundStudio_build2024._3._1_alpha.MainCore
{
    public class PresetLauncher
    {
        Log log;
        PluginLauncher pluginLauncher;
        PresetItemsXMLConfig presetItemsXMLConfig;
        Preset preset;
        bool Debug;
        string PresetName;

        IEnumerable<Plugin> EnablePlugins = new List<Plugin>();

        public PresetLauncher(Log log, PluginLauncher pluginLauncher, PresetItemsXMLConfig presetItemsXMLConfig, bool debug,string presetName)
        {
            this.log = log;
            this.pluginLauncher = pluginLauncher;
            this.presetItemsXMLConfig = presetItemsXMLConfig;
            Debug = debug;
            PresetName = presetName;
        }

        public void PresetLaunch() 
        {
            presetItemsXMLConfig.checkPresetConfig(PresetName);
            preset = new Preset(PresetName, presetItemsXMLConfig.GetPresetEnablePluginList(PresetName), presetItemsXMLConfig.GetPresetPluginConfig(PresetName));
            pluginLauncher.DisableAllPlugin();
            pluginLauncher.EnablePluginList(preset.EnablePlugin);
            pluginLauncher.PluginSubmitConfig(preset.EnablePlugin, preset.PluginConfig);
            pluginLauncher.StartDefaultWindow(preset.EnablePlugin);
            pluginLauncher.GivePluginHandle(pluginLauncher.GetPluginHandle(preset.EnablePlugin));
            pluginLauncher.GivePluginWindow(pluginLauncher.GetPluginWindow(preset.EnablePlugin));
            pluginLauncher.StartOhterWindow(preset.EnablePlugin);
        }

        public void PresetLaunch(string presetName)
        {
            presetItemsXMLConfig.checkPresetConfig(presetName);
            preset = new Preset(presetName, presetItemsXMLConfig.GetPresetEnablePluginList(presetName), presetItemsXMLConfig.GetPresetPluginConfig(presetName));
            pluginLauncher.DisableAllPlugin();
            pluginLauncher.ResetAllPlugin();
            pluginLauncher.EnablePluginList(preset.EnablePlugin);
            pluginLauncher.PluginSubmitConfig(preset.EnablePlugin, preset.PluginConfig);
            pluginLauncher.StartDefaultWindow(preset.EnablePlugin);
            pluginLauncher.GivePluginHandle(pluginLauncher.GetPluginHandle(preset.EnablePlugin));
            pluginLauncher.GivePluginWindow(pluginLauncher.GetPluginWindow(preset.EnablePlugin));
            pluginLauncher.StartOhterWindow(preset.EnablePlugin);
        }
    }
}
