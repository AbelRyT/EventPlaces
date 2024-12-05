
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
        // Navegar a la nueva página
        await Navigation.PushAsync(new SeleccionFechasPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas proceder con la reserva?", "Sí", "No");

        if (isConfirmed)
        {
            // Si el usuario confirma, procede a la siguiente página
            await Navigation.PushAsync(new PagoReserva());
        }
        else
        {
            // Si el usuario cancela, no hace nada o muestra un mensaje
            await DisplayAlert("Cancelado", "La reserva no se ha realizado.", "OK");
        }
    }

    // Navegar a la página de Favoritos
    private void OnFavoritesClicked(object sender, EventArgs e)
    {
        // Navegación a la página de favoritos (ejemplo)
        //Navigation.PushAsync(new FavoritesPage());
    }

    // Navegar a la página principal
    private void OnHomeClicked(object sender, EventArgs e)
    {
        // Navegación a la página principal (ejemplo)
        Navigation.PushAsync(new MenuPrincipal());
    }

    // Navegar a la página de Mis Lugares
    private void OnMyPlacesClicked(object sender, EventArgs e)
    {
        // Navegación a la página de Mis Lugares (ejemplo)
        //Navigation.PushAsync(new MyPlacesPage());
    }

    // Navegar a la página de Configuración
    private void OnSettingsClicked(object sender, EventArgs e)
    {
        // Navegación a la página de Configuración (ejemplo)
        //Navigation.PushAsync(new SettingsPage());
    }
}