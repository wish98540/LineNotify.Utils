using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

namespace LineNotify.Utils
{
    public class LineNotifyProvider
    {
        /// <summary>
        /// 是否拋出內部異常
        /// </summary>
        public bool IsThrowInternalError { get; set; }

        /// <summary>
        /// 發送 Line Notify
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        /// <exception cref="LineNotifyProviderException"></exception>
        public async Task<NotifyResponse> NotifyAsync(NotifyRequest request, CancellationToken cancelToken)
        {
            var url = "api/notify";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken) },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", request.Message},
                    {"stickerPackageId", request.StickerPackageId},
                    {"stickerId", request.StickerId},
                    {"imageFullsize", request.ImageLink},
                    {"imageThumbnail", request.ImageThumbnailLink},
                }),
            };

            using (var client = this.CreateApiClient())
            {
                var response = await client.SendAsync(httpRequest, cancelToken);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (this.IsThrowInternalError)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new LineNotifyProviderException(error);
                    }
                }

                var result = JsonConvert.DeserializeObject<NotifyResponse>(await response.Content.ReadAsStringAsync());
                result.Header = response.Headers;

                return result;
            }
        }

        /// <summary>
        /// 發送 Line Notify 包含圖片
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        /// <exception cref="LineNotifyProviderException"></exception>

        public async Task<NotifyResponse> NotifyAsync(NotifyWithImageRequest request,
                                               CancellationToken cancelToken)
        {
            var url = $"api/notify?message={request.Message}";
            using (var formDataContent = new MultipartFormDataContent())
            {
                var imageName = Path.GetFileName(request.FilePath);
                var imageContent = new ByteArrayContent(request.FileBytes);
                var mimeType = MimeMapping.GetMimeMapping(imageName);   // 透過檔案取得 ContentType
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

                formDataContent.Add(imageContent, "imageFile", imageName);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Headers = { Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken) },
                    Content = formDataContent
                };

                using (var client = this.CreateApiClient())
                {
                    var response = await client.SendAsync(httpRequest, cancelToken);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (this.IsThrowInternalError)
                        {
                            var error = await response.Content.ReadAsStringAsync();
                            throw new LineNotifyProviderException(error);
                        }
                    }

                    var result = JsonConvert.DeserializeObject<NotifyResponse>(await response.Content.ReadAsStringAsync());
                    result.Header = response.Headers;

                    return result;
                }
            }
        }

        /// <summary>
        /// 建立 HttpClient
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateApiClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://notify-api.line.me/"),
            };
            return client;
        }
    }
}
