using EventPlaces.Event_Places;
using EventPlaces.Ventanas;

namespace EventPlaces.Pages;

public partial class CancelarReservaPage : ContentPage
{
	public CancelarReservaPage()
	{
		InitializeComponent();
	}

    private async void OnConfirmarCancelacionClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmaci�n", "�Est�s seguro de que deseas cancelar la reserva?", "S�", "No");
        if (confirm)
        {
            // L�gica para cancelar la reserva
            await DisplayAlert("�xito", "Tu reserva ha sido cancelada correctamente.", "OK");
            await Navigation.PushAsync(new MainMenu
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                ()); // Vuelve a la pantalla principal
        }
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Reservados()); // Vuelve a la pantalla anterior
    }
}