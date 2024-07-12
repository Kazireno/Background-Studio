using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.Module
{
    public class Preset
    {
        public string Name;
        public List<string> EnablePlugin;
        public List<List<string>> PluginConfig;

        public Preset(string name, List<string> enablePlugin, List<List<string>> pluginConfig)
        {
            Name = name;
            EnablePlugin = enablePlugin;
            PluginConfig = pluginConfig;
        }
    }
}
