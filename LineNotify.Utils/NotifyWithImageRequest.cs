using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineNotify.Utils
{
    /// <summary>LINE Notify 請求(帶圖片)</summary>
	public class NotifyWithImageRequest
    {
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public byte[] FileBytes
        {
            get
            {
                if (File.Exists(FilePath)) return File.ReadAllBytes(FilePath);
                return new byte[0];
            }
        }
    }
}
