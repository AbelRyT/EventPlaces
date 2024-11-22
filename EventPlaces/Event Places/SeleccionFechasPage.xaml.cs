namespace EventPlaces.Event_Places;

public partial class SeleccionFechasPage : ContentPage
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public SeleccionFechasPage()
    {
        InitializeComponent();
        BindingContext = this;
        startDatePicker.MinimumDate = DateTime.Now;
    }

    // Al seleccionar la fecha de inicio
    private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        StartDate = e.NewDate;
        startDateLabel.Text = StartDate.ToString("dd MMMM yyyy");
    }

    // Al seleccionar la fecha de fin
    private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndDate = e.NewDate;
        endDateLabel.Text = EndDate.ToString("dd MMMM yyyy");
    }

    // Al confirmar las fechas
    private async void OnConfirmDatesClicked(object sender, EventArgs e)
    {
        if (StartDate >= EndDate)
        {
            await DisplayAlert("Error", "La fecha de fin debe ser posterior a la fecha de inicio.", "OK");
            return;
        }

        // Mostrar las fechas seleccionadas (o puedes usar este código para continuar el proceso de la reserva)
        await DisplayAlert("Fechas Confirmadas", $"Reserva del {StartDate:dd MMM yyyy} al {EndDate:dd MMM yyyy}", "OK");

        await Navigation.PushAsync(new DescripcionReserva());
    }
}
