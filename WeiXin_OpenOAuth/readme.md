
本库为.NET4.5。

已经支持所有微信6 API，包括自定义菜单、模板信息接口、素材上传接口、群发接口、多客服接口、支付接口、微小店接口、卡券接口等。

（同时由于易信的API目前与微信保持一致，此SDK也可以直接用于易信，如需使用易信的自定义菜单，通用接口改成易信的通讯地址即可）


已经支持用户会话上下文（解决服务器无法使用Session处理用户信息的问题）。

已经全面支持微信公众号、企业号、开放平台的最新API。

目前官方的API都已完美集成，除非有特殊说明，所有升级都会尽量确保向下兼容，所以已经发布的版本请放心使用或直接升级（覆盖）最新的[ChainClouds.Weixin.MP.dll] 。

如果需要使用或修改此项目的源代码，建议先Fork。也欢迎将您修改的通用版本Pull Request过来。



项目文件夹说明
--------------
> ChainClouds.Weixin.MP.BuildOutPut：所有最新版本DLL发布文件夹

> ChainClouds.Weixin.MP.MvcExtension：ChainClouds.Weixin.MP.MvcExtension.dll源码，为MVC4.0项目提供的扩展包。

> ChainClouds.Weixin.MP.Web：前端（ASP.NET MVC 4.0）

> ChainClouds.Weixin.MP.Web.WebForms：前端（ASP.NET WebForms）

> ChainClouds.Weixin.MP：ChainClouds.Weixin.MP.dll 微信公众账号SDK源代码

> ChainClouds.Weixin.QY：ChainClouds.Weixin.QY.dll 微信企业号SDK源代码

> ChainClouds.Weixin.Open：ChainClouds.Weixin.Open.dll 第三方开放平台SDK源代码

> ChainClouds.Weixin：所有ChainClouds.Weixin.[x].dll 基础类库源代码

ChainClouds.Weixin.MP.Web中的关键代码说明（这是MVC项目，WebForms项目见Weixin.aspx）
--------------
###/Controllers/WeixinController.cs
下面的Token需要和微信公众平台后台设置的Token同步，如果经常更换建议写入Web.config等配置文件（实际使用过程中两列建议使用数字+英文大小写改写Token，Token一旦被破解，微信请求将很容易被伪造！）：
```C#
public readonly string Token = "weixin";
```
下面这个Action（Get）用于接收并返回微信后台Url的验证结果，无需改动。地址如：http://domain/Weixin或http://domain/Weixin/Index
```C#
/// <summary>
/// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.chainclouds.cn/weixin
/// </summary>
[HttpGet]
[ActionName("Index")]
public ActionResult Get(PostModel postModel, string echostr)
{
    if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content(echostr); //返回随机字符串则表示验证通过
    }
    else
    {
        return Content("failed:" + postModel.Signature + "," + MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
            "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
    }
}
```
上述方法中的PostModel是一个包括了了Signature、Timestamp、Nonce（由微信服务器通过请求时的Url参数传入），以及AppId、Token、EncodingAESKey等一系列内部敏感的信息（需要自行传入）的实体类，同时也会在后面用到。


下面这个Action（Post）用于接收来自微信服务器的Post请求（通常由用户发起），这里的if必不可少，之前的Get只提供微信后台保存Url时的验证，每次Post必须重新验证，否则很容易伪造请求。
```C#
/// <summary>
/// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
/// </summary>
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("参数错误！");
    }
    ...
}
```
###如何处理微信公众账号请求？
ChainClouds.Weixin.MP提供了2中处理请求的方式。简单举例MessageHandler的处理方法。

MessageHandler的处理流程非常简单：
``` C#
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("参数错误！");
    }
    
    postModel.Token = Token;
    postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
    postModel.AppId = AppId;//根据自己后台的设置保持一致

    var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息（第一步）
    
    messageHandler.Execute();//执行微信处理过程（第二步）
    
    return new FixWeixinBugWeixinResult(messageHandler);//返回（第三步）
}
```
整个消息除了postModel的赋值以外，接收（第一步）、处理（第二步）、返回（第三步）分别只需要一行代码。

上述代码中的CustomMessageHandler是一个自定义的类，继承自ChainClouds.Weixin.MP.MessageHandler.cs。MessageHandler是一个抽象类，包含了执行各种不同请求类型的抽象方法（如文字，语音，位置、图片等等），我们只需要在自己创建的CustomMessageHandler中逐个实现这些方法就可以了。刚建好的CustomMessageHandler.cs如下：
```C#
using System;
using System.IO;
using ChainClouds.Weixin.MP.MessageHandlers;
using ChainClouds.Weixin.MP.Entities;

namespace ChainClouds.Weixin.MP.Web.CustomerMessageHandler
{
    public class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }
        
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //...
        }

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            //...
        }
        
        //更多没有重写的OnXX方法，将默认返回DefaultResponseMessage中的结果。
        ....
    }
}
```
其中OnTextRequest、OnVoiceRequest等分别对应了接收文字、语音等不同的请求类型。

比如我们需要对文字类型请求做出回应，只需要完善OnTextRequest方法：
```C#
      public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
      {
          //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs
          var responseMessage = CreateResponseMessage<ResponseMessageText>();
          responseMessage.Content =
              string.Format(
                  "您刚才发送了文字信息：{0}",
                  requestMessage.Content);
          return responseMessage;
      }
```
这样CustomMessageHandler在执行messageHandler.Execute()的时候，如果发现请求信息的类型是文本，会自动调用以上代码，并返回代码中的responseMessage作为返回信息。responseMessage可以是IResponseMessageBase接口下的任何类型（包括文字、新闻、多媒体等格式）。

MessageHandler增加了对用户会话上下文的支持，用于解决服务器上无法使用Session管理用户会话的缺陷。
