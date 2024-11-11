using EventPlaces.Event_Places;

namespace EventPlaces.Pages;

public partial class EditarReservaPage : ContentPage
{
	public EditarReservaPage()
	{
		InitializeComponent();
	}

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Validar las entradas
        if (fechaInicioPicker.Date > fechaFinPicker.Date)
        {
            await DisplayAlert("Error", "La fecha de inicio no puede ser posterior a la fecha de fin", "OK");
            return;
        }

        if (string.IsNullOrEmpty(huespedesEntry.Text) || !int.TryParse(huespedesEntry.Text, out int numeroHuespedes) || numeroHuespedes <= 0)
        {
            await DisplayAlert("Error", "Introduce un n�mero v�lido de hu�spedes", "OK");
            return;
        }

        // Guardar los cambios (implementa la l�gica para actualizar la reserva)
        await DisplayAlert("�xito", "Reserva actualizada correctamente", "OK");
        await Navigation.PushAsync(new Reservados());// Volver a la pantalla anterior
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        // L�gica para cancelar la edici�n
        bool confirm = await DisplayAlert("Confirmaci�n", "�Est�s seguro de que quieres cancelar los cambios?", "S�", "No");
        if (confirm)
        {
            await Navigation.PushAsync(new Reservados()); // Volver a la pantalla anterior
        }
    }
}