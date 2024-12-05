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
        bool confirm = await DisplayAlert("Confirmaci�n", "�Est�s seguro de que quieres cerrar sesi�n?", "S�", "No");
        if (confirm)
        {
            // L�gica para cerrar sesi�n y volver a la pantalla de inicio de sesi�n
            await DisplayAlert("Cerrar Sesi�n", "Has cerrado sesi�n exitosamente.", "OK");
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Regresa al menu
        await Navigation.PushAsync(new MenuPrincipal());
    }
}