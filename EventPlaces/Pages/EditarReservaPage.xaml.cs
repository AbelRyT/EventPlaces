using Common;
using EventPlaces.Api;
using EventPlaces.Event_Places;
using System.Text;
using System.Text.Json;

namespace EventPlaces.Pages;

public partial class EditarReservaPage : ContentPage
{
    private ReservacionDto ReservacionDto;
    private readonly HttpClient _httpClient;
	public EditarReservaPage(ReservacionDto reservacionDto)
	{
		InitializeComponent();
        ReservacionDto = reservacionDto;
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
        BindingContext = ReservacionDto;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        btnGuardar.IsEnabled = false;
        btnCancelar.IsEnabled = false;
        // Validar las entradas
        if (ReservacionDto.FechaInicio.Date > ReservacionDto.FechaFin.Date)
        {
            await DisplayAlert("Error", "La fecha de inicio no puede ser posterior a la fecha de fin", "OK");
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            return;
        }

       
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;

        var json = JsonSerializer.Serialize(ReservacionDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{Routes.Api}Reservaciones/UpdateReservacion", content);

        if (response.IsSuccessStatusCode)
        {
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            await Navigation.PushAsync(new Reservados());

        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            await DisplayAlert("Error", $"Error: {errorResponse}", "OK");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        // Lógica para cancelar la edición
        bool confirm = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres cancelar la reserva?", "Sí", "No");
        if (confirm)
        {
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            var cancelar = ReservacionDto;
            cancelar.EstadoId = 3;
            var json = JsonSerializer.Serialize(cancelar);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{Routes.Api}Reservaciones/CancelarReservacion", content);

            if (response.IsSuccessStatusCode)
            {
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                btnGuardar.IsEnabled = true;
                btnCancelar.IsEnabled = true;

                await Navigation.PushAsync(new Reservados());
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                btnGuardar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                await DisplayAlert("Error", $"Error: {errorResponse}", "OK");
            }
        }
    }
}