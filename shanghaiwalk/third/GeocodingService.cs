using System;
using System.Threading.Tasks;

namespace shanghaiwalk.third
{
	public class GeocodingService
	{
		public GeocodingService()
		{
		}
		public static readonly Uri BaiduApiUrl =
			new Uri("http://api.map.baidu.com/geocoder/v2/");
		public static async Task<BaiduGeocodingResponse> GetBaiduResponseAsync(BaiduGeocodingRequest request)
		{
			var url = new Uri(BaiduApiUrl, request.ToUri());
			return await Http.Get(url).As<BaiduGeocodingResponse>();
		}
	}
}
