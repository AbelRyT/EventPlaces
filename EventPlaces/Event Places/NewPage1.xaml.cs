using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventPlaces.Event_Places
{
    public partial class NewPage1 : ContentPage
    {
        // Colecci�n de reservaciones
        public ObservableCollection<Reservation> Reservations { get; set; }

        public NewPage1()
        {
            InitializeComponent();

            // Inicializamos la colecci�n de reservaciones
            Reservations = new ObservableCollection<Reservation>
            {
                new Reservation
                {
                    Title = "Emotions Puerto Plata",
                    Description = "Playa Dorada\nPiscina\nSpa\nBar",
                    Price = "9,500",
                    ImageUrl = "images/imagen1.jpg", // Cambia a la ruta v�lida
                    NavigateCommand = new Command(() => NavigateToDetails("Emotions Puerto Plata"))
                },
                new Reservation
                {
                    Title = "Residencial Majestuoso Thomen",
                    Description = "Hasta 3 habitaciones\nSeguridad 24 Horas\n�rea social y terraza",
                    Price = "15,000",
                    ImageUrl = "images/imagen2.jpg",
                    NavigateCommand = new Command(() => NavigateToDetails("Residencial Majestuoso Thomen"))
                },
                new Reservation
                {
                    Title = "Torre de lujo entrada a Santiago",
                    Description = "Desde 5 habitaciones\n2 Ba�os\n1 Parqueos",
                    Price = "17,500",
                    ImageUrl = "images/imagen3.jpg",
                    NavigateCommand = new Command(() => NavigateToDetails("Torre de lujo entrada a Santiago"))
                },
                new Reservation
                {
                    Title = "Residencial en Gurabo",
                    Description = "2 Habitaciones\n1 Ba�o\nHasta 2 Parqueos",
                    Price = "22,500",
                    ImageUrl = "images/imagen4.jpg",
                    NavigateCommand = new Command(() => NavigateToDetails("Residencial en Gurabo"))
                },
                new Reservation
                {
                    Title = "Villa Laura Jarabacoa",
                    Description = "3 Habitaciones\nPiscina al aire libre\nParqueo gratis incluido\nCuenta con balc�n",
                    Price = "10,400",
                    ImageUrl = "images/imagen5.jpg",
                    NavigateCommand = new Command(() => NavigateToDetails("Villa Laura Jarabacoa"))
                }
            };

            // Establecemos el contexto de datos para la p�gina
            BindingContext = this;
        }

        // M�todo para manejar la navegaci�n
        private async void NavigateToDetails(string reservationTitle)
        {
            // Aqu� puedes navegar a una nueva p�gina
            await DisplayAlert("Reserva seleccionada", $"Navegando a {reservationTitle}", "OK");
            await Navigation.PushAsync(new DescripcionReserva()); // Cambia "MisLugares" por tu p�gina de destino
        }

        // M�todo para manejar el bot�n de b�squeda
        private async void OnSearchButtonTapped(object sender, EventArgs e)
        {
            // await DisplayAlert("Buscar", "Funci�n de b�squeda a�n no implementada", "OK");
            await Navigation.PushAsync(new buscar_lugar()); // Cambia "MisLugares" por tu p�gina de destino

        }
    }

    // Clase para representar reservaciones
    public class Reservation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public ICommand NavigateCommand { get; set; } // Comando para manejar la navegaci�n
    }
}
