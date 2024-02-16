using Main.Models;
namespace Main.Pages;

public partial class ReservationPage : ContentPage
{
	private readonly DataService _dataService = new DataService();
	public ReservationViewModel Reservation { get; set; }

	public ReservationPage()
	{
		InitializeComponent();
		Reservation = new ReservationViewModel();
		this.BindingContext = Reservation;
	}

	private async void OnBookAppointmentClicked(object sender, EventArgs e)
	{
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
