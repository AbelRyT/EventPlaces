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
        bool confirm = await DisplayAlert("Confirmaci�n de Pago", "�Deseas proceder con el pago?", "S�", "No");
        if (confirm)
        {
            // Simula el procesamiento del pago
            await DisplayAlert("�xito", "Pago realizado con �xito. �Gracias por tu reserva!", "OK");
            await Navigation.PushAsync( new MainMenu()); // Regresa a la pantalla principal o de reservas
        }
        else
        {
            await DisplayAlert("Cancelado", "El pago ha sido cancelado.", "OK");
        }
    }
}