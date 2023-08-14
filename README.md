# LineNotify

## API
* [POST]
* https://notify-api.line.me/api/notify
* �o�e�T���q�����ϥΪ̩ΨϥΪ̸s�աC��A�Ⱦ�X�ɻݭn�z�LLINE�q���Τ�ɡA�z�L�X���v��(Access Token)�ǰe�ήɰT���C

## ����
* 1000/hr �@�p�ɥi�H�e1000�����
* 50/hr �@�p�ɥi�H�e50�i�Ϥ�

## �Ѽ�
![](https://i.imgur.com/fAxJ7PG.png)

## �B�J
1. ���o�@��**�v��** `Bearer Token`
![](https://i.imgur.com/xOCNZPw.png)
![](https://i.imgur.com/PQH3Agl.png)
![](https://hackmd.io/_uploads/SySoVNzhh.png)

2. �ϥ� Postman ��API
![](https://i.imgur.com/KsSzL8O.png)

## Postman �I�s Notify API ����
1. �ƻs Sample �� curl �R�O
```clike
$ curl -X POST -H 'Authorization: Bearer <access_token>' -F 'message=foobar' \
https://notify-api.line.me/api/notify
{"status":200,"message":"ok"}
```
2. �N API �פJ�� Postman
    - �ϥ� `Import` �\��(�Ϋ� `Ctrl` + `O`)
    ![](https://hackmd.io/_uploads/HJUACzPhh.png)
    - �K�J `curl` �{���X���q�A�åh�� `$` �Ÿ�
    ![](https://hackmd.io/_uploads/S1Ot17v3n.png)
3. �N AccessToken �令�A���X���v�� (2�ذ��k)
    1. �����ק� Headers ���Y���]�w
    ![](https://hackmd.io/_uploads/HknGlXv2n.png)
    2. �]�w Authorization, �çR�� Header (Authorization Bearer)
    ![](https://hackmd.io/_uploads/r1gYGXP2n.png)
4. (Options) ����T�w���Ҷq�A�ӷP�ʸ�ƫ�ĳ�i�H�]�w���Ѽ�(Parameter)
    - �ϥ� `{{Param}}` ���覡�A�N�ѼƱa�J�ШD
    - �аѦ�: [Using variables - Postman](https://learning.postman.com/docs/sending-requests/variables/)
    - �d�ҡG
        1. �]�w Collections �� Parameters
        ![](https://hackmd.io/_uploads/rJnWXSP32.png)
        2. �ϥ� Parameter �I�s API
        ![](https://hackmd.io/_uploads/BkTWVXwhn.png)
5. �o�X�ШD
![](https://hackmd.io/_uploads/r12jDNDn3.png)
![](https://hackmd.io/_uploads/ryL5t4P2h.png)

## �ϥ� C# �I�s Notify API ����
���B�ɥΨðѦҤF�E�p���j�j���M��[�c�A�аѦ� [Ref2](#�Ѧ�)

�D�n�[�c�p�U�G
![](https://hackmd.io/_uploads/B1Sd-rP32.png)

- **LineNotifyProvider** - ���� LineNotify ���A��
- **LineNotifyProviderException** - LINE Notify �����`��
- **NotifyRequest** - LINE Notify ���ШD (��r\�K��\�����Ϥ� �T��)
- **NotifyResponse** - LINE Notify ���ШD�^��
- **NotifyWithImageRequest** - LINE Notify ���ШD (�W�ǹϤ��ɮ�)

## �ϥΤ�k
### �O ��r/�K��/�����Ϥ� �T��
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

### �O �Ϥ��T��
```csharp
var resuqst = new NotifyWithImageRequest
{
    AccessToken = token,
    Message = message,
    FilePath = image
};

var response = await provider.NotifyAsync(resuqst, CancellationToken.None);
```

## �Ѧ�
1. [LINE Notify API Document - LINE Notify](https://notify-bot.line.me/doc/en/)
2. [�q�L LINE Notify �o�e�T�� - �E�p��@�j������](https://dotblogs.com.tw/yc421206/2021/07/14/via_line_notify_send_message_stiker_and_image)
3. [��@ Line Notify �q���A�� (1) - POY CHANG](https://blog.poychang.net/line-notify-1-basic/)
4. [��@ Line Notify �q���A�� (2) �f�t ASP.NET Web API - POY CHANG](https://blog.poychang.net/line-notify-2-use-web-api/)