using HaircutReservation.Services;
using Main.Models;
using Microsoft.Extensions.Configuration;
namespace Main.Pages;

public partial class ReservationPage : ContentPage
{
	private readonly JSONDataService _dataService = new JSONDataService();
	private readonly CosmosDataService _cosmosDataService; 
	public ReservationViewModel Reservation { get; set; }
	IConfiguration configuration;
	public ReservationPage(IConfiguration config)
	{
		InitializeComponent();
		Reservation = new ReservationViewModel();
		configuration = config;
		
		_cosmosDataService = new CosmosDataService(configuration["CosmosDb:ConnectionString"]);
		this.BindingContext = Reservation;
	}

	private async void OnBookAppointmentClicked(object sender, EventArgs e)
	{
		await _cosmosDataService.GetReservationsAsync();
		// Simple validation
		if (string.IsNullOrWhiteSpace(Reservation.CustomerName) || string.IsNullOrWhiteSpace(Reservation.SelectedService))
		{
			await DisplayAlert("Error", "Please enter your name and select a service.", "OK");
			return;
		}

		await DisplayAlert("Success", $"Appointment booked for {Reservation.CustomerName} on {Reservation.AppointmentDate:d} at {Reservation.AppointmentDate:t} for a {Reservation.SelectedService}.", "OK");

		// Here you would typically send the reservation to a backend server or local database.
		await _dataService.SaveReservationAsync(Reservation.Reservation);

		// Refresh the list
		var reservations = await _dataService.GetReservationsAsync();
		appointmentsListView.ItemsSource = reservations;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var reservations = await _dataService.GetReservationsAsync();
		appointmentsListView.ItemsSource = reservations;
	}
	private void OnAppointmentSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is Reservation selectedReservation)
		{
			// Handle the selection
			// For example, display the details of the selected reservation
			DisplayAlert("Appointment Selected", $"You selected {selectedReservation.CustomerName}'s appointment on {selectedReservation.AppointmentDate}.", "OK");

			// Optionally, deselect the item
			((ListView)sender).SelectedItem = null;
		}
	}

}
