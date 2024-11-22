namespace EventPlaces.Pages;

public partial class EditarPerfil : ContentPage
{
	public EditarPerfil()
	{
		InitializeComponent();
	}

    private async void OnChangeImageClicked(object sender, EventArgs e)
    {
        // Lógica para cambiar la imagen de perfil (puedes usar plugins para seleccionar una imagen de la galería)
        await DisplayAlert("Cambiar Imagen", "Funcionalidad de cambio de imagen no implementada.", "OK");
    }

    private async void OnGuardarCambiosClicked(object sender, EventArgs e)
    {
        // Lógica para validar y guardar los cambios en el perfil

        bool success = true; // Simulación de éxito en la operación

        if (success)
        {
            await DisplayAlert("Éxito", "Tus cambios han sido guardados exitosamente.", "OK");
            await Navigation.PushAsync(new PerfilUsuario());
        }
        else
        {
            await DisplayAlert("Error", "Ocurrió un error al guardar los cambios. Inténtalo de nuevo.", "OK");
        }
    }
}