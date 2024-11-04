using System.Collections.ObjectModel;

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
                },
                new MiLugares
                {
                    Title = "Residencial en Gurabo",
                    Habitaciones = "2 \n",
                    Banos = "1 \n",
                    Parqueos = "2 \n",
                    Ubicacion = "Gurabo \n",
                    Price = "22,500",
                    ImageUrl = "/images/imagen4.jpg"
                },
                new MiLugares
                {
                    Title = "Villa Laura Jarabacoa",
                    Habitaciones = "4 \n",
                    Banos = "2 \n",
                    Parqueos = "3 \n",
                    Ubicacion = "Jarabacoa \n",
                    Price = "10,400",
                    ImageUrl = "/images/imagen5.jpg"
                }
            };

            // Establecemos el contexto de datos para la página
            BindingContext = this;
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
