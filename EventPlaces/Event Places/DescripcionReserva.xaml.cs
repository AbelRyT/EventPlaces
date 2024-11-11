
namespace EventPlaces.Event_Places;

public partial class DescripcionReserva : ContentPage
{
	public DescripcionReserva()
	{
		InitializeComponent();
	}

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar a la nueva p�gina
        await Navigation.PushAsync(new SeleccionFechasPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Confirmaci�n", "�Est�s seguro de que deseas proceder con la reserva?", "S�", "No");

        if (isConfirmed)
        {
            // Si el usuario confirma, procede a la siguiente p�gina
            await Navigation.PushAsync(new Reservados());
        }
        else
        {
            // Si el usuario cancela, no hace nada o muestra un mensaje
            await DisplayAlert("Cancelado", "La reserva no se ha realizado.", "OK");
        }
    }
}