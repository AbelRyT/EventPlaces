using Common;
using EventPlaces.Api;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventPlaces.Event_Places
{
    public partial class MenuPrincipal : ContentPage
    {
        public ObservableCollection<LugarDto> Lugares { get; set; } = new ObservableCollection<LugarDto>();

        private readonly HttpClient _httpClient;

        public MenuPrincipal()
        {
            InitializeComponent();

            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);

            BindingContext = this;
            _ = LoadLugaresAsync();
        }

        private async Task LoadLugaresAsync()
        {
            try
            {
                string apiUrl = $"{Routes.Api}Lugares/GetLugares"; 
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var lugaresList = JsonConvert.DeserializeObject<List<LugarDto>>(json);

                    Lugares.Clear();
                    foreach (var lugar in lugaresList)
                    {
                        Lugares.Add(lugar);
                    }
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo cargar la lista de lugares.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var lugar = frame.BindingContext as LugarDto;
            if (lugar != null)
            {
                // Navegar a la página de detalles
                Navigation.PushAsync(new DescripcionReserva(lugar));
            }
        }
    }
}
