using System;
namespace shanghaiwalk.weixin
{
    public class SHWKMessageHandler
    {
		public class CustomMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
		{
			public CustomMessageContext()
			{
				base.MessageContextRemoved += CustomMessageContext_MessageContextRemoved;
			}

			void CustomMessageContext_MessageContextRemoved(object sender, Senparc.Weixin.Context.WeixinContextRemovedEventArgs<IRequestMessageBase, IResponseMessageBase> e)
			{
				/* 注意，这个事件不是实时触发的（当然你也可以专门写一个线程监控）
				 * 为了提高效率，根据WeixinContext中的算法，这里的过期消息会在过期后下一条请求执行之前被清除
				 */

				var messageContext = e.MessageContext as CustomMessageContext;
				if (messageContext == null)
				{
					return;//如果是正常的调用，messageContext不会为null
				}

				//TODO:这里根据需要执行消息过期时候的逻辑，下面的代码仅供参考

				//Log.InfoFormat("{0}的消息上下文已过期",e.OpenId);
				//api.SendMessage(e.OpenId, "由于长时间未搭理客服，您的客服状态已退出！");
			}
		}

		public class SHWKMessageHandler : MessageHandler<CustomMessageContext>
		{
			public SHWKMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
				: base(inputStream, postModel, maxRecordCount)
			{

			}

			public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
			{
				var responseMessage = CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
				responseMessage.Content = "Welcome";
				return responseMessage;
			}

			public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
			{
				//根据文本查询数据库
				return base.OnTextRequest(requestMessage);
			}


			//更多没有重写的OnXX方法，将默认返回DefaultResponseMessage中的结果。

		}
    }
}
