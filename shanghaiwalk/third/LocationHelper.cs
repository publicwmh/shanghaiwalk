using System;
using shanghaiwalk.option;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

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
            var response = await GeocodingService.GetBaiduResponseAsync(request);
            if (response.result != null)
            {
                var result=Convert2GPS(response.result.location.lat, response.result.location.lng);
                re.lat = result[0];
                re.lng = result[1];
                return re;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// baidu地址转成GPS地址
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns> 返回值 [0] lat [1] lng</returns>
        public  IList<double> Convert2GPS(double lat,double lng)
        {
            IList<double> re = new List<double>();
            var relist = BaiduAPI.ConvertToGPS(lat.ToString(), lng.ToString());
            re.Add( 2 * lat - (float)relist[0].gps_lat);
            re.Add( 2 * lng - (float)relist[0].gps_lon);
            return re;
        }
    }
}
