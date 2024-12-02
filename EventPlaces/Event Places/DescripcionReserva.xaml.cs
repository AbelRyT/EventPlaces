
using Common;
using EventPlaces.Pages;

namespace EventPlaces.Event_Places;

public partial class DescripcionReserva : ContentPage
{
    public LugarDto LugarSeleccionado { get; set; }

    public DescripcionReserva(LugarDto lugar)
    {
        InitializeComponent();
        LugarSeleccionado = lugar;
        BindingContext = LugarSeleccionado;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
       
            await Navigation.PushAsync(new ReservacionPage(LugarSeleccionado));
        
    }
}