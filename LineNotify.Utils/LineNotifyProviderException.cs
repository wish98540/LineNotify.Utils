using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineNotify.Utils
{
    /// <summary>Line Notify 異常訊息</summary>
	[Serializable]
    public class LineNotifyProviderException : Exception
    {
        public LineNotifyProviderException() { }
        public LineNotifyProviderException(string message) : base(message) { }
        public LineNotifyProviderException(string message, Exception inner) : base(message, inner) { }
        protected LineNotifyProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        /// <summary>
        /// 複寫 Message
        /// </summary>
        public override string Message => $"Exception: {base.Message}";
    }
}
