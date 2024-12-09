using Common;
using EventPlaces.Api;
using EventPlaces.Event_Places;
using System.Text;
using System.Text.Json;

namespace EventPlaces.Pages;

public partial class ReservacionPage : ContentPage
{
    public LugarDto Lugar { get; set; }
    public ReservacionDto Reservacion { get; set; }

    private readonly HttpClient _httpClient = new HttpClient();

    public ReservacionPage(LugarDto lugar)
    {
        InitializeComponent();
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
        Lugar = lugar;
        Reservacion = new ReservacionDto
        {
            LugarId = lugar.Id,
            UsuarioId = Constante.usuarioId,
            FechaInicio = DateTime.Today,
            FechaFin = DateTime.Today.AddDays(1),
            EstadoId = 1,
            EstadoNombre = "Reservada"
        };
        BindingContext = this;
    }

    private async void OnConfirmarReservaClicked(object sender, EventArgs e)
    {
        btnConfirmar.IsEnabled = false;
        if (Reservacion.FechaInicio >= Reservacion.FechaFin)
        {
            await DisplayAlert("Error", "La fecha de fin debe ser posterior a la fecha de inicio.", "OK");
            btnConfirmar.IsEnabled = true;
            return;
        }

        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;

        var json = JsonSerializer.Serialize(Reservacion);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{Routes.Api}Reservaciones/CreateReservacion", content);

        if (response.IsSuccessStatusCode)
        {
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            btnConfirmar.IsEnabled = true;

            await Navigation.PushAsync(new Reservados());

        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            btnConfirmar.IsEnabled = true;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            await DisplayAlert("Error", $"Error al registrar en la API: {errorResponse}", "OK");
        }


    }
}