namespace EventPlaces.Pages;

public partial class EditarPerfil : ContentPage
{
	public EditarPerfil()
	{
		InitializeComponent();
	}

    private async void OnChangeImageClicked(object sender, EventArgs e)
    {
        // L�gica para cambiar la imagen de perfil (puedes usar plugins para seleccionar una imagen de la galer�a)
        await DisplayAlert("Cambiar Imagen", "Funcionalidad de cambio de imagen no implementada.", "OK");
    }

    private async void OnGuardarCambiosClicked(object sender, EventArgs e)
    {
        // L�gica para validar y guardar los cambios en el perfil

        bool success = true; // Simulaci�n de �xito en la operaci�n

        if (success)
        {
            await DisplayAlert("�xito", "Tus cambios han sido guardados exitosamente.", "OK");
            await Navigation.PushAsync(new PerfilUsuario());
        }
        else
        {
            await DisplayAlert("Error", "Ocurri� un error al guardar los cambios. Int�ntalo de nuevo.", "OK");
        }
    }
}