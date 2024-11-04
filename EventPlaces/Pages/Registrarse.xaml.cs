using Firebase.Auth;

namespace EventPlaces.Pages;

public partial class Registrarse : ContentPage
{
	public Registrarse()
	{
		InitializeComponent();
	}

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDtL1yuMeyR4sDmXVD-xe7Z69ikiOZFvMY"));
            await authProvider.CreateUserWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo registrar: {ex.Message}", "OK");
        }
    }
}