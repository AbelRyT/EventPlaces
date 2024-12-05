using EventPlaces.Event_Places;
using Firebase.Auth;
using System.Text.RegularExpressions;

namespace EventPlaces.Pages;

public partial class LoginPage : ContentPage
{
    private bool isLoginInProgress = false; // Bandera para evitar múltiples ejecuciones
    private bool isPasswordVisible = true; // Estado de visibilidad de la contraseña

    public LoginPage()
    {
        InitializeComponent();
        isPasswordVisible = true; 
    passwordEntry.IsPassword = isPasswordVisible;
        UpdatePasswordVisibilityIcon(); // icono con que inicia la vista de la contraseña
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar a la nueva página
        await Navigation.PushAsync(new Registrarse());
    }

    private void OnEmailCompleted(object sender, EventArgs e)
    {
        passwordEntry.Focus(); 
    }

    private void OnPasswordCompleted(object sender, EventArgs e)
    {
        
        OnLoginClicked(sender, e);
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Verificar si ya hay un inicio de sesión en curso
        if (isLoginInProgress)
            return;

        // Indicar que el inicio de sesión está en progreso
        isLoginInProgress = true;
        loginButton.IsEnabled = false; // Deshabilitar el botón

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

        
        if (isValid)
        {
            await Navigation.PushAsync(new PerfilUsuario());
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
                    // Limpia los campos de entrada después de un inicio de sesión exitoso
                    emailEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;

                    //await Navigation.PushAsync(new MenuPrincipal());
                }
                else
                {
                    await DisplayAlert("Autenticación Inválida", "Usuario o Contraseña Incorrecta", "OK");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                    await DisplayAlert("Autenticación Inválida", "Usuario o Contraseña Incorrecta", "OK");
                else
                    await DisplayAlert("Error", $"No se pudo iniciar sesión: {ex.Message}", "OK");
            }
            finally
            {
                // Ocultar el indicador de carga
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }
        }

        // Reiniciar el estado del botón y la bandera
        isLoginInProgress = false;
        loginButton.IsEnabled = true;
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

    private async void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
    {
        // Alternar visibilidad de la contraseña
        if (isPasswordVisible)
        {
            isPasswordVisible = false;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el ícono a "ojo cerrado"
            UpdatePasswordVisibilityIcon();

            // Esperar un segundo y volver a ocultar
            await Task.Delay(1000);
            isPasswordVisible = true;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el ícono a "ojo abierto"
            UpdatePasswordVisibilityIcon();
        }
        else
        {
            // Mostrar la contraseña mientras el botón está activado
            isPasswordVisible = true;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el ícono a "ojo abierto"
            UpdatePasswordVisibilityIcon();
        }
    }

    private void UpdatePasswordVisibilityIcon()
    {
        togglePasswordVisibilityButton.Source = isPasswordVisible ? "eye_icon.png" : "eye_off_icon.png";
    }

}
