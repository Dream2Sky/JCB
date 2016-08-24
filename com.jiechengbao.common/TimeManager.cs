using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.common
{
    public class TimeManager
    {
        protected static readonly DateTime unixTPStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimestamp()
        {
            try
            {
                TimeSpan toNow = DateTime.Now.Subtract(unixTPStart);
                return (long)Math.Round(toNow.TotalSeconds);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
