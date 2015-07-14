using System;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Json;

namespace trade
{
	
	public class Api
	{
		private bool loop = false;
		
		public static Api getInstanse()
		{
			return new Api ();
		}

		public async Task<string> get(ApiRequest requestType, Dictionary<string, string> data, int timeOut){
			var httpClient = new HttpClient(){
				Timeout = TimeSpan.FromMilliseconds(3000)


			};
			string url;
			switch(requestType){
			case ApiRequest.FIND_MOVIE:
				url = "http://www.omdbapi.com/";
				break;
			case ApiRequest.MOVIE_DATA:
				url = "http://www.omdbapi.com/";
				break;
			default:
				url = "";
				break;
			}

			try{
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url + buildParams(data)));
				request.ContentType = "application/json";
				request.Method = "GET";

				// Send the request to the server and wait for the response:
				using (WebResponse response = await request.GetResponseAsync ())
				{
					// Get a stream representation of the HTTP web response:
					using (Stream stream = response.GetResponseStream ())
					{
						// Use this stream to build a JSON document object:
						JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
						Console.Out.WriteLine("Response: {0}", jsonDoc.ToString ());
						// Return the JSON document:
						return jsonDoc.ToString();
					}
				}
			}catch(HttpRequestException e){
				//oops		
				return null;
			}finally{				
				Thread.Sleep (timeOut);
			}
		}

		public async Task<string> get(ApiRequest requestType, Dictionary<string, string> data){
			return await get (requestType, data, 0);
		}

		public async void loopRequest(ApiRequest requestType, Dictionary<string, string> data, int timeout){
			loop = true;
			while (loop) {
				await get(requestType, data, timeout);
			}
		}

		public void stopLoop(){
			loop = false;
		}

		public string buildParams (Dictionary<string, string> data){
			if (data != null && data.Count > 0) {
				String s= "?";
				foreach (string item in data.Keys) {
					s += item  
						+ "=" + data[item] + "&"; 
				}
				s.Substring (0, s.Length - 2);
				return s;
			} else {
				return "";
			}	
		}


	}


}
	
