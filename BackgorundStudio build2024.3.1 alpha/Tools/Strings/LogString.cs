using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.Tools.Strings
{
    /// <summary>
    /// 日志所用字符串处理
    /// </summary>
    public static class LogString
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetTimeNow() 
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        }

        /// <summary>
        /// 返回日志严重等级字符串
        /// </summary>
        /// <param name="level">日志严重等级类型</param>
        /// <returns></returns>
        public static string getLevel(LogLevel level)
        {
            string LEVEL;
            switch (level)
            {
                case LogLevel.Debug:
                    LEVEL = "Debug";
                    break;
                case LogLevel.Error:
                    LEVEL = "Error";
                    break;
                case LogLevel.Warn:
                    LEVEL = "Warn";
                    break;
                case LogLevel.Info:
                    LEVEL = "Info";
                    break;
                default:
                    LEVEL = "???";
                    break;
            }
            return LEVEL;
        }

        /// <summary>
        /// 返回日志主体字符串
        /// </summary>
        /// <param name="type">日志发布主体类型</param>
        /// <returns></returns>
        public static string getType(LogType type)
        {
            string TYPE;
            switch (type)
            {
                case LogType.Main:
                    TYPE = "Main";
                    break;
                case LogType.Plugin:
                    TYPE = "Plugin";
                    break;
                case LogType.API:
                    TYPE = "API";
                    break;
                default:
                    TYPE = "???";
                    break;
            }
            return TYPE;
        }

        /// <summary>
        /// 日志等级
        /// </summary>
        public enum LogLevel 
        {
            Info,
            Warn,
            Error,
            Debug
        }

        /// <summary>
        /// 日志发布主体
        /// </summary>
        public enum LogType 
        {
            Main,
            API,
            Plugin
        }
    }
}
