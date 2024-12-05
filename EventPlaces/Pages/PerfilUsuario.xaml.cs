using EventPlaces.Event_Places;

namespace EventPlaces.Pages;

public partial class PerfilUsuario : ContentPage
{
	public PerfilUsuario()
	{
		InitializeComponent();
	}

    private async void OnEditarPerfilClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditarPerfil());
    }

    private async void OnCambiarContrasenaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CambiarPassword());
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres cerrar sesión?", "Sí", "No");
        if (confirm)
        {
            // Lógica para cerrar sesión y volver a la pantalla de inicio de sesión
            await DisplayAlert("Cerrar Sesión", "Has cerrado sesión exitosamente.", "OK");
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Regresa al menu
        await Navigation.PushAsync(new MenuPrincipal());
    }
}