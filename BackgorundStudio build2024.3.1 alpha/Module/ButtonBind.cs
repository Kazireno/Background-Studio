using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.Module
{
    internal class ButtonBind
    {
        public string Name { get; set; }
        public string Config { get; set; }
        public PluginAPI.Tools.ConfigType ConfigType { get; set; }

        public ButtonBind(string name, string config, PluginAPI.Tools.ConfigType configType)
        {
            Name = name;
            Config = config;
            ConfigType = configType;
        }

        public override string ToString() 
        {
            return Name + ":" + Config + "-" + ConfigType.ToString();
        }
    }
}
