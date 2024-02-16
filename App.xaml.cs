using Main.Pages;

namespace Main
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new ReservationPage());
		}
	}
}
