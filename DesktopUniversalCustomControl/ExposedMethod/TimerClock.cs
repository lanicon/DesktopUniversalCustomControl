using System;
using System.Windows.Threading;

namespace DesktopUniversalCustomControl.ExposedMethod
{
    /// <summary>
    /// 时钟
    /// </summary>
    public class TimerClock
    {
        public TimerClock()
        {

        }

        /// <summary>
        /// 时钟方法
        /// </summary>
        /// <param name="second">时间间隔</param>
        /// <param name="action">委托方法</param>
        /// <param name="isStart">是否开启时钟</param>
        public static DispatcherTimer IntervalMultiSeconds(DispatcherTimer timer, double second, Action action)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(second);
            timer.Tick += delegate { action(); };
            return timer;
        }
    }
}
