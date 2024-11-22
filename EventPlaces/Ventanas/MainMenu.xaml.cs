using EventPlaces.Event_Places;
using EventPlaces.Pages;
using Microsoft.Maui.Controls;

namespace EventPlaces.Ventanas
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private async void OnMoonButtonClicked(object sender, EventArgs e)
        {
            await MoonButton.ScaleTo(1.2, 100);  // Escala a 1.2 en 100 ms
            await MoonButton.ScaleTo(1.0, 100);  // Vuelve a escala 1.0 en 100 ms
        }

        private async void OnNavButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await button.ScaleTo(1.2, 100);  // Escala a 1.2 en 100 ms
                await button.ScaleTo(1.0, 100);  // Vuelve a escala 1.0 en 100 ms

                await Navigation.PushAsync(new SettingsPage());
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Animar el botón
                await button.ScaleTo(0.95, 100, Easing.CubicIn);
                await button.ScaleTo(1, 100, Easing.CubicOut);
            }
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                // Animación de escala al presionar el Frame completo
                await frame.ScaleTo(0.9, 50, Easing.CubicIn);  
                await frame.ScaleTo(1, 50, Easing.CubicOut);

                if (frame == PrincipalFrame)
                    await Navigation.PushAsync(new PerfilUsuario());
                else if (frame == FindPlaceFrame)
                    await Navigation.PushAsync(new NewPage1());
                else if (frame == MyPaymentFrame)
                    await Navigation.PushAsync(new SellReport());
                else if (frame == MyPlacesFrame)
                    await Navigation.PushAsync(new Reservados());
            }
        }

    }
}