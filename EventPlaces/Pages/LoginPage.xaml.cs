
using EventPlaces.Event_Places;
using EventPlaces.Ventanas;
using Firebase.Auth;
using System.Text.RegularExpressions;

namespace EventPlaces.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar a la nueva p�gina
        await Navigation.PushAsync(new Registrarse());
    }

    private void OnEmailCompleted(object sender, EventArgs e)
    {
        passwordEntry.Focus(); // Pasa al siguiente campo (Contrase�a)
    }

    private void OnPasswordCompleted(object sender, EventArgs e)
    {
        // Aqu� puedes llamar al m�todo de login o hacer cualquier otra acci�n
        OnLoginClicked(sender, e);
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {

        bool isValid = true;

        // Validaci�n de correo
        if (string.IsNullOrWhiteSpace(emailEntry.Text) || !IsValidEmail(emailEntry.Text))
        {
            emailErrorLabel.Text = "Correo no v�lido";
            emailErrorLabel.IsVisible = true;
            isValid = false;
        }
        else
        {
            emailErrorLabel.IsVisible = false;
        }

        // Validaci�n de contrase�a
        if (string.IsNullOrWhiteSpace(passwordEntry.Text) || passwordEntry.Text.Length < 6)
        {
            passwordErrorLabel.Text = "La contrase�a debe tener al menos 6 caracteres";
            passwordErrorLabel.IsVisible = true;
            isValid = false;
        }
        else
        {
            passwordErrorLabel.IsVisible = false;
        }

        // Si todo es v�lido, proceder con el login
        if (isValid)
        {
            try
            {
                // Mostrar el indicador de carga
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
                string token = auth.FirebaseToken;
                if (!string.IsNullOrEmpty(token))
                {
                    await Navigation.PushAsync(new MainMenu());
                }
                else
                {
                    await DisplayAlert("Autenticacion Invalidad", "Usuario o Contrase�a Incorrecta", "OK");
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                    await DisplayAlert("Autenticacion Invalidad", "Usuario o Contrase�a Incorrecta", "OK");
                else
                    await DisplayAlert("Error", $"No se pudo iniciar sesi�n: {ex.Message}", "OK");
            }
            finally
            {
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new OlvideMiPassword());
    }
}