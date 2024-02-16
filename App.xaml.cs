using Main.Pages;

namespace Main
{
	public partial class App : Application
	{
		public App(ReservationPage page)
		{
			InitializeComponent();
			MainPage = page;
		}
	}
}
