using Microsoft.Extensions.Logging;
using DevExpress.Maui;
using Microsoft.Extensions.Configuration;
using HaircutReservation.Services;
using Main.Pages;

namespace Main
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			

			var cosmosConnectionString = builder.Configuration["CosmosDb:ConnectionString"];
			builder.Services.AddSingleton<CosmosDataService>(s => new CosmosDataService(cosmosConnectionString));
			builder.Services.AddTransient<ReservationPage>();


			builder.UseDevExpress();

			return builder.Build();
		}
	}
}
