
namespace Main.Models
{
	public class Reservation
{
	public DateTime AppointmentDate { get; set; }
	public DateTime AppointmentTime { get; set; }
	public string CustomerName { get; set; }
	public string Service { get; set; } // For simplicity, we're using a string. This could be an enum or a separate model.
}
}