using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace shanghaiwalk.third
{
	internal static class Http
	{
		public class HttpGetResponse
		{
			private Uri requestUri;

			public HttpGetResponse(Uri uri)
			{
				requestUri = uri;
			}
			 
			
			public async Task<string> AsString()
			{
				var output = String.Empty;
				var g = WebRequest.Create(requestUri);
                var response = await g.GetResponseAsync();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					output = reader.ReadToEnd();
                    return output;
                }		 
			}

			public async Task<T> As<T>() where T : class
			{
				T output = null;
                var str = await AsString();
				using (var stringReader = new StringReader(str))
				{
					var jsonReader = new JsonTextReader(stringReader);
					var serializer = new JsonSerializer();
					//serializer.Converters.Add(new JsonEnumTypeConverter());
					output = serializer.Deserialize<T>(jsonReader);
				}
				return output;
			}
		}

		public static HttpGetResponse Get(Uri uri)
		{
			return new HttpGetResponse(uri);
		}

		 
	}
}
