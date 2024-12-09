using Common;
using EventPlaces.Api;
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
                Estado = "PAGADO"
            };

            var json = JsonSerializer.Serialize(pago);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Routes.Api}Pagos/CreatePago", content);


            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.Navigation.PushModalAsync(new VolantePagoPage(Reservacion));
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"Error al registrar en la API: {errorResponse}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Cancelado", "El pago ha sido cancelado.", "OK");
            await Navigation.PushAsync(new PagoReserva(Reservacion));
        }
    }
}