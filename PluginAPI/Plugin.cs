using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PluginAPI
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface Plugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 插件所有者
        /// </summary>
        string owner { get; }

        /// <summary>
        /// 插件介绍
        /// </summary>
        string profile { get; }

        /// <summary>
        /// 插件版本
        /// </summary>
        string version { get; }

        /// <summary>
        /// 启用状态
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// 图层透明度
        /// </summary>
        short Alpha { get; set; }

        /// <summary>
        /// 初始化插件
        /// <para>你应该将插件初始化中所有需要做的工作放在这里。</para>
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// 启动插件
        /// <para>你应当将启动插件核心功能之前所有的工作放在这里</para>
        /// </summary>
        /// <returns></returns>
        bool Launch();

        /// <summary>
        /// 开启主要功能
        /// <para>所有预备工作准备完成后核心将会调用此方法来启动插件的核心功能</para>
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// 窗口类型：
        /// <para>0-基层窗口    置于壁纸最底层显示底层图层的窗口</para>
        /// <para>1-组件窗口    置于基层窗口上，用作组件或装饰的窗口</para>
        /// <para>2-无窗口      功能性插件无需要显示在壁纸层的窗口</para>
        /// </summary>
        int WindowstType { get; }

        /// <summary>
        /// 提交窗口句柄
        /// </summary>
        /// <returns></returns>
        IntPtr? submitWindowsHandle();

        /// <summary>
        /// 获取基层窗口句柄
        /// </summary>
        /// <param name="intPtr"></param>
        void getDefaultWindowHandle(IntPtr intPtr);

        /// <summary>
        /// 提交插件窗口
        /// </summary>
        /// <returns></returns>
        Window? submitWindow();

        /// <summary>
        /// 获取基层窗口
        /// </summary>
        /// <param name="window">基层窗口</param>
        void getDefaultWindow(Window? window);

        /// <summary>
        /// 获取储存的配置
        /// </summary>
        /// <param name="config">配置信息</param>
        void getConfig(List<string> config);

        /// <summary>
        /// 提交需储存的配置
        /// </summary>
        /// <returns></returns>
        List<string>? submitConfig();

        /// <summary>
        /// 重置插件
        /// </summary>
        void Reset();

        /// <summary>
        /// 获取配置项类型
        /// </summary>
        /// <returns></returns>
        List<Tools.ConfigType> GetConfigTypes();
    }
}