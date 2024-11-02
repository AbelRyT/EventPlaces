using Microsoft.Maui.Controls;
namespace EventPlaces.Ventanas;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
    private async void OnNavButtonClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            await button.ScaleTo(1.2, 100);  // Escala a 1.2 en 100 ms
            await button.ScaleTo(1.0, 100);  // Vuelve a escala 1.0 en 100 ms
        }
    }
}