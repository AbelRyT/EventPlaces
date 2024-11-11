namespace EventPlaces.Pages;

public partial class CancelarReservaPage : ContentPage
{
	public CancelarReservaPage()
	{
		InitializeComponent();
	}

    private async void OnConfirmarCancelacionClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas cancelar la reserva?", "Sí", "No");
        if (confirm)
        {
            // Lógica para cancelar la reserva
            await DisplayAlert("Éxito", "Tu reserva ha sido cancelada correctamente.", "OK");
            await Navigation.PopToRootAsync(); // Vuelve a la pantalla principal
        }
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Vuelve a la pantalla anterior
    }
}