using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

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
			 
			
			public string AsString()
			{
				var output = String.Empty;
				var g = WebRequest.Create(requestUri);

                var response = g.GetResponseAsync().Result;

				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					output = reader.ReadToEnd();

				}
				 

				return output;
			}

			public T As<T>() where T : class
			{
				T output = null;

				using (var stringReader = new StringReader(AsString()))
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
