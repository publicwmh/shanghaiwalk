using System;
namespace shanghaiwalk.third
{
	public class GeocodingService
	{
		public GeocodingService()
		{
		}
		public static readonly Uri BaiduApiUrl =
			new Uri("http://api.map.baidu.com/geocoder/v2/");
		public static BaiduGeocodingResponse GetBaiduResponse(BaiduGeocodingRequest request)
		{
			var url = new Uri(BaiduApiUrl, request.ToUri());
			return Http.Get(url).As<BaiduGeocodingResponse>();
		}
	}
}
