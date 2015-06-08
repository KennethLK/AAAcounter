using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAccounter.Model
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 打印信息
        /// </summary> 
        public static void Info(string methodName, string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("----【" + methodName + "】：" + message + ",Time:" + DateTime.Now.ToString("mm:ss.fff"));
#endif
        }

        /// <summary>
        /// 不推荐使用
        /// </summary> 
        public static void Info(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("---- " + message + ",Time:" + DateTime.Now.ToString("mm:ss.fff"));
#endif
        }

        public static void Warn(string methodName, string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("----【" + methodName + "】：," + message + ",Time:" + DateTime.Now.ToString("mm:ss.fff"));
#endif
        }

        public static void Warn(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("---- " + message + ",Time:" + DateTime.Now.ToString("mm:ss.fff"));
#endif
        }
    }
}
