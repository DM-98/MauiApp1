using System.Net.Http.Json;
using System.Text.Json;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	private readonly HttpClient httpClient;
	private readonly JsonSerializerOptions jsonSerializerOptions;

	public MainPage(HttpClient httpClient)
	{
		InitializeComponent();

		this.httpClient = httpClient;
		this.httpClient.DefaultRequestHeaders.Add("X-Tunnel-Authorization", "tunnel <Your Tunnel Access Token>"); // if u use tunnel

		jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		try
		{
			HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("WeatherForecast", new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = "Test"
			}, jsonSerializerOptions);

			if (responseMessage.IsSuccessStatusCode)
			{
				WeatherForecast weatherForecastResult = await responseMessage.Content.ReadFromJsonAsync<WeatherForecast>(jsonSerializerOptions);

				await DisplayAlert("Success", $"Date: {weatherForecastResult.Date}, TemperatureC: {weatherForecastResult.TemperatureC}, Summary: {weatherForecastResult.Summary}", "OK");
			}
			else
			{
				await DisplayAlert("Error", responseMessage.ReasonPhrase, "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", "Exception: " + ex.Message + " | Inner Exception: " + ex.InnerException, "OK");
		}
	}
}