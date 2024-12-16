using Firebase.Auth;
using System.Text.RegularExpressions;


namespace EventPlaces.Pages;

public partial class OlvideMiPassword : ContentPage
{
    public OlvideMiPassword()
    {
        InitializeComponent();
    }


    private async void OnResetPasswordClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;

        // Validar correo electrónico
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            emailErrorLabel.Text = "Por favor, ingresa un correo válido.";
            emailErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            emailErrorLabel.IsVisible = false;
        }

        try
        {
            // Mostrar indicador de carga
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
            await authProvider.SendPasswordResetEmailAsync(email);

            await DisplayAlert("Éxito", "Se ha enviado un enlace de restablecimiento de contraseña a tu correo.", "OK");
            await Navigation.PushAsync(new LoginPage()); 
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo enviar el correo: {ex.Message}", "OK");
        }
        finally
        {
            // Ocultar indicador de carga
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }
    }

    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar de regreso a la página de inicio de sesión
        await Navigation.PopAsync();
    }
}
