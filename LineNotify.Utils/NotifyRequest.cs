using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineNotify.Utils
{
    /// <summary>LINE Notify 請求</summary>
	public class NotifyRequest
    {
        /// <summary>
        /// Bearer Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 貼圖包ID
        /// </summary>
        public string StickerPackageId { get; set; }
        /// <summary>
        /// 貼圖ID
        /// </summary>
        public string StickerId { get; set; }
        /// <summary>
        /// 圖片網址
        /// </summary>
        public string ImageLink { get; set; }
        /// <summary>
        /// 縮圖網址
        /// </summary>
        public string ImageThumbnailLink { get; set; }
    }
}
