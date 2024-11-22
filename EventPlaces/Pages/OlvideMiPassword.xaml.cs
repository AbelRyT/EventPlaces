namespace EventPlaces.Pages;

public partial class OlvideMiPassword : ContentPage
{
	public OlvideMiPassword()
	{
		InitializeComponent();
	}

    private async void OnEnviarInstruccionesClicked(object sender, EventArgs e)
    {
        // L�gica para enviar el correo de restablecimiento de contrase�a
        // Aqu� ir�a el c�digo para verificar el correo e iniciar el proceso

        await DisplayAlert("Instrucciones Enviadas", "Se han enviado las instrucciones de recuperaci�n a tu correo electr�nico.", "OK");
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar de regreso a la p�gina de inicio de sesi�n
        await Navigation.PopAsync();
    }
}