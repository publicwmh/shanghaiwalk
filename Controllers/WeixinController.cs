using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using shanghaiwalk.Baiye;
using shanghaiwalk.option;
using shanghaiwalk.weixin;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shanghaiwalk.Controllers
{
    public class WeixinController : Controller
    {
        private readonly WeixinOption weixinoption;
        private readonly OssOption ossOption;
        private readonly BaiduApiOption baiduapiOption;
        private readonly BaiYeContext _baiyecontext;
        public WeixinController(IOptions<WeixinOption> optweixin,
                                IOptions<OssOption> optoss,
                                IOptions<BaiduApiOption> optbaidu,
                               BaiYeContext baiyecontext)
        {
            weixinoption = optweixin.Value;
            ossOption = optoss.Value;
            baiduapiOption = optbaidu.Value;
            _baiyecontext = baiyecontext;
        }
		[HttpGet]
		[ActionName("Test")]
        public BaiYeMapItem Test(string loc){
            BaiYeMapService service = new BaiYeMapService(ossOption,baiduapiOption,_baiyecontext);
            return service.GetMapInfo("绍兴路", false);
        }
       
		/// <summary>
		/// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
		/// </summary>
		[HttpGet]
		[ActionName("Index")]
		public ActionResult Get(PostModel postModel, string echostr)
		{
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, weixinoption.Token))
			{
				return Content(echostr); //返回随机字符串则表示验证通过
			}
			else
			{
				return Content("failed:" + postModel.Signature + ","
					+ Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, weixinoption.Token) + "。" +
					"如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
			}
		}

		/// <summary>
		/// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
		/// </summary>
		[HttpPost]
		[ActionName("Index")]
		public ActionResult Post(PostModel postModel)
		{
			if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, weixinoption.Token))
			{
				return Content("参数错误！");
			}


			postModel.Token = weixinoption.Token;
            postModel.EncodingAESKey = weixinoption.WeixinAESKey;//根据自己后台的设置保持一致
            postModel.AppId = weixinoption.AppId;//根据自己后台的设置保持一致

            var messageHandler = new WeixinMessageHandler(this.Request.Body,postModel,ossOption,baiduapiOption,_baiyecontext);//接收消息（第一步）

            messageHandler.Execute();//执行微信处理过程（第二步）
           
			return new FixWeixinBugWeixinResult(messageHandler);//返回（第三步）

		}
    }
}
