
using EventPlaces.Event_Places;
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
        // Navegar a la nueva p�gina
        await Navigation.PushAsync(new Registrarse());
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
            string token = auth.FirebaseToken;
            if (!string.IsNullOrEmpty(token))
            {
                await DisplayAlert("�xito", "Inicio de sesi�n exitoso", "OK");
                await Navigation.PushAsync(new NewPage1());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo iniciar sesi�n: {ex.Message}", "OK");
        }
    }


}