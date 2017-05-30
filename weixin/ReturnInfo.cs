using System;
using Senparc.Weixin.MP.Entities;
using shanghaiwalk.Baiye;

namespace shanghaiwalk.weixin
{
	public class ReturnInfo
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
			Article item2 = new Article
			{
				Description = "系统更新日志",
				Title = "系统更新日志",
				PicUrl = this.dir + "logo.png",
				Url = this.dir + "WxSH/ReadMe/"
			};
			arg_70_0.Articles.Add(new Article
			{
				Description = text,
				PicUrl = this.dir + "logo.png",
				Title = text + "(->时光)",
				Url = item.TmpPicUrl
			});
			//this.user.SaveUserEner(requestMessage.FromUserName, item.KeyWordId);
			arg_70_0.Articles.Add(item2);
			return arg_70_0;
		}
	}
}
