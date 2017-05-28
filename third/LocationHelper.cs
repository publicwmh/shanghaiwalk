using System;
namespace shanghaiwalk.third
{
	public class LocationHelper
	{
		string ak;
		public LocationHelper()
		{
			//ak = ConfigurationManager.AppSettings["baiduKey"];
			
		}
		

		public BaiduLocation GetGPS(string addr)
		{
			BaiduLocation re = new BaiduLocation();
			var request = new BaiduGeocodingRequest();
			request.address = addr;

			request.ak = ak;
			var response = GeocodingService.GetBaiduResponse(request);

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

		//      public string FindName(string adr)
		//      {
		//          var r = session.QueryOver<heritagesh>().Where(p => p.oldname == adr).SingleOrDefault();
		//          if (r != null)
		//          {
		//              return r.adr;
		//          }
		//          else
		//          {
		//              return "";
		//          }
		//      }
	}
}
