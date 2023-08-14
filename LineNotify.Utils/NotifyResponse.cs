using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LineNotify.Utils
{
    /// <summary>Line Notify 回應</summary>
    public class NotifyResponse
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 表頭檔
        /// </summary>
        public HttpHeaders Header { get; set; }
        /// <summary>
        /// 剩餘訊息量/小時
        /// </summary>
        public int X_RateLimit_Remaining
        {
            get
            {
                return Convert.ToInt32(Header.GetValues("X-RateLimit-Remaining").FirstOrDefault() ?? "0");
            }
        }
        /// <summary>
        /// 剩餘圖片量/小時
        /// </summary>
        public int X_RateLimit_ImageRemaining
        {
            get
            {
                return Convert.ToInt32(Header.GetValues("X-RateLimit-ImageRemaining").FirstOrDefault() ?? "0");
            }
        }
        /// <summary>
        /// 下次重置時間
        /// </summary>
        public DateTime X_RateLimit_Reset
        {
            get
            {
                var val = Header.GetValues("X-RateLimit-Reset").FirstOrDefault();
                return UnixTimeStampToDateTime(val).ToLocalTime();
            }
        }

        /// <summary>
        /// 取得 DateTime，透過 UnixTimeStamp
        /// </summary>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(string timeStamp)
        {
            var stamp = Convert.ToDouble(timeStamp);
            return UnixTimeStampToDateTime(stamp);
        }
        /// 
        /// <summary>
        /// 取得 DateTime，透過 UnixTimeStamp
        /// </summary>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double timeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
