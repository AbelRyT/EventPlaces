namespace EventPlaces.Pages;

public partial class CambiarPassword : ContentPage
{
	public CambiarPassword()
	{
		InitializeComponent();
	}

    private async void OnCambiarContrasenaClicked(object sender, EventArgs e)
    {
        // Aquí puedes agregar la lógica para validar las entradas y cambiar la contraseña

        // Ejemplo de validación simple
        bool success = true; // Simulación de éxito en la operación

        if (success)
        {
            await DisplayAlert("Éxito", "Tu contraseña ha sido cambiada exitosamente.", "OK");
            await Navigation.PushAsync(new PerfilUsuario()); // Regresa a la pantalla anterior
        }
        else
        {
            await DisplayAlert("Error", "Ocurrió un error al cambiar la contraseña. Inténtalo de nuevo.", "OK");
        }
    }
}