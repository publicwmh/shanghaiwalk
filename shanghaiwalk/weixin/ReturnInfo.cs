using System;
using Senparc.Weixin.MP.Entities;
using shanghaiwalk.Baiye;

namespace shanghaiwalk.weixin
{
	public class ReturnMessageBuilder
	{
		private string dir = "";

		//private UseSeekService user = new UseSeekService();

		public ResponseMessageNews GetMapItemShow(BaiYeMapItem item, IRequestMessageBase requestMessage)
		{
			ResponseMessageNews arg_70_0 = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
			string text;
			if (requestMessage is RequestMessageText)
			{
				text = ((RequestMessageText)requestMessage).Content;
			}
			else
			{
				text = "定位结果";
			}
			arg_70_0.Articles.Add(new Article
			{
				Description = text,
				PicUrl = "http://wkshanghai.oss-cn-hangzhou.aliyuncs.com/logo.png",
				Title = text + "(->时光)",
				Url = item.TmpPicUrl
			});
            //this.user.SaveUserEner(requestMessage.FromUserName, item.KeyWordId);
            
            if (item.POIKey != 0) {

                var item2 = new Article
                {
                    Description = text,
                    PicUrl = "http://wkshanghai.oss-cn-hangzhou.aliyuncs.com/logo.png",
                    Title = text + "介绍",
                    Url = "http://wk.kwok.io/poi/info/"+item.POIKey.ToString()
                };
                arg_70_0.Articles.Add(item2);
            }
            return arg_70_0;
		}
	}
}
