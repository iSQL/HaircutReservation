using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Models;
using Microsoft.Azure.Cosmos;

namespace HaircutReservation.Services
{
	public class CosmosDataService
	{
		private CosmosClient cosmosClient;
		private Microsoft.Azure.Cosmos.Container container;

		public CosmosDataService(string connectionString)
		{
			this.cosmosClient = new CosmosClient(connectionString);
			this.container = cosmosClient.GetContainer("HaircutAppDB", "Reservations");
		}
		public async Task<List<Reservation>> GetReservationsAsync()
		{
			var query = this.container.GetItemQueryIterator<Reservation>("SELECT * FROM c");
			List<Reservation> results = new List<Reservation>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				results.AddRange(response.ToList());
			}
			return results;
		}
		public async Task SaveReservationAsync(Reservation reservation)
		{
			await this.container.UpsertItemAsync(reservation);
		}
	}
}
