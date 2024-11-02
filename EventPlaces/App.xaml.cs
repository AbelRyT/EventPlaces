using EventPlaces.Ventanas;
namespace EventPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SettingsPage();
        }
    }
}
