using System;
using shanghaiwalk.option;
using System.Threading.Tasks;

namespace shanghaiwalk.third
{
	public class LocationHelper
	{
		string ak;
		public LocationHelper(BaiduApiOption baiduapioption)
		{
            ak = baiduapioption.Tk;
		}
		

		public async Task<BaiduLocation> GeoLoc2GPS(string addr)
		{
			BaiduLocation re = new BaiduLocation();
			var request = new BaiduGeocodingRequest();
            request.address = addr;
			request.ak = ak;
            request.city = "上海市";
			var response =await GeocodingService.GetBaiduResponseAsync(request);
			if (response.result != null)
			{
				var relist = BaiduAPI.ConvertToGPS(response.result.location.lat.ToString(), response.result.location.lng.ToString());
				re.lat = 2 * response.result.location.lat - (float)relist[0].gps_lat;
				re.lng = 2 * response.result.location.lng - (float)relist[0].gps_lon;
				return re;
			}
			else
			{
				return null;
			}
		}

	}
}
