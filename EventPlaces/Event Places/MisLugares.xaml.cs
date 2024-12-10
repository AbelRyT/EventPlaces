using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventPlaces.Event_Places
{
    public partial class MisLugares : ContentPage
    {
        public ObservableCollection<MiLugares> Lugares { get; set; }

        public MisLugares()
        {
            InitializeComponent();

            // Inicializamos la colección de lugares en el constructor
            Lugares = new ObservableCollection<MiLugares>
            {
                new MiLugares
                {
                    Title = "Emotions Puerto Plata",
                    Habitaciones = "2 \n",
                    Banos = "2 \n",
                    Parqueos = "1 \n",
                    Ubicacion = "Puerto Plata \n",
                    Price = "9,500",
                    ImageUrl = "/images/imagen1.jpg" // Cambia esta URL a la imagen real
                },
                new MiLugares
                {
                    Title = "Residencial Majestuoso Thomen",
                    Habitaciones = "3 \n",
                    Banos = "1 \n",
                    Parqueos = "1 \n",
                    Ubicacion = "Punta Cana \n",
                    Price = "15,000",
                    ImageUrl = "/images/imagen2.jpg"
                },
                new MiLugares
                {
                    Title = "Torre de lujo entrada a Santiago",
                    Habitaciones = "5 \n",
                    Banos = "2 \n",
                    Parqueos = "1 \n",
                    Ubicacion = "Santiago \n",
                    Price = "17,500",
                    ImageUrl = "/images/imagen3.jpg"
                }
              
            };

            // Establecemos el contexto de datos para la página
            BindingContext = this;
        }


        private async void ShowNotification()
        {
            // Mostrar la barra de notificación
            NotificationBar.IsVisible = true;

            // Animación de aparición
            await NotificationBar.FadeTo(1, 250); // Duración: 250ms

            // Esperar 3 segundos
            await Task.Delay(3000);

            // Animación de desaparición
            await NotificationBar.FadeTo(0, 250); // Duración: 250ms

            // Ocultar la barra
            NotificationBar.IsVisible = false;
        }

        private void OnHeartTapped(object sender, EventArgs e)
        {
            // Mostrar notificación
            ShowNotification();
        }


    }




    // Definimos la clase MiLugares
    public class MiLugares
    {
        public string Title { get; set; }
        public string Habitaciones { get; set; }
        public string Banos { get; set; }
        public string Parqueos { get; set; }
        public string Ubicacion { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
