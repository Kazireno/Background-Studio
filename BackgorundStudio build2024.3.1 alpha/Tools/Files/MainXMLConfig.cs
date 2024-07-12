using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
using BackgorundStudio_build2024._3._1_alpha.Tools.Logs;
using System.Diagnostics;

namespace BackgorundStudio_build2024._3._1_alpha.Tools.Files
{
    /// <summary>
    /// 主核心XML配置文件控制类
    /// </summary>
    public class MainXMLConfig
    {
        private string MainCoreVersion;
        private bool IsDebug;
        public string XMLVersion;
        string directory = Directory.GetCurrentDirectory();
        Log Log;

        /// <summary>
        /// 初始化XML控制类并读取配置文件
        /// </summary>
        /// <param name="mainCoreVersion">主核心版本</param>
        /// <param name="logFileName">日志文件名</param>
        /// <param name="isDebug">调试状态</param>
        public MainXMLConfig(string mainCoreVersion, Log log, bool isDebug)
        {
            MainCoreVersion = mainCoreVersion;
            IsDebug = isDebug;
            Log = log;

            //判断配置文件是否存在，不存在则创建新的
            if (!File.Exists("Config.xml"))
            {
                WriteXML();
            }
            //判断配置文件是否可读取，不可读取则创建新的
            if (!LoadXML())
            {
                DelXML();
                WriteXML();
            }
            //判断配置文件版本是否和核心相同，不同则更新
            if (XMLVersion != MainCoreVersion)
            {
                DelXML();
                WriteXML();
            }

        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        public void WriteXML()
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                XmlDeclaration xmlDecl = tDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                tDoc.AppendChild(xmlDecl);

                XmlElement Config = tDoc.CreateElement("Config");
                tDoc.AppendChild(Config);

                XmlElement mainCoreVersion = tDoc.CreateElement("MainCoreVersion");
                mainCoreVersion.InnerText = MainCoreVersion;
                Config.AppendChild(mainCoreVersion);

                XmlElement Preset = tDoc.CreateElement("Preset");
                Preset.InnerText = "DefaultPreset";
                Config.AppendChild(Preset);

                tDoc.Save(@"Config.xml");

                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "Finish Write XML Config .");

            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Write XML Config File !");
                if (IsDebug) Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());
                return;
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public bool LoadXML()
        {
            try
            {
                XmlDocument Load = new XmlDocument();
                Load.Load(@"Config.xml");

                XmlNode Config = Load.SelectSingleNode("Config");
                XmlNode mainCoreVersion = Config.SelectSingleNode("MainCoreVersion");

                XMLVersion = mainCoreVersion.InnerText;
                Log.NewLog(LogString.LogLevel.Info, LogString.LogType.Main, "The XML Config Version = " + mainCoreVersion.InnerText);

                return true;
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Load XML Config File !");
                if (IsDebug) Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 删除配置文件
        /// </summary>
        public void DelXML()
        {
            try
            {
                File.Delete(@"Config.xml");
            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Delete XML Config File !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());
            }
        }

        /// <summary>
        /// 获取预设名
        /// </summary>
        /// <returns></returns>
        public string GetPresetName()
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();

                tDoc.Load(@"Config.xml");

                XmlNode Config = tDoc.SelectSingleNode("Config");
                XmlNode Preset = Config.SelectSingleNode("Preset");

                return Preset.InnerText.ToString();

            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Read XML Config File !");
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取所有的预设
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllPreset()
        {
            List<string> preset = new List<string>();

            string save_directory = directory + "/Saves/";
            if (!Directory.Exists(save_directory))
            {
                Directory.CreateDirectory(save_directory);
                return preset;
            }
            var Files = Directory.GetFiles(save_directory, "*.xml");
            foreach (var file in Files)
            {
                string fileName = file.Split("/")[file.Split("/").Length - 1];
                preset.Add(fileName.ToString().Split(".")[0]);
            }
            return preset;
        }

        /// <summary>
        /// 改变默认启动预设
        /// </summary>
        /// <param name="presetName">预设名</param>
        public void ChangeDefaultPreset(string presetName)
        {
            try
            {
                XmlDocument tDoc = new XmlDocument();
                tDoc.Load(@"Config.xml");

                XmlNode Config = tDoc.SelectSingleNode("Config");
                XmlNode preset = Config.SelectSingleNode("Preset");
                preset.InnerText = presetName;
                tDoc.Save(@"Config.xml");

            }
            catch (Exception e)
            {
                Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, "Can not Load XML Config File !");
                if (IsDebug) Log.NewLog(LogString.LogLevel.Error, LogString.LogType.Main, e.ToString());

            }
        }
    }
}
