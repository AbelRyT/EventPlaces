
using EventPlaces.Event_Places;

namespace EventPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


           MainPage = new DescripcionReserva();  //LISTIO!!
          MainPage = new NavigationPage(new DescripcionReserva());

          // MainPage = new MisLugares(); //Listo,Esta pagina ahora es de Mis Favoritos!
            //MainPage = new hacerreservas(); 
            //MainPage = new Reservados();
            //MainPage = new NavigationPage(new Reservados());

            // *MainPage = new buscar_lugar();
            // MainPage = new NavigationPage(new buscar_lugar());

            //MainPage = new cant_huespedes();

            //MainPage = new NewPage1();   //LISTIO!!
            //MainPage = new NavigationPage(new NewPage1());
        }
    }
}
