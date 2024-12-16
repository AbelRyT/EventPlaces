using Common;
using EventPlaces.Api;
using EventPlaces.Event_Places;
using Firebase.Auth;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EventPlaces.Pages;

public partial class LoginPage : ContentPage
{
    private bool isLoginInProgress = false; // Bandera para evitar m�ltiples ejecuciones
    private bool isPasswordVisible = true; // Estado de visibilidad de la contrase�a
    private readonly HttpClient _httpClient = new HttpClient();


    public LoginPage()
    {
        InitializeComponent();
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
        isPasswordVisible = true;
        passwordEntry.IsPassword = isPasswordVisible;
        UpdatePasswordVisibilityIcon(); // icono con que inicia la vista de la contrase�a
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar a la nueva p�gina
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
        // Verificar si ya hay un inicio de sesi�n en curso
        if (isLoginInProgress)
            return;

        // Indicar que el inicio de sesi�n est� en progreso
        isLoginInProgress = true;
        loginButton.IsEnabled = false; // Deshabilitar el bot�n

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
                    UsuarioDto userDto = new UsuarioDto
                    {
                        Email = emailEntry.Text
                    };

                    var jsonDTO = JsonSerializer.Serialize(userDto);
                    var content = new StringContent(jsonDTO, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync($"{Routes.Api}Usuarios/GetUsuarioByEmail", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var user = JsonSerializer.Deserialize<UsuarioDto>(json, options);
                        Constante.usuarioId = user.Id;

                        emailEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;

                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        await DisplayAlert("Error", response.ReasonPhrase, "OK");
                    }


                }
                else
                {
                    await DisplayAlert("Autenticaci�n Inv�lida", "Usuario o Contrase�a Incorrecta", "OK");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                    await DisplayAlert("Autenticaci�n Inv�lida", "Usuario o Contrase�a Incorrecta", "OK");
                else
                    await DisplayAlert("Error", $"No se pudo iniciar sesi�n: {ex.Message}", "OK");
            }
            finally
            {
                // Ocultar el indicador de carga
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }
        }

        // Reiniciar el estado del bot�n y la bandera
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
        // Alternar visibilidad de la contrase�a
        if (isPasswordVisible)
        {
            isPasswordVisible = false;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el �cono a "ojo cerrado"
            UpdatePasswordVisibilityIcon();

            // Esperar un segundo y volver a ocultar
            await Task.Delay(1000);
            isPasswordVisible = true;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el �cono a "ojo abierto"
            UpdatePasswordVisibilityIcon();
        }
        else
        {
            // Mostrar la contrase�a mientras el bot�n est� activado
            isPasswordVisible = true;
            passwordEntry.IsPassword = isPasswordVisible;

            // Actualizar el �cono a "ojo abierto"
            UpdatePasswordVisibilityIcon();
        }
    }

    private void UpdatePasswordVisibilityIcon()
    {
        togglePasswordVisibilityButton.Source = isPasswordVisible ? "eye_icon.png" : "eye_off_icon.png";
    }

}
