using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.API
{
    /// <summary>
    /// 加载程序域 
    /// </summary>
    public class PluginLoadContext: AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        /// <summary>
        /// 加载程序域
        /// </summary>
        /// <param name="pluginPath">路径</param>
        public PluginLoadContext(string pluginPath)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        /// <summary>
        /// 加载程序域
        /// </summary>
        /// <param name="assemblyName">程序域</param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        /// <summary>
        /// 加载非托管DLL
        /// </summary>
        /// <param name="unmanagedDllName">路径</param>
        /// <returns></returns>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
