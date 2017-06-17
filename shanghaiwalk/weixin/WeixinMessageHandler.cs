using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using shanghaiwalk.Baiye;
using shanghaiwalk.option;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace shanghaiwalk.weixin
{
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    public class WeixinMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        private readonly ILogger _logger;

        public WeixinMessageHandler(XDocument input,PostModel post,
                                    OssOption ossoption,
                                    BaiduApiOption baiduapiOption,
                                    BaiYeContext baiyecontext,ILogger logger):base(input,post)
        {
            _logger = logger;
            POIService = new POIService(baiyecontext, _logger);
            service = new BaiYeMapService(ossoption,baiduapiOption,baiyecontext, _logger, POIService);
            
            
        }

        private BaiYeMapService service;
        private POIService POIService;
        private static ReturnMessageBuilder build = new ReturnMessageBuilder();

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            ResponseMessageText expr_06 = base.CreateResponseMessage<ResponseMessageText>();
            expr_06.Content = "Welcome";
            return expr_06;
        }


        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            ResponseMessageText responseMessageText = base.CreateResponseMessage<ResponseMessageText>();
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                
                string content = requestMessage.Content;
                _logger.LogInformation($"微信用户输入:{content}");
                if (!content.Contains("x"))
                {
                    if (content == "help" || content == "?")
                    {
                        stringBuilder.Append("欢迎使用，我们正在构建功能更完善的公众号。你可以直接输入地名或地名，系统将会直接返回百业地图，\n                        建议输入关键词为详细的地名，包含门牌号或者专有地名等，会提高查询的成功率。 欢迎联系本人提供老照片，老地图，老建筑资料。");
                    }
                    else
                    {
                        if (content == "w" || content == "s" || content == "e" || content == "n")
                        {
                            IResponseMessageBase result = this.GetNext(content, UserInfoContext.Get(requestMessage.FromUserName), requestMessage);
                            return result;
                        }
                        bool usehpic = false;
                        BaiYeMapItem mapInfo = this.service.GetMapInfo(content, usehpic).Result;
                        if (mapInfo != null )
                        {
                            ResponseMessageNews mapItemShow = build.GetMapItemShow(mapInfo, requestMessage);
                            if (mapItemShow != null)
                            {
                                //UserInfoContext.Set(requestMessage.FromUserName, mapInfo);
                                IResponseMessageBase result = mapItemShow;
                                return result;
                            }
                            stringBuilder.Append("未找到地图，望见谅。建议输入关键词为详细的地名，包含门牌号或者专有地名等，会提高查询的成功率");
                        }
                        else
                        {
                            stringBuilder.Append("未找到地图，系统正在内测中，望见谅。");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stringBuilder.Append("很抱歉，系统出错。" + ex.Message);
                _logger.LogError(ex, "异常");
            }
            stringBuilder.AppendLine("\r\n#");
            responseMessageText.Content = stringBuilder.ToString();
            return responseMessageText;
        }

        private IResponseMessageBase GetNext(string str, BaiYeMapItem baiYeMapItem, RequestMessageText requestMessage)
        {
            return null;
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            ResponseMessageText expr_06 = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            expr_06.Content = "欢迎关注漫游上海公众平台。或许每一间旧商铺，旧名居，旧街道还在吧，城市的肌理她一直在那里，让我们去漫游上海吧！ 用户可以直接输入地址或专有地名进行搜索，鉴于平台数据仍在完善未能搜索到结果情况请见谅";
            return expr_06;
        }

        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
             BaiYeMapItem mapInfo = this.service.GetMapInfoByGPS("来自用户提交的位置", Convert.ToSingle(requestMessage.Location_Y.ToString()), Convert.ToSingle(requestMessage.Location_X.ToString()), false);
            StringBuilder stringBuilder = new StringBuilder();
            if (mapInfo != null)
            {
                return build.GetMapItemShow(mapInfo, requestMessage);
            }
            stringBuilder.Append("未找到地图，望见谅。建议输入关键词为详细的地名，包含门牌号或者专有地名等，会提高查询的成功率");
            ResponseMessageText expr_6D = base.CreateResponseMessage<ResponseMessageText>();
            expr_6D.Content = stringBuilder.ToString();
            return expr_6D;
        }
    }
}
