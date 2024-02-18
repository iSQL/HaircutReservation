using HaircutReservation.Services;
using Main.Models;
using Microsoft.Extensions.Configuration;
namespace Main.Pages;

public partial class ReservationPageAPI : ContentPage
{
	private readonly APIService _dataService = new APIService();
	public ReservationViewModel Reservation { get; set; }
	public ReservationPageAPI()
	{
		InitializeComponent();
		Reservation = new ReservationViewModel();

		_dataService = new APIService();
		this.BindingContext = Reservation;
	}

	private async void OnBookReservationClicked(object sender, EventArgs e)
	{
		//await _dataService.GetReservationsAsync();
		// Simple validation
		if (string.IsNullOrWhiteSpace(Reservation.CustomerName) || string.IsNullOrWhiteSpace(Reservation.SelectedService))
		{
			return; //ignore for now
			await DisplayAlert("Error", "Please enter your name and select a service.", "OK");
			
		}

		// Here you would typically send the reservation to a backend server or local database.
		await _dataService.SaveReservationAsync(Reservation.Reservation);

		// Refresh the list
		var reservations = await _dataService.GetReservationsAsync();
		reservationsListView.ItemsSource = reservations;
		
		await DisplayAlert("Success", $"Appointment booked for {Reservation.CustomerName} on {Reservation.AppointmentDate:d} at {Reservation.AppointmentDate:t} for a {Reservation.SelectedService}.", "OK");
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var reservations = await _dataService.GetReservationsAsync();
		reservationsListView.ItemsSource = reservations;
	}
	private void OnReservationSelected(object sender, SelectedItemChangedEventArgs e)
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
