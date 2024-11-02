
using EventPlaces.Event_Places;

namespace EventPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           MainPage = new AppShell();
           //MainPage = new Reservados();
           //MainPage = new NewPage1();
        }
    }
}
