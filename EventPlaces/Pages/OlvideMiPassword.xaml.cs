using System.Text.RegularExpressions;


namespace EventPlaces.Pages;

public partial class OlvideMiPassword : ContentPage
{
    public OlvideMiPassword()
    {
        InitializeComponent();
    }


    private async void OnEnviarInstruccionesClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;

        // Verifica si el campo est� vac�o
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo electr�nico.", "OK");
            return;
        }

        // Verifica si el correo tiene un formato v�lido
        if (!IsValidEmail(email))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo electr�nico v�lido.", "OK");
            return;
        }

        // L�gica para enviar el correo de restablecimiento de contrase�a
        await DisplayAlert("Instrucciones Enviadas", "Se han enviado las instrucciones de recuperaci�n a tu correo electr�nico.", "OK");
    }

    private bool IsValidEmail(string email)
    {
        // Expresi�n regular para validar el formato del correo electr�nico
        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex);
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar de regreso a la p�gina de inicio de sesi�n
        await Navigation.PopAsync();
    }
}
