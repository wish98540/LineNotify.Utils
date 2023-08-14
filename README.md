# LineNotify

## API
* [POST]
* https://notify-api.line.me/api/notify
* 發送訊息通知給使用者或使用者群組。當服務整合時需要透過LINE通知用戶時，透過訪問權杖(Access Token)傳送及時訊息。

## 限制
* 1000/hr 一小時可以送1000筆資料
* 50/hr 一小時可以送50張圖片

## 參數
![](https://i.imgur.com/fAxJ7PG.png)

## 步驟
1. 取得一組**權杖** `Bearer Token`
![](https://i.imgur.com/xOCNZPw.png)
![](https://i.imgur.com/PQH3Agl.png)
![](https://hackmd.io/_uploads/SySoVNzhh.png)

2. 使用 Postman 打API
![](https://i.imgur.com/KsSzL8O.png)

## Postman 呼叫 Notify API 介紹
1. 複製 Sample 的 curl 命令
```clike
$ curl -X POST -H 'Authorization: Bearer <access_token>' -F 'message=foobar' \
https://notify-api.line.me/api/notify
{"status":200,"message":"ok"}
```
2. 將 API 匯入到 Postman
    - 使用 `Import` 功能(或按 `Ctrl` + `O`)
    ![](https://hackmd.io/_uploads/HJUACzPhh.png)
    - 貼入 `curl` 程式碼片段，並去除 `$` 符號
    ![](https://hackmd.io/_uploads/S1Ot17v3n.png)
3. 將 AccessToken 改成你的訪問權杖 (2種做法)
    1. 直接修改 Headers 裡頭的設定
    ![](https://hackmd.io/_uploads/HknGlXv2n.png)
    2. 設定 Authorization, 並刪除 Header (Authorization Bearer)
    ![](https://hackmd.io/_uploads/r1gYGXP2n.png)
4. (Options) 基於資訊安全考量，敏感性資料建議可以設定為參數(Parameter)
    - 使用 `{{Param}}` 的方式，將參數帶入請求
    - 請參考: [Using variables - Postman](https://learning.postman.com/docs/sending-requests/variables/)
    - 範例：
        1. 設定 Collections 的 Parameters
        ![](https://hackmd.io/_uploads/rJnWXSP32.png)
        2. 使用 Parameter 呼叫 API
        ![](https://hackmd.io/_uploads/BkTWVXwhn.png)
5. 發出請求
![](https://hackmd.io/_uploads/r12jDNDn3.png)
![](https://hackmd.io/_uploads/ryL5t4P2h.png)

## 使用 C# 呼叫 Notify API 介紹
此處借用並參考了余小章大大的套件架構，請參考 [Ref2](#參考)

主要架構如下：
![](https://hackmd.io/_uploads/B1Sd-rP32.png)

- **LineNotifyProvider** - 提供 LineNotify 的服務
- **LineNotifyProviderException** - LINE Notify 的異常類
- **NotifyRequest** - LINE Notify 的請求 (文字\貼圖\網路圖片 訊息)
- **NotifyResponse** - LINE Notify 的請求回應
- **NotifyWithImageRequest** - LINE Notify 的請求 (上傳圖片檔案)

## 使用方法
### 是 文字/貼圖/網路圖片 訊息
```csharp
var resuqst = new NotifyRequest
{
    AccessToken = token,
    Message = message,
    StickerPackageId = stickerPackageId,
    StickerId = stickerId,
    ImageThumbnailLink = imageLink,
    ImageLink = imageLink,
};

var response = await provider.NotifyAsync(resuqst, CancellationToken.None);
```

### 是 圖片訊息
```csharp
var resuqst = new NotifyWithImageRequest
{
    AccessToken = token,
    Message = message,
    FilePath = image
};

var response = await provider.NotifyAsync(resuqst, CancellationToken.None);
```

## 參考
1. [LINE Notify API Document - LINE Notify](https://notify-bot.line.me/doc/en/)
2. [通過 LINE Notify 發送訊息 - 余小章@大內殿堂](https://dotblogs.com.tw/yc421206/2021/07/14/via_line_notify_send_message_stiker_and_image)
3. [實作 Line Notify 通知服務 (1) - POY CHANG](https://blog.poychang.net/line-notify-1-basic/)
4. [實作 Line Notify 通知服務 (2) 搭配 ASP.NET Web API - POY CHANG](https://blog.poychang.net/line-notify-2-use-web-api/)