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

        // Verifica si el campo está vacío
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo electrónico.", "OK");
            return;
        }

        // Verifica si el correo tiene un formato válido
        if (!IsValidEmail(email))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
            return;
        }

        // Lógica para enviar el correo de restablecimiento de contraseña
        await DisplayAlert("Instrucciones Enviadas", "Se han enviado las instrucciones de recuperación a tu correo electrónico.", "OK");
    }

    private bool IsValidEmail(string email)
    {
        // Expresión regular para validar el formato del correo electrónico
        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex);
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar de regreso a la página de inicio de sesión
        await Navigation.PopAsync();
    }
}
