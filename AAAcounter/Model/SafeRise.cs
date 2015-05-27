using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AAAcounter.Model
{
    public class SafeRaise
    {
        /// <summary>
        /// 安全调用Action
        /// </summary>
        public static void Raise(Action action)
        {
            try
            {
                if (action != null)
                    action();
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[0]！" + action.GetType());
#endif
            }
        }

        /// <summary>
        /// 安全调用Action
        /// </summary>
        public static async void DispatcherRaise<T>(Action<T> action, T args)
        {

            if (action == null)
                return;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
             {
                 try
                 {

                     action(args);
                 }
                 catch
                 {
#if DEBUG
                 Logger.Info("SafeRaise", "调用Safe Action出错:[1]！" + action.Target.ToString());
#endif
             }
             });

        }

        /// <summary>
        /// 安全调用Action
        /// </summary>
        public static async void DispatcherRaise<T, T1>(Action<T, T1> action, T args, T1 args1)
        {

            if (action == null)
                return;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                try
                {

                    action(args, args1);
                }
                catch
                {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[10]！" + action.Target.ToString());
#endif
            }
            });

        }

        /// <summary>
        /// 安全调用Action
        /// </summary>
        public static async void DispatcherRaise(Action action)
        {
            if (action == null)
                return;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
           {
               try
               {
                   action();
               }
               catch
               {
#if DEBUG
               Logger.Info("SafeRaise", "调用Safe Action出错:[2]！" + action.Target.ToString());
#endif
           }
           });
        }

        /// <summary>
        /// 安全调用Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T Raise<T>(Func<T> func)
        {
            try
            {
                if (func != null)
                    return func.Invoke();

                return (T)Activator.CreateInstance(typeof(T));
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[8]！" + func.ToString());
#endif
                return (T)Activator.CreateInstance(typeof(T));
            }
        }

        /// <summary>
        /// 安全调用Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public static T1 Raise<T, T1>(Func<T, T1> func, T args)
        {
            try
            {
                if (func != null)
                    return func.Invoke(args);

                return (T1)Activator.CreateInstance(typeof(T1));
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[3]！" + func.ToString());
#endif
                return (T1)Activator.CreateInstance(typeof(T1));
            }
        }

        /// <summary>
        /// 安全调用Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public static T2 Raise<T, T1, T2>(Func<T, T1, T2> func, T args, T1 args1)
        {
            try
            {
                if (func != null)
                    return func.Invoke(args, args1);

                return (T2)Activator.CreateInstance(typeof(T1));
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[4]！" + func.ToString());
#endif
                return (T2)Activator.CreateInstance(typeof(T1));
            }
        }

        /// <summary>
        /// 安全调用Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public static void Raise<T>(Action<T> action, T args)
        {
            try
            {
                if (action != null)
                    action(args);

            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[5]！" + action.ToString());
#endif
            }
        }


        /// <summary>
        /// 安全调用Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public static void Raise<T, T1>(Action<T, T1> action, T args, T1 args1)
        {
            try
            {
                if (action != null)
                    action(args, args1);
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe Action出错:[6]！" + action.ToString());
#endif
            }
        }


        /// <summary>
        /// 安全调用无参数的eventhandle
        /// </summary>
        /// <param name="eventToRaise"></param>
        /// <param name="sender"></param>
        public static void Raise(EventHandler eventToRaise, object sender)
        {
            if (eventToRaise == null)
                return;

            try
            {
                eventToRaise(sender, EventArgs.Empty);
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe EventHandler出错:[7]！" + eventToRaise.ToString());
#endif
            }

        }

        /// <summary>
        /// 安全调用带参数的eventhandle
        /// </summary>
        /// <param name="eventToRaise"></param>
        /// <param name="sender"></param>
        public static void Raise(EventHandler<EventArgs> eventToRaise, object sender)
        {
            Raise(eventToRaise, sender, EventArgs.Empty);
        }

        /// <summary>
        /// 安全调用带参数的eventhandle
        /// </summary>
        /// <param name="eventToRaise"></param>
        /// <param name="sender"></param>
        public static void Raise<T>(EventHandler<T> eventToRaise, object sender, T args) where T : EventArgs
        {
            if (eventToRaise == null)
                return;

            if (eventToRaise != null)
            {
                //try
                //{
                eventToRaise(sender, args);
                //                }
                //                catch
                //                {
                //#if DEBUG
                //                    Debug.WriteLine("调用Safe EventHandler出错！：" + eventToRaise.ToString());
                //#endif
                //                }
            }

        }


        /// <summary>
        /// 安全调用无参数的eventhandle
        /// </summary>
        /// <param name="eventToRaise"></param>
        /// <param name="sender"></param>
        public static void Raise(EventHandler eventToRaise, object sender, EventArgs args)
        {
            if (eventToRaise == null)
                return;

            try
            {
                eventToRaise(sender, args);
            }
            catch
            {
#if DEBUG
                Logger.Info("SafeRaise", "调用Safe EventHandler出错:[8]！" + eventToRaise.ToString());
#endif
            }
        }
    }
}