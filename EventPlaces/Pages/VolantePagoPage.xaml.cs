using Common;

namespace EventPlaces.Pages;

public partial class VolantePagoPage : ContentPage
{
	public readonly ReservacionDto Reservacion;
	public VolantePagoPage(ReservacionDto reservacionDto)
	{
		InitializeComponent();

        Reservacion = reservacionDto;
        BindingContext = Reservacion;
    }

    private async void OnCerrarClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopModalAsync(); // Cierra el modal
    }
}