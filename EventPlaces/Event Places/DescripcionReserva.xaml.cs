using System.Collections.Generic;
using System.Timers; // Asegúrate de incluir este espacio de nombres
using Microsoft.Maui.Graphics;

namespace EventPlaces.Event_Places;

public partial class DescripcionReserva : ContentPage
{
    private int _currentIndex = 0;
    private System.Timers.Timer _sliderTimer; // Usar System.Timers.Timer explícitamente

    public DescripcionReserva()
    {
        InitializeComponent();

        // Lista de imágenes
        var images = new List<string>
        {
            "imagen1.jpg", // Asegúrate de que estas imágenes existan en tu proyecto
            "imagen2.jpg",
            "imagen3.jpg"
        };

        // Asignar la lista al CarouselView
        ImageCarousel.ItemsSource = images;

        // Configurar el temporizador para avanzar automáticamente
        _sliderTimer = new System.Timers.Timer(3000); // Avanzar cada 3 segundos
        _sliderTimer.Elapsed += OnSliderTimerElapsed;
        _sliderTimer.AutoReset = true; // Repetir automáticamente
        _sliderTimer.Start();
    }



    private void OnSliderTimerElapsed(object sender, ElapsedEventArgs e)
    {
        // Cambiar al siguiente índice
        _currentIndex = (_currentIndex + 1) % ((List<string>)ImageCarousel.ItemsSource).Count;

        // Actualizar el carrusel en el hilo principal
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ImageCarousel.Position = _currentIndex;
        });
    }

    private List<Habitacion> habitaciones = new List<Habitacion> { new Habitacion { Nombre = "Habitación 1", Adultos = 1, Ninos = 0 } };

    private async void OnHuespedesClicked(object sender, EventArgs e)
    {
        int totalPersonas = 0;
        int totalHabitaciones = 0;

        var modalPage = new ContentPage
        {
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Color.FromArgb("#B5D0E2"), Offset = 0.0F },
                new GradientStop { Color = Color.FromArgb("#E8D5D8"), Offset = 1.0F }
            }
            }
        };

        var habitacionLayout = new VerticalStackLayout
        {
            Spacing = 15
        };

        // Método para actualizar el diseño de habitaciones
        void UpdateHabitacionLayout()
        {
            habitacionLayout.Children.Clear();
            totalPersonas = 0;
            totalHabitaciones = habitaciones.Count;

            foreach (var habitacion in habitaciones)
            {
                totalPersonas += habitacion.Adultos + habitacion.Ninos;

                var frame = new Frame
                {
                    BackgroundColor = Colors.White,
                    CornerRadius = 15,
                    Padding = 15,
                    HasShadow = true,
                    Content = new VerticalStackLayout
                    {
                        Spacing = 10,
                        Children =
                    {
                        new Label
                        {
                            Text = habitacion.Nombre,
                            FontSize = 18,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Colors.Black
                        },
                        new Grid
                        {
                            ColumnDefinitions = new ColumnDefinitionCollection
                            {
                                new ColumnDefinition { Width = GridLength.Star },
                                new ColumnDefinition { Width = GridLength.Auto },
                                new ColumnDefinition { Width = GridLength.Auto },
                                new ColumnDefinition { Width = GridLength.Auto }
                            },
                            RowDefinitions = new RowDefinitionCollection
                            {
                                new RowDefinition(),
                                new RowDefinition()
                            },
                            Children =
                            {
                                // Adultos
                                new Label
                                {
                                    Text = "Adultos",
                                    FontSize = 16,
                                    TextColor = Colors.Black,
                                    VerticalOptions = LayoutOptions.Center
                                }.SetGridPosition(0, 0),

                                new Button
                                {
                                    Text = "-",
                                    FontSize = 18,
                                    BackgroundColor = Colors.LightGray,
                                    TextColor = Colors.Black,
                                    CornerRadius = 25,
                                    WidthRequest = 40,
                                    HeightRequest = 40,
                                     Margin = new Thickness(0, 0, 10, 0), // Espacio desde el número
                                    Command = new Command(() =>
                                    {
                                        if (habitacion.Adultos > 1) habitacion.Adultos--;
                                        UpdateHabitacionLayout();
                                    })
                                }.SetGridPosition(0, 1),

                                new Label
                                {
                                    Text = habitacion.Adultos.ToString(),
                                    FontSize = 16,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                    TextColor = Colors.Black
                                }.SetGridPosition(0, 2),

                                new Button
                                {
                                    Text = "+",
                                    FontSize = 18,
                                    BackgroundColor = Colors.LightGray,
                                    TextColor = Colors.Black,
                                    CornerRadius = 25,
                                    WidthRequest = 40,
                                    HeightRequest = 40,
                                     Margin = new Thickness(10, 0, 0, 0), // Espacio desde el número
                                    Command = new Command(() =>
                                    {
                                        habitacion.Adultos++;
                                        UpdateHabitacionLayout();
                                    })
                                }.SetGridPosition(0, 3),

                                // Niños
                                new Label
                                {
                                    Text = "Niños (0 a 17 años)",
                                    FontSize = 16,
                                    TextColor = Colors.Black,
                                    VerticalOptions = LayoutOptions.Center
                                }.SetGridPosition(1, 0),

                                new Button
                                {
                                    Text = "-",
                                    FontSize = 18,
                                    BackgroundColor = Colors.LightGray,
                                    TextColor = Colors.Black,
                                    CornerRadius = 25,
                                    WidthRequest = 40,
                                    HeightRequest = 40,
                                     Margin = new Thickness(0, 0, 10, 0), // Espacio desde el número
                                    Command = new Command(() =>
                                    {
                                        if (habitacion.Ninos > 0) habitacion.Ninos--;
                                        UpdateHabitacionLayout();
                                    })
                                }.SetGridPosition(1, 1),

                                new Label
                                {
                                    Text = habitacion.Ninos.ToString(),
                                    FontSize = 16,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                    TextColor = Colors.Black
                                }.SetGridPosition(1, 2),

                                new Button
                                {
                                    Text = "+",
                                    FontSize = 18,
                                    BackgroundColor = Colors.LightGray,
                                    TextColor = Colors.Black,
                                    CornerRadius = 25,
                                    WidthRequest = 40,
                                    HeightRequest = 40,
                                     Margin = new Thickness(10, 0, 0, 0), // Espacio desde el número
                                    Command = new Command(() =>
                                    {
                                        habitacion.Ninos++;
                                        UpdateHabitacionLayout();
                                    })
                                }.SetGridPosition(1, 3)
                            }
                        }
                    }
                    }
                };

                habitacionLayout.Children.Add(frame);
            }
        }

        UpdateHabitacionLayout();

        modalPage.Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 20,
                Padding = new Thickness(20),
                Children =
            {
                new Label
                {
                    Text = "Huéspedes",
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Black
                },
                habitacionLayout,
                new HorizontalStackLayout
                {
                    Spacing = 10,
                    Children =
                    {
                        new Button
                        {
                            Text = "Agregar Habitación",
                            BackgroundColor = Colors.Green,
                            TextColor = Colors.White,
                            FontAttributes = FontAttributes.Bold,
                            CornerRadius = 25,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Command = new Command(() =>
                            {
                                var nuevaHabitacion = new Habitacion
                                {
                                    Nombre = $"Habitación {habitaciones.Count + 1}",
                                    Adultos = 1,
                                    Ninos = 0
                                };
                                habitaciones.Add(nuevaHabitacion);
                                UpdateHabitacionLayout();
                            })
                        },
                        new Button
                        {
                            Text = "Eliminar Habitación",
                            BackgroundColor = Colors.Red,
                            TextColor = Colors.White,
                            FontAttributes = FontAttributes.Bold,
                            CornerRadius = 25,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Command = new Command(() =>
                            {
                                if (habitaciones.Count > 1)
                                {
                                    habitaciones.RemoveAt(habitaciones.Count - 1);
                                    UpdateHabitacionLayout();
                                }
                            })
                        }
                    }
                },
                new Button
                {
                    Text = "Confirmar",
                    BackgroundColor = Color.FromArgb("#4A90E2"),
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    CornerRadius = 25,
                    HeightRequest = 50,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Command = new Command(async () =>
                    {
                        HuespedesLabel.Text = $"{totalPersonas} Personas, {totalHabitaciones} Habitaciones";
                        await Navigation.PopModalAsync();
                    })
                }
            }
            }
        };

        await Navigation.PushModalAsync(modalPage);
    }

    // Clase auxiliar para manejar las habitaciones
    public class Habitacion
    {
        public string Nombre { get; set; }
        public int Adultos { get; set; }
        public int Ninos { get; set; }
    }

    private async void OnBackButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage1());
    }

    private async void OnFechaButtonClicked(object sender, EventArgs e)
    {
        // Crear un cuadro emergente estilizado
        DatePicker startDatePicker = new DatePicker
        {
            MinimumDate = DateTime.Now,
            MaximumDate = DateTime.Now.AddYears(1),
            Date = DateTime.Now
        };

        DatePicker endDatePicker = new DatePicker
        {
            MinimumDate = DateTime.Now,
            MaximumDate = DateTime.Now.AddYears(1),
            Date = DateTime.Now.AddDays(1)
        };

        var confirmButton = new Button
        {
            Text = "Confirmar",
            BackgroundColor = Color.FromArgb("#0000FF"),
            TextColor = Color.FromArgb("#FFFFFF"),
            FontAttributes = FontAttributes.Bold,
            HeightRequest = 50,
            CornerRadius = 10
        };
        var stack = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(20),
            Children =
    {
        new Label
        {
            Text = "Selecciona la fecha de inicio:",
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Purple // Negro
        },
        startDatePicker,
        new Label
        {
            Text = "Selecciona la fecha de fin:",
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Purple // Negro
        },
        endDatePicker,
        confirmButton
    }
        };

        // Asegúrate de que los DatePickers también usen texto negro
        startDatePicker.TextColor = Colors.Black;
        endDatePicker.TextColor = Colors.Black;



        var modalPage = new ContentPage
        {
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Color.FromArgb("#b5d0e2"), Offset = 0.0F },
                new GradientStop { Color = Color.FromArgb("#e8d5d8"), Offset = 1.0F }



            }
            },
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Spacing = 15,
                    Children =
                {
                    new Label
                    {
                        Text = "Selecciona el rango de fechas",
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center,
                       TextColor = Color.FromArgb("#000000")
                    },
                    stack
                }
                }
            }
        };

        confirmButton.Clicked += async (s, args) =>
        {
            // Validar y actualizar el rango de fechas
            if (startDatePicker.Date > endDatePicker.Date)
            {
                await modalPage.DisplayAlert("Error", "La fecha de inicio no puede ser posterior a la fecha de fin.", "OK");
                return;
            }

            FechaLabel.Text = $"{startDatePicker.Date:dd MMM.} - {endDatePicker.Date:dd MMM.}";
            await Navigation.PopModalAsync();
        };

        await Navigation.PushModalAsync(modalPage);
    }





    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Detener el temporizador al salir de la página
        if (_sliderTimer != null)
        {
            _sliderTimer.Stop();
            _sliderTimer.Dispose();
        }

    }

}


