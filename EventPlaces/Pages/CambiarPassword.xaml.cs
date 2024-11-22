namespace EventPlaces.Pages;

public partial class CambiarPassword : ContentPage
{
	public CambiarPassword()
	{
		InitializeComponent();
	}

    private async void OnCambiarContrasenaClicked(object sender, EventArgs e)
    {
        // Aqu� puedes agregar la l�gica para validar las entradas y cambiar la contrase�a

        // Ejemplo de validaci�n simple
        bool success = true; // Simulaci�n de �xito en la operaci�n

        if (success)
        {
            await DisplayAlert("�xito", "Tu contrase�a ha sido cambiada exitosamente.", "OK");
            await Navigation.PushAsync(new PerfilUsuario()); // Regresa a la pantalla anterior
        }
        else
        {
            await DisplayAlert("Error", "Ocurri� un error al cambiar la contrase�a. Int�ntalo de nuevo.", "OK");
        }
    }
}