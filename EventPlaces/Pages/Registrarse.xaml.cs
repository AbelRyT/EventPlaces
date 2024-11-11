using Firebase.Auth;
using System.Text.RegularExpressions;

namespace EventPlaces.Pages;

public partial class Registrarse : ContentPage
{
	public Registrarse()
	{
		InitializeComponent();
	}

    private async void OnRegisterClicked(object sender, EventArgs e)
    {

        bool isValid = true;

        // Validación de correo
        if (string.IsNullOrWhiteSpace(emailEntry.Text) || !IsValidEmail(emailEntry.Text))
        {
            emailErrorLabel.Text = "Correo no válido";
            emailErrorLabel.IsVisible = true;
            isValid = false;
        }
        else
        {
            emailErrorLabel.IsVisible = false;
        }

        // Validación de contraseña
        if (string.IsNullOrWhiteSpace(passwordEntry.Text) || passwordEntry.Text.Length < 6)
        {
            passwordErrorLabel.Text = "La contraseña debe tener al menos 6 caracteres";
            passwordErrorLabel.IsVisible = true;
            isValid = false;
        }
        else
        {
            passwordErrorLabel.IsVisible = false;
        }

        // Validación de confirmación de contraseña
        if (passwordEntry.Text != confirmPasswordEntry.Text)
        {
            confirmPasswordErrorLabel.Text = "Las contraseñas no coinciden";
            confirmPasswordErrorLabel.IsVisible = true;
            isValid = false;
        }
        else
        {
            confirmPasswordErrorLabel.IsVisible = false;
        }

        // Si todo es válido, proceder con el registro
        if (isValid)
        {
            try
            {
                // Mostrar el indicador de carga
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
                await authProvider.CreateUserWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
                await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo registrar: {ex.Message}", "OK");
            }
            finally
            {
                // Mostrar el indicador de carga
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }
        }
       
    }

    private void OnEmailCompleted(object sender, EventArgs e)
    {
        passwordEntry.Focus(); 
    }

    private void OnPasswordCompleted(object sender, EventArgs e)
    {
        confirmPasswordEntry.Focus();
    }

    private void OnConfirmPasswordCompleted(object sender, EventArgs e)
    {
        OnRegisterClicked(sender, e);
    }


    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}