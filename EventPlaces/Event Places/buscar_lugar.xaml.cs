namespace EventPlaces.Event_Places;

public partial class buscar_lugar : ContentPage
{
    public buscar_lugar()
    {
        InitializeComponent();

        // Configuramos la fecha m�nima para la FechaSalidaPicker al iniciar la p�gina
        FechaSalidaPicker.MinimumDate = FechaLlegadaPicker.Date.AddDays(1);
    }

    // Evento para validar las fechas
    private void OnFechaLlegadaSeleccionada(object sender, DateChangedEventArgs e)
    {
        if (FechaSalidaPicker.Date <= e.NewDate)
        {
            // Ajustamos la fecha m�nima de salida si es menor o igual a la llegada
            FechaSalidaPicker.Date = e.NewDate.AddDays(1);
        }

        // Actualizamos la fecha m�nima permitida para la salida
        FechaSalidaPicker.MinimumDate = e.NewDate.AddDays(1);
    }

    // Evento para manejar el clic en el bot�n o frame de hu�spedes
    private async void OnHuespedesButtonTapped(object sender, EventArgs e)
    {
        // Navegar a la p�gina cant_huespedes
        await Navigation.PushAsync(new cant_huespedes());

    }
    private async void OnBuscarButtonClicked(object sender, EventArgs e)
    {
        // Navegar a la p�gina NewPage1
        await Navigation.PushAsync(new MenuPrincipal());
    }

}
