using EventPlaces.Ventanas;

namespace EventPlaces.Pages;

public partial class PagoReserva : ContentPage
{
	public PagoReserva()
	{
		InitializeComponent();
    }

    private async void OnPagarAhoraClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmación de Pago", "¿Deseas proceder con el pago?", "Sí", "No");
        if (confirm)
        {
            // Simula el procesamiento del pago
            await DisplayAlert("Éxito", "Pago realizado con éxito. ¡Gracias por tu reserva!", "OK");
            await Navigation.PushAsync( new MainMenu()); // Regresa a la pantalla principal o de reservas
        }
        else
        {
            await DisplayAlert("Cancelado", "El pago ha sido cancelado.", "OK");
        }
    }
}