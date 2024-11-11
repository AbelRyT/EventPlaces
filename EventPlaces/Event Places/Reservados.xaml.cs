using EventPlaces.Pages;
using System.Collections.ObjectModel;

namespace EventPlaces.Event_Places
{
    public partial class Reservados : ContentPage
    {
        public ObservableCollection<Reservacion> Reservaciones { get; set; }

        public Reservados()
        {
            InitializeComponent();

            // Lista de reservaciones de ejemplo
            Reservaciones = new ObservableCollection<Reservacion>
            {
                new Reservacion
                {
                    Titulo = "Emotions Puerto Plata",
                    Fecha = "23 de octubre del 2024",
                    Hora = "10:30 AM",
                    Estado = "Pendiente",
                    ImagenUrl = "/images/imagen1.jpg"
                },
                new Reservacion
                {
                    Titulo = "Residencial Majestuoso Thomen",
                    Fecha = "23 de octubre del 2024",
                    Hora = "10:30 AM",
                    Estado = "Confirmada",
                    ImagenUrl = "/images/imagen2.jpg"
                },
                new Reservacion
                {
                    Titulo = "Torre de lujo entrada a Santiago",
                    Fecha = "23 de octubre del 2024",
                    Hora = "10:30 AM",
                    Estado = "Pendiente",
                    ImagenUrl = "/images/imagen3.jpg"
                },
                new Reservacion
                {
                    Titulo = "Residencial en Gurabo",
                    Fecha = "23 de octubre del 2024",
                    Hora = "10:30 AM",
                    Estado = "Confirmada",
                    ImagenUrl = "/images/imagen4.jpg"
                },
                new Reservacion
                {
                    Titulo = "Villa Laura Jarabacoa",
                    Fecha = "23 de octubre del 2024",
                    Hora = "10:30 AM",
                    Estado = "Pendiente",
                    ImagenUrl = "/images/imagen5.jpg"
                }
            };

            // Establecer el contexto de enlace de datos
            BindingContext = this;
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            // Navegar a la nueva página
            await Navigation.PushAsync(new hacerreservas());
        }

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarReservaPage());
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CancelarReservaPage());
        }
    }

    // Modelo de datos para la reservación
    public class Reservacion
    {
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
        public string ImagenUrl { get; set; }
    }
}
