using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;

namespace PluginAPI
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {
        //加载程序集
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string winName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, uint fuFlage, uint timeout, IntPtr result);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc proc, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string winName);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hwnd, IntPtr parentHwnd);

        /// <summary>
        /// 将指定窗口置于壁纸层，请不要使用此方法！
        /// </summary>
        /// <param name="window">目标窗口</param>
        public static void BackgroundWindow(Window window)
        {
            IntPtr FormHandle = FindWindow("Progman", null);
            IntPtr result = IntPtr.Zero;
            SendMessageTimeout(FormHandle, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 2, result);
            EnumWindows((hwnd, lParam) =>
            {
                if (FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                {
                    IntPtr tempHwnd = FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);

                    ShowWindow(tempHwnd, 0);
                }
                return true;
            }, IntPtr.Zero);
            SetParent(new WindowInteropHelper(window).Handle, FormHandle);
        }

        /// <summary>
        /// 将指定窗口置于壁纸层，请使用此方法！
        /// </summary>
        /// <param name="window">目标窗口</param>
        public static void TackWindowOnBackgroun(Window window)
        {
            IntPtr FormHandle = FindWindow("Progman", null);
            BackgroundWindow(window);
            SetParent(new WindowInteropHelper(window).Handle, FormHandle);
        }

        /// <summary>
        /// 将指定窗口置于目标窗口中
        /// </summary>
        /// <param name="window">指定窗口</param>
        /// <param name="targetWindow">目标窗口</param>
        public static void TackWindowOnTargetWindow(Window window, IntPtr targetWindow)
        {
            SetParent(new WindowInteropHelper(window).Handle, targetWindow);
        }

        /// <summary>
        /// 将窗口变回普通窗口
        /// </summary>
        /// <param name="window">窗口</param>
        public static void TackBackWindow(Window window) 
        {
            window.Owner = null;
            SetParent(new WindowInteropHelper(window).Handle, IntPtr.Zero);
        }

        /// <summary>
        /// 配置项类型
        /// </summary>
        public enum ConfigType
        {
            /// <summary>
            /// 字符串
            /// </summary>
            String,
            /// <summary>
            /// 地址/路径
            /// </summary>
            Path,
            /// <summary>
            /// 百分比
            /// </summary>
            Percentage,
            /// <summary>
            /// 分辨率
            /// </summary>
            Resolution,
            /// <summary>
            /// 坐标
            /// </summary>
            Coordinate,
            /// <summary>
            /// 布尔
            /// </summary>
            Bool,
            /// <summary>
            /// RGB颜色
            /// </summary>
            RGB
        }

        /// <summary>
        /// 字符串转分辨率
        /// </summary>
        /// <param name="config">字符串</param>
        /// <returns></returns>
        public static int GetResolutionX(string? config)
        {
            if (config == "") return 0;
            return Convert.ToInt32(config.Split("*")[0]);
        }

        /// <summary>
        /// 字符串转分辨率
        /// </summary>
        /// <param name="config">字符串</param>
        /// <returns></returns>
        public static int GetResolutionY(string? config)
        {
            if (config == "") return 0;
            return Convert.ToInt32(config.Split("*")[1]);
        }

        /// <summary>
        /// 分辨率转字符串
        /// </summary>
        /// <param name="x">分辨率X</param>
        /// <param name="y">分辨率Y</param>
        /// <returns></returns>
        public static string IntToResolution(int? x, int? y)
        {
            if (x == null || y == null) return "";
            return x.ToString() + "*" + y.ToString();
        }

        /// <summary>
        /// 字符串转坐标
        /// </summary>
        /// <param name="config">字符串</param>
        /// <returns></returns>
        public static int GetCoordinateX(string? config)
        {
            if (config == null || config == "") return 0;
            return Convert.ToInt32(config.Split("-")[0]);
        }

        /// <summary>
        /// 字符串转坐标
        /// </summary>
        /// <param name="config">字符串</param>
        /// <returns></returns>
        public static int GetCoordinateY(string? config)
        {
            if (config == null || config == "") return 0;
            return Convert.ToInt32(config.Split("-")[1]);
        }

        /// <summary>
        /// 坐标转字符串
        /// </summary>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <returns></returns>
        public static string IntToCoordinate(int? x, int? y)
        {
            if (x == null || y == null) return "0-0";
            return x.ToString() + "-" + y.ToString();
        }

        /// <summary>
        /// 字符串转RGB
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
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

        /// <summary>
        /// RGB转字符串
        /// </summary>
        /// <param name="A">A</param>
        /// <param name="R">R</param>
        /// <param name="G">G</param>
        /// <param name="B">B</param>
        /// <returns></returns>
        public static string ARGBToString(int? A, int? R, int? G, int? B)
        {
            if (A == null || R == null || G == null || B == null) return "0,0,0,0";
            return A.ToString() + "," + R.ToString() + "," + G.ToString() + "," + B.ToString();
        }

        /// <summary>
        /// 字符串转布尔
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static bool StringToBool(string value)
        {
            if (value == "True") return true;
            else if (value == "False") return false;
            else return false;
        }

        /// <summary>
        /// 布尔转字符串
        /// </summary>
        /// <param name="value">布尔</param>
        /// <returns></returns>
        public static string BoolToString(bool value)
        {
            return value ? "True" : "False";
        }
    }
}
