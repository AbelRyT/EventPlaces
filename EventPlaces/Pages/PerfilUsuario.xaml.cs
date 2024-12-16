using Common;
using EventPlaces.Api;
using System.Text.Json;
using System.Text;

namespace EventPlaces.Pages;

public partial class PerfilUsuario : ContentPage
{
    private readonly HttpClient _httpClient;
    private UsuarioDto _usuario;

    public PerfilUsuario()
    {
        InitializeComponent();
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
        CargarPerfilUsuario();
    }

    private async void CargarPerfilUsuario()
    {
        try
        {
            // Mostrar indicador de carga
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;

            var response = await _httpClient.GetAsync($"{Routes.Api}Usuarios/{Constante.usuarioId}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _usuario = JsonSerializer.Deserialize<UsuarioDto>(json, options);

                // Actualizar la interfaz con los datos del usuario
                if (_usuario != null)
                {
                    emailEntry.Text = _usuario.Email;
                    nameEntry.Text = _usuario.Nombre;
                    phoneEntry.Text = _usuario.Telefono;
                    profileImage.Source = string.IsNullOrEmpty(_usuario.ImagenURL)
                        ? "default_profile.png"
                        : _usuario.ImagenURL;
                }
            }
            else
            {
                await DisplayAlert("Error", "No se pudo cargar el perfil del usuario.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "OK");
        }
        finally
        {
            // Ocultar indicador de carga
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        try
        {
            if (_usuario == null)
            {
                await DisplayAlert("Error", "No se pudo obtener los datos del usuario.", "OK");
                return;
            }

            // Actualizar datos del usuario
            _usuario.Nombre = nameEntry.Text;
            _usuario.Telefono = phoneEntry.Text;

            // Serializar y enviar los cambios
            var jsonDTO = JsonSerializer.Serialize(_usuario);
            var content = new StringContent(jsonDTO, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{Routes.Api}Usuarios/{_usuario.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�xito", "Los cambios se han guardado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar los cambios.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "OK");
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Confirmar cierre de sesi�n
        bool confirmLogout = await DisplayAlert("Cerrar Sesi�n", "�Est�s seguro de que deseas cerrar sesi�n?", "S�", "No");
        if (!confirmLogout) return;

        try
        {
            // Reiniciar datos del usuario y redirigir al inicio de sesi�n
            Constante.usuarioId = 0;
            await Navigation.PushAsync(new LoginPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cerrar sesi�n: {ex.Message}", "OK");
        }
    }
}