namespace EventPlaces.Pages;

public partial class OlvideMiPassword : ContentPage
{
	public OlvideMiPassword()
	{
		InitializeComponent();
	}

    private async void OnEnviarInstruccionesClicked(object sender, EventArgs e)
    {
        // Lógica para enviar el correo de restablecimiento de contraseña
        // Aquí iría el código para verificar el correo e iniciar el proceso

        await DisplayAlert("Instrucciones Enviadas", "Se han enviado las instrucciones de recuperación a tu correo electrónico.", "OK");
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar de regreso a la página de inicio de sesión
        await Navigation.PopAsync();
    }
}