using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HaircutReservation.Services
{
	public class APIService
	{
		private readonly HttpClient httpClient;
		private readonly string baseUri = "https://localhost:7101/api"; 
		private readonly string reservationsEndpoint = "/Reservations";
		private const string ApiKey = "HelloKey123";

		public APIService()
		{
			this.httpClient = new HttpClient();
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(baseUri)
			};

			// Add httpClient API Key to every request
			httpClient.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
		}

		public async Task<List<Reservation>> GetReservationsAsync()
		{
			try
			{
				var response = await httpClient.GetFromJsonAsync<List<Reservation>>($"{baseUri}{reservationsEndpoint}");
				return response ?? new List<Reservation>();
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine(ex.Message);
				if (ex.StatusCode.HasValue)
				{
					Console.WriteLine($"HTTP Status Code: {ex.StatusCode.Value}");
				}
				throw;
			}
		}

		public async Task SaveReservationAsync(Reservation reservation)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync($"{baseUri}{reservationsEndpoint}", reservation);
				response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine(ex.Message);
				if (ex.StatusCode.HasValue)
				{
					Console.WriteLine($"HTTP Status Code: {ex.StatusCode.Value}");
				}
				throw; 
			}
		}
	}
}
