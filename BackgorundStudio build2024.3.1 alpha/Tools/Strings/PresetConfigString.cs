using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.Tools.Strings
{
    internal static class PresetConfigString
    {
        public static string GetConfigName(string Config)
        {
            return Config.Split('$')[0];
        }

        public static string GetConfigDescription(string Config)
        {
            return Config.Split('$')[1];
        }

        public static string CombinationPluginConfig(string ConfigName, string ConfigDescription)
        {
            return ConfigName + "$" + ConfigDescription;
        }

        public static int GetResolutionX(string? config)
        {
            if (config == "") return 0;
            return Convert.ToInt32(config.Split("*")[0]);
        }

        public static int GetResolutionY(string? config)
        {
            if (config == "") return 0;
            return Convert.ToInt32(config.Split("*")[1]);
        }

        public static string IntToResolution(int? x, int? y)
        {
            if (x == null || y == null) return "";
            return x.ToString() + "*" + y.ToString();
        }

        public static int GetCoordinateX(string? config)
        {
            if (config == null || config == "") return 0;
            return Convert.ToInt32(config.Split("-")[0]);
        }

        public static int GetCoordinateY(string? config)
        {
            if (config == null || config == "") return 0;
            return Convert.ToInt32(config.Split("-")[1]);
        }

        public static string IntToCoordinate(int? x, int? y)
        {
            if (x == null || y == null) return "0-0";
            return x.ToString() + "-" + y.ToString();
        }

        public static List<int> StringToARGB(string? value)
        {
            if (value == null || value == "") return new List<int> { 0 };
            List<int> result = new List<int>();
            result.Add(Convert.ToInt32(value.Split(",")[0]));
            result.Add(Convert.ToInt32(value.Split(",")[1]));
            result.Add(Convert.ToInt32(value.Split(",")[2]));
            result.Add(Convert.ToInt32(value.Split(",")[3]));
            return result;
        }

        public static string ARGBToString(int? A, int? R, int? G, int? B)
        {
            if (A == null || R == null || G == null || B == null) return "0,0,0,0";
            return A.ToString() + "," + R.ToString() + "," + G.ToString() + "," + B.ToString();
        }

        public static bool StringToBool(string value)
        {
            if (value == "True") return true;
            else if (value == "False") return false;
            else return false;
        }

        public static string BoolToString(bool value)
        {
            return value ? "True" : "False";
        }
    }
}
