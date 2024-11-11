
using EventPlaces.Event_Places;
using EventPlaces.Ventanas;
using Firebase.Auth;

namespace EventPlaces.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Navegar a la nueva página
        await Navigation.PushAsync(new Registrarse());
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {

            //await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            //await Navigation.PushAsync(new MainMenu());

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
            string token = auth.FirebaseToken;
            if (!string.IsNullOrEmpty(token))
            {
                await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
                await Navigation.PushAsync(new MainMenu());
            }
            else
            {
                await DisplayAlert("Autenticacion Invalidad", "Usuario o Contraseña Incorrecta", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo iniciar sesión: {ex.Message}", "OK");
        }
    }


}