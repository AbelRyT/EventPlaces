
using EventPlaces.Event_Places;

namespace EventPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new buscar_lugar());

            // MainPage = new DescripcionReserva();
            //  MainPage = new MisLugares();
            //MainPage = new hacerreservas();
            //MainPage = new Reservados();
            // MainPage = new buscar_lugar();
            //MainPage = new cant_huespedes();
            MainPage = new NewPage1();
            MainPage = new NavigationPage(new NewPage1());
        }
    }
}
