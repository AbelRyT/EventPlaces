using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventPlaces.Event_Places
{
    public partial class cant_huespedes : ContentPage
    {
        public ObservableCollection<Habitacion> Habitaciones { get; set; }

        public cant_huespedes()
        {
            InitializeComponent();

            Habitaciones = new ObservableCollection<Habitacion>
            {
                new Habitacion { Nombre = "Habitación 1", Adultos = 2, Ninos = 0 }
            };

            BindingContext = this;
        }

        private async void OnBuscarButtonClicked(object sender, EventArgs e)
        {
            // Ejemplo de navegación
            await Navigation.PushAsync(new buscar_lugar());
        }

        private void OnAgregarHabitacionClicked(object sender, EventArgs e)
        {
            int numero = Habitaciones.Count + 1;
            Habitaciones.Add(new Habitacion { Nombre = $"Habitación {numero}", Adultos = 1, Ninos = 0 });
        }

        private void OnEliminarHabitacionClicked(object sender, EventArgs e)
        {
            if (Habitaciones.Count > 1)
                Habitaciones.RemoveAt(Habitaciones.Count - 1);
        }
    }


    public class Habitacion : INotifyPropertyChanged
    {
        private int _adultos;
        private int _ninos;

        public string Nombre { get; set; }

        public int Adultos
        {
            get => _adultos;
            set
            {
                _adultos = value;
                OnPropertyChanged();
            }
        }

        public int Ninos
        {
            get => _ninos;
            set
            {
                _ninos = value;
                OnPropertyChanged();
            }
        }

        public Command IncrementarAdultosCommand => new Command(() => Adultos++);
        public Command DecrementarAdultosCommand => new Command(() =>
        {
            if (Adultos > 1) Adultos--;
        });

        public Command IncrementarNinosCommand => new Command(() => Ninos++);
        public Command DecrementarNinosCommand => new Command(() =>
        {
            if (Ninos > 0) Ninos--;
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }



}
