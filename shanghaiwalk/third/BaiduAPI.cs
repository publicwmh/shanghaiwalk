using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace shanghaiwalk.third
{
	[Serializable]
	public class LLCovertResult
	{
		public string error { get; set; }
		public string x { get; set; }
		public string y { get; set; }
	}

	public class BaiduAPI
	{
		/// <summary>
		/// 
		/// </summary>
		static string APIurl = @"http://api.map.baidu.com/ag/coord/convert?";
		public static string Encode(string str)
		{
			byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
			return Convert.ToBase64String(encbuff);
		}
		public static string Decode(string str)
		{
			byte[] decbuff = Convert.FromBase64String(str);
			return System.Text.Encoding.UTF8.GetString(decbuff);
		}
		public static List<GeographicCoordinate> ConvertToGPS(string lat, string lon)
		{
			Dictionary<string, string> keyarg = new Dictionary<string, string>();
			keyarg.Add("from", "0");
			keyarg.Add("to", "4");
			keyarg.Add("x", lon);
			keyarg.Add("y", lat);
			keyarg.Add("mode", "1");
			Dictionary<string, string> requestParams = keyarg;
			return ReadResult(Execute(requestParams));
		}
		public static List<GeographicCoordinate> ConvertGoogleToBaidu(string lat, string lon)
		{
			Dictionary<string, string> keyarg = new Dictionary<string, string>();
			keyarg.Add("from", "2");
			keyarg.Add("to", "4");
			keyarg.Add("x", lon);
			keyarg.Add("y", lat);
			keyarg.Add("mode", "1");
			Dictionary<string, string> requestParams = keyarg;
			return ReadResult(Execute(requestParams));
		}
		private static List<GeographicCoordinate> ReadResult(IList<LLCovertResult> iList)
		{
			List<GeographicCoordinate> re = new List<GeographicCoordinate>();
			foreach (var item in iList)
			{
				if (item.error == "0")
				{
					if (string.IsNullOrEmpty(item.x))
					{
						re.Add(new GeographicCoordinate()
						{
							error = 0,
							gps_lon = 0
									,
							gps_lat = 0
						});
					}
					else
					{
						re.Add(new GeographicCoordinate()
						{
							error = 0,
							gps_lon = decimal.Parse(Decode(item.x))
									,
							gps_lat = decimal.Parse(Decode(item.y))
						});
					}
				}
				else
				{
					re.Add(new GeographicCoordinate() { error = 1 });
				}
			}
			return re;
		}

		internal static IList<LLCovertResult> Execute(IDictionary<string, string> requestParams)
		{
			IList<LLCovertResult> h;
			string requestUrl = GetRequestUrl(requestParams);
			HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
			if (req == null)
			{
				throw new Exception("Unable to create an HTTP request for URL " + requestUrl);
			}
			//代理
			JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
            using (HttpWebResponse resp = req.GetResponseAsync().Result as HttpWebResponse)
			{
				using (StreamReader respReader = new StreamReader(resp.GetResponseStream()))
				{
					string str = respReader.ReadToEnd();
					if (!string.IsNullOrEmpty(str))
					{
						try
						{
							h = JsonConvert.DeserializeObject<IList<LLCovertResult>>(str, setting);
						}
						catch (Exception ex)
						{
							h = null;
						}
					}
					else
					{
						h = null;
					}
				}
			}
			return h;
		}
		private static string GetRequestUrl(IDictionary<string, string> requestParams)
		{
			StringBuilder qsParams = new StringBuilder();
			foreach (string key in requestParams.Keys)
			{
				if (qsParams.Length > 0)
				{
					qsParams.Append("&");
				}
                qsParams.AppendFormat("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(requestParams[key]));
			}
			return string.Format("{0}{1}", APIurl, qsParams.ToString());
		}
	}
	[Serializable]
	public class GeographicCoordinate
	{
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.Latitude, this.Longitude, this.Altitude);
		}
		public double Altitude { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int confidence { get; set; }
		public int error { get; set; }
		public decimal gps_lat { get; set; }
		public decimal gps_lon { get; set; }
	}
}
