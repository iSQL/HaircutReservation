using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Main.Models;

public class DataService
{
	private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appointments.json");

	public async Task<List<Reservation>> GetReservationsAsync()
	{
		if (!File.Exists(_filePath))
		{
			return new List<Reservation>();
		}

		using (var stream = File.OpenRead(_filePath))
		{
			return await JsonSerializer.DeserializeAsync<List<Reservation>>(stream) ?? new List<Reservation>();
		}
	}

	public async Task SaveReservationAsync(Reservation reservation)
	{
		var reservations = await GetReservationsAsync();
		reservations.Add(reservation);
		using (var stream = File.OpenWrite(_filePath))
		{
			await JsonSerializer.SerializeAsync(stream, reservations);
		}
	}
}
