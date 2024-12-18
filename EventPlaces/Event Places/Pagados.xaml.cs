using Common;
using EventPlaces.Api;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace EventPlaces.Event_Places;

public partial class Pagados : ContentPage
{
    public ObservableCollection<PagoDto> Pagos { get; set; } = new ObservableCollection<PagoDto>();
    private readonly HttpClient _httpClient;

    public Pagados()
	{
		InitializeComponent();
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);

        BindingContext = this;
        _ = LoadPagosAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Recargar los datos cada vez que se muestra la página
        _ = LoadPagosAsync();
    }

    private async Task LoadPagosAsync()
    {
        try
        {
            string apiUrl = $"{Routes.Api}Pagos/GetPagos";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var PagosList = JsonConvert.DeserializeObject<List<PagoDto>>(json);
                PagosList.OrderByDescending(x => x.Id);

                Pagos.Clear();
                foreach (var pago in PagosList)
                {
                    Pagos.Add(pago);
                }
            }
            else
            {
                await DisplayAlert("Error", "No se pudo cargar la lista de Pagos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }
}


public class EstadoColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string estado)
        {
            return estado.ToLower() switch
            {
                "completado" => Color.FromArgb("#4CAF50"),
                "pendiente" => Color.FromArgb("#FFC107"),
                "fallido" => Color.FromArgb("#F44336"),
                _ => Color.FromArgb("#9E9E9E"),
            };
        }
        return Color.FromArgb("#9E9E9E");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
