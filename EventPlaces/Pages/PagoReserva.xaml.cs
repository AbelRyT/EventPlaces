using Common;
using EventPlaces.Api;
using EventPlaces.Event_Places;
using EventPlaces.Ventanas;
using System.Text;
using System.Text.Json;

namespace EventPlaces.Pages;

public partial class PagoReserva : ContentPage
{
    private readonly HttpClient _httpClient;
    public ReservacionDto Reservacion { get; set; }
    public PagoReserva(ReservacionDto reservacionDto)
    {
        InitializeComponent();
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);


        Reservacion = reservacionDto;
        BindingContext = Reservacion;
    }

    private async void OnPagarAhoraClicked(object sender, EventArgs e)
    {
       btnPagar.IsEnabled = false;
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;

        bool confirm = await DisplayAlert("Confirmación de Pago", "¿Deseas proceder con el pago?", "Sí", "No");
        if (confirm)
        {
            var pago = new PagoDto
            {
                UsuarioId = Constante.usuarioId,
                ReservacionId = Reservacion.Id,
                FechaPago = DateTime.Now,
                Monto = Reservacion.PrecioTotal,
                MetodoPago = "TARJETA",
                Estado = "PAGADO",
                ReservacionDto = Reservacion
            };

            var json = JsonSerializer.Serialize(pago);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Routes.Api}Pagos/CreatePago", content);


            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.Navigation.PushModalAsync(new VolantePagoPage(Reservacion));
                btnPagar.IsEnabled = true;
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                await Shell.Current.GoToAsync("//Pagados");
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"{errorResponse}", "OK");
                btnPagar.IsEnabled = true;
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }
        }
        else
        {
            await DisplayAlert("Cancelado", "El pago ha sido cancelado.", "OK");
            btnPagar.IsEnabled = true;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            await Shell.Current.GoToAsync("//Pagados");
        }
    }
}