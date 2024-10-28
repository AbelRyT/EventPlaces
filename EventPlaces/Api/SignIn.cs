using System.Text.Json;

namespace EventPlaces.Api
{
    public class SignIn
    {
        public async Task SignInWithGoogle(string googleIdToken)
        {
            var firebaseApiKey = "AIzaSyAh3iS9LWzBL_pySGaChaGbbw4wt3MzJ5c";
            var firebaseUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={firebaseApiKey}";

            var requestData = new
            {
                postBody = $"id_token={googleIdToken}&providerId=google.com",
                requestUri = "http://localhost",
                returnSecureToken = true
            };

            var client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(requestData), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(firebaseUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var authData = JsonSerializer.Deserialize<FirebaseAuthResponse>(responseString);
                // authData contiene la información del usuario autenticado.
                Console.WriteLine($"Usuario autenticado: {authData.email}");
            }
            else
            {
                Console.WriteLine($"Error al autenticar: {responseString}");
            }
        }

        public class FirebaseAuthResponse
        {
            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string localId { get; set; }
        }
    }
}
