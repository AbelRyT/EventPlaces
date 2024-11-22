using System.Collections.ObjectModel;

namespace EventPlaces.Event_Places
{
    public partial class MenuPrincipal : ContentPage
    {
        // La propiedad Reservations debe estar dentro de la clase
        public ObservableCollection<Reservation> Reservations { get; set; }

        public MenuPrincipal()
        {
            InitializeComponent();

            // Inicializamos la colección de reservaciones en el constructor
            Reservations = new ObservableCollection<Reservation>
            {
                new Reservation
                {
                    Title = "Emotions Puerto Plata",
                    Description = "Playa Dorada\nPiscina\nSpa\nBar",
                    Price = "9,500",
                    ImageUrl = "/images/imagen1.jpg"// Cambia esta URL a la imagen real
                },
                new Reservation
                {
                    Title = "Residencial Majestuoso Thomen",
                    Description = "Hasta 3 habitaciones\nSeguridad 24 Horas\nÁrea social y terraza",
                    Price = "15,000",
                    ImageUrl = "/images/imagen2.jpg"
                },
                new Reservation
                {
                    Title = "Torre de lujo entrada a Santiago",
                    Description = "Desde 5 habitaciones\n2 Baños\n1 Parqueos",
                    Price = "17,500",
                    ImageUrl = "/images/imagen3.jpg"
                },
                new Reservation
                {
                    Title = "Residencial en Gurabo",
                    Description = "2 Habitaciones\n1 Baño\nHasta 2 Parqueos",
                    Price = "22,500",
                    ImageUrl = "/images/imagen4.jpg"
                },
                new Reservation
                {
                    Title = "Villa Laura Jarabacoa",
                    Description = "3 Habitaciones\nPiscina al aire libre\nParqueo gratis incluido\nCuenta con balcón",
                    Price = "10,400",
                    ImageUrl = "/images/imagen5.jpg"
                }
            };

            // Establecemos el contexto de datos para la página
            BindingContext = this;
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            // Navegar a la nueva página
            await Navigation.PushAsync(new DescripcionReserva());
        }

        private async void LugarButton_Clicked(object sender, EventArgs e)
        {
            // Lógica para seleccionar o buscar un lugar
            await DisplayAlert("Lugar", "Funcionalidad de selección de lugar en desarrollo", "OK");
        }

        private async void FechasButton_Clicked(object sender, EventArgs e)
        {
            // Lógica para seleccionar un rango de fechas
            var startDate = await DisplayPromptAsync("Seleccionar Fechas", "Introduce la fecha de inicio (formato: dd/MM/yyyy):");
            var endDate = await DisplayPromptAsync("Seleccionar Fechas", "Introduce la fecha de fin (formato: dd/MM/yyyy):");

            if (!string.IsNullOrWhiteSpace(startDate) && !string.IsNullOrWhiteSpace(endDate))
            {
                DateTime start, end;
                if (DateTime.TryParse(startDate, out start) && DateTime.TryParse(endDate, out end))
                {
                    await DisplayAlert("Fechas Seleccionadas", $"Desde: {start:dd/MM/yyyy} Hasta: {end:dd/MM/yyyy}", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Las fechas introducidas no son válidas", "OK");
                }
            }
        }

        private async void HuespedButton_Clicked(object sender, EventArgs e)
        {
            // Lógica para seleccionar la cantidad de huéspedes
            var huespedes = await DisplayPromptAsync("Huéspedes", "Introduce el número de huéspedes:");
            if (!string.IsNullOrWhiteSpace(huespedes))
            {
                await DisplayAlert("Huéspedes", $"Número de huéspedes: {huespedes}", "OK");
            }
        }

        private async void BuscarButton_Clicked(object sender, EventArgs e)
        {
            // Lógica para realizar la búsqueda con los filtros seleccionados
            await DisplayAlert("Buscar", "Funcionalidad de búsqueda en desarrollo", "OK");
        }

    }

    // Definimos la clase Reservation
    public class Reservation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
