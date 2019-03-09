using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Lab3
{
	class Program
	{
		static string GetMethod(HttpClient client)
		{
			return client
				.GetStringAsync("/image/svg")
				.Result;
		}

		static string PostMethod(HttpClient client)
		{
			var parameters = new Dictionary<string, string> { { "a", "b" }, { "1", "2" } };

			return client
				.PostAsync("/post", new FormUrlEncodedContent(parameters))
				.Result
				.Content
				.ReadAsStringAsync()
				.Result;
		}

		static string DeleteMethod(HttpClient client)
		{
			return client
				.DeleteAsync("/delete")
				.Result
				.Content
				.ReadAsStringAsync()
				.Result;
		}

		static string PutMethod(HttpClient client)
		{
			var parameters = new Dictionary<string, string> { { "sos", "ati" }, { "mas", "ati" } };

			return client.PutAsync("/put", new FormUrlEncodedContent(parameters))
				.Result
				.Content
				.ReadAsStringAsync()
				.Result;
		}

		static string PatchMethod(HttpClient client)
		{
			var request = new HttpRequestMessage(new HttpMethod("PATCH"), client.BaseAddress + "/patch");

			try
			{
				return client
					.SendAsync(request)
					.Result
					.Content
					.ReadAsStringAsync()
					.Result;
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		static void Main(string[] args)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://httpbin.org");
				Console.WriteLine($"GET (img : svg):\n{GetMethod(client)}");
				Console.WriteLine($"POST:\n{PostMethod(client)}");
				Console.WriteLine($"DELETE:\n{DeleteMethod(client)}");
				Console.WriteLine($"PUT:\n{PutMethod(client)}");
				Console.WriteLine($"PATCH:\n{PatchMethod(client)}");
			}

			Console.ReadKey();
		}
	}
}
