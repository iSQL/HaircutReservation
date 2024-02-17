using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
	public class ReservationViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<string> Services { get; set; }
		private string _selectedService;
		public Reservation Reservation { get; set; }
		
		public string CustomerName
		{
			get { return Reservation.CustomerName; }
			set { Reservation.CustomerName = value; }
		}

		public DateTime AppointmentDate
		{
			get { return Reservation.AppointmentDate; }
			set { Reservation.AppointmentDate = value; }
		}
		public string Service
		{
			get { return Reservation.ServiceType; }
			set { Reservation.ServiceType = value; }
		}

		public string SelectedService
		{
			get => _selectedService;
			set
			{
				if (_selectedService != value)
				{
					_selectedService = value;
					OnPropertyChanged(nameof(SelectedService));
					Reservation.ServiceType = value;
				}
			}
		}

		public ReservationViewModel() {
			Services = new ObservableCollection<string>
				{
					"Basic Haircut",
					"Deluxe Haircut",
					"Beard Trim"
					// Add more services as needed
				};
			Reservation = new Reservation();
		}
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
