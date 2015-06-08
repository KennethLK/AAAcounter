/* *
* ClassName: ObjectExtension
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 8:57:41 PM
* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAccounter.Model
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 小数点后三位为毫秒
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double GetMili1970(this DateTime date)
        {
            DateTime _1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            return (date - _1970).TotalMilliseconds / 1000;
        }

        public static DateTime ToDate(this long timespan)
        {
            DateTime _1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            return _1970.AddMilliseconds(timespan);
        }

        public static DateTime ToDate(this double timespan)
        {
            long ts = (long)(timespan * 1000);
            return ts.ToDate();
        }
    }
}
