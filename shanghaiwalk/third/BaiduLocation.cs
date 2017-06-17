using System;
using System.Linq;
using Newtonsoft.Json;

namespace shanghaiwalk.third
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaiduGeocodingResponse
    {
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("result")]
        public Baiduresult result { get; set; }

    }

    public class Baiduresult
    {
        [JsonProperty("precise")]
        public int precise { get; set; }
        [JsonProperty("location")]
        public BaiduLocation location { get; set; }
        [JsonProperty("confidence")]
        public int confidence { get; set; }
        [JsonProperty("level")]
        public string level { get; set; }

        [JsonProperty("formatted_address")]
        public string formatted_address { get; set; }
        [JsonProperty("business")]
        public string business { get; set; }
        //      [JsonProperty("addressComponent")]
        //      public BaiduAddressComponent addressComponent { get; set; }
        //      [JsonProperty("pois")]
        //      public BaiduPois[] pois { get; set; }
    }
    public class BaiduLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class BaiduGeocodingRequest
    {
        public BaiduGeocodingRequest()
        {
            output = "json";
            coordtype = "bd09ll";
        }
        /// <summary>
        /// The address that you want to geocode.
        /// </summary>
        /// <remarks>Required if latlng not present.</remarks>
        public string address { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 输出参数json 或xml
        /// 
        /// </summary>
        public string output { get; set; }

        /// <summary>
        /// 坐标的类型，目前支持的坐标类型包括：bd09ll（百度墨卡托坐标）、gcj02ll（国测局墨卡托坐标）、wgs84ll（ GPS经纬度）
        /// </summary>
        public string coordtype { get; set; }

        /// <summary>
        /// lat<纬度>,lng<经度>
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 是否显示poi信息
        /// </summary>
        public string pois { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string ak { get; set; }

        /// <summary>
        /// 回调
        /// </summary>
        public string callback { get; set; }
        internal Uri ToUri()
        {

            return new Uri($"?output={output}&ak={ak}&callback={callback}" +
                           $"&address={System.Net.WebUtility.UrlEncode(address)}" +
                           $"&city={System.Net.WebUtility.UrlEncode(city)}" +
                           $"&coordtype={coordtype}&location={location}&pois={pois}", UriKind.Relative);
        }
    }
}
