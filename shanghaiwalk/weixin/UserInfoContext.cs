using System;
using System.Collections.Generic;
using shanghaiwalk.Baiye;

namespace shanghaiwalk.weixin
{
	public class UserInfoContext
	{
		public static IDictionary<string, BaiYeMapItem> CurUserMap = new Dictionary<string, BaiYeMapItem>();

		public static void Set(string user, BaiYeMapItem map)
		{
			if (UserInfoContext.CurUserMap.ContainsKey(user))
			{
				UserInfoContext.CurUserMap[user] = map;
				return;
			}
			UserInfoContext.CurUserMap.Add(user, map);
		}

		public static BaiYeMapItem Get(string user)
		{
			if (UserInfoContext.CurUserMap.ContainsKey(user))
			{
				return UserInfoContext.CurUserMap[user];
			}
			return null;
		}
	}
}
