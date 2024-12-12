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

        private async void OnEditarClicked(object sender, EventArgs e)
        {
            // Obtiene el contexto del elemento actual para pasarlo a la p�gina de detalles
            var button = sender as Button;
            var reservacion = button?.BindingContext as Reservacion;

            if (reservacion != null)
            {
                // Navega a la p�gina DescripcionReservas con el objeto reservacion
                await Navigation.PushAsync(new DescripcionReserva());
            }
        }


        private async void ShowNotification()
        {
            // Mostrar la barra de notificaci�n
            NotificationBar.IsVisible = true;

            // Animaci�n de aparici�n
            await NotificationBar.FadeTo(1, 250); // Duraci�n: 250ms

            // Esperar 3 segundos
            await Task.Delay(3000);

            // Animaci�n de desaparici�n
            await NotificationBar.FadeTo(0, 250); // Duraci�n: 250ms

            // Ocultar la barra
            NotificationBar.IsVisible = false;
        }

        private void OnCancelarClicked(object sender, EventArgs e)
        {
            // Mostrar notificaci�n
            ShowNotification();
        }

    }



    // Modelo de datos para la reservaci�n
    public class Reservacion
    {
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
        public string ImagenUrl { get; set; }
    }
}
