
using EventPlaces.Pages;

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
            await Navigation.PushAsync(new PagoReserva());
        }
        else
        {
            // Si el usuario cancela, no hace nada o muestra un mensaje
            await DisplayAlert("Cancelado", "La reserva no se ha realizado.", "OK");
        }
    }

    // Navegar a la p�gina de Favoritos
    private void OnFavoritesClicked(object sender, EventArgs e)
    {
        // Navegaci�n a la p�gina de favoritos (ejemplo)
        //Navigation.PushAsync(new FavoritesPage());
    }

    // Navegar a la p�gina principal
    private void OnHomeClicked(object sender, EventArgs e)
    {
        // Navegaci�n a la p�gina principal (ejemplo)
        Navigation.PushAsync(new MenuPrincipal());
    }

    // Navegar a la p�gina de Mis Lugares
    private void OnMyPlacesClicked(object sender, EventArgs e)
    {
        // Navegaci�n a la p�gina de Mis Lugares (ejemplo)
        //Navigation.PushAsync(new MyPlacesPage());
    }

    // Navegar a la p�gina de Configuraci�n
    private void OnSettingsClicked(object sender, EventArgs e)
    {
        // Navegaci�n a la p�gina de Configuraci�n (ejemplo)
        //Navigation.PushAsync(new SettingsPage());
    }
}