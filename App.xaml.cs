using Main.Pages;

namespace Main
{
	public partial class App : Application
	{
		public App(ReservationPageAPI page)
		{
			InitializeComponent();
			MainPage = page;
		}
	}
}
