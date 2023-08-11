using Microsoft.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();

		builder.UseMauiApp<App>();
		builder.ConfigureFonts(fonts =>
		{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton(httpClient => new HttpClient { BaseAddress = new Uri("https://<redacted>.euw.devtunnels.ms/") }); // Your API

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}