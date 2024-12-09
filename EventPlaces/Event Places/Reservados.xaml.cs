using Common;
using EventPlaces.Api;
using EventPlaces.Pages;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace EventPlaces.Event_Places
{
    public partial class Reservados : ContentPage
    {
        public ObservableCollection<ReservacionDto> Reservaciones { get; set; } = new ObservableCollection<ReservacionDto>();

        private readonly HttpClient _httpClient;

        public Reservados()
        {
            InitializeComponent();

            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);

            BindingContext = this;
            _ = LoadReservacionesAsync();
        }

        private async Task LoadReservacionesAsync()
        {
            try
            {
                string apiUrl = $"{Routes.Api}Reservaciones/GetReservaciones";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var ReservacionesList = JsonConvert.DeserializeObject<List<ReservacionDto>>(json);

                    Reservaciones.Clear();
                    foreach (var reservacion in ReservacionesList)
                    {
                        Reservaciones.Add(reservacion);
                    }
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo cargar la lista de Reservaciones.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarReservaPage());
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagoReserva(Reservaciones.First()));
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }
    }

}
