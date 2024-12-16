
using EventPlaces.Event_Places;
using EventPlaces.Pages;

namespace EventPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            bool isAuthenticated = false;

            if (isAuthenticated)
            {
                MainPage = new AppShell(); // Mostrar menú si está autenticado
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage()); // Mostrar Login si no lo está
            }
        }
    }
}
