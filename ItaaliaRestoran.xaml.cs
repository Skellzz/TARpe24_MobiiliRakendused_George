using Kolm_keelt.Resources.Localization;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Naidis_TARpe24;

public partial class ItaaliaRestoran : ContentPage
{
    public class Toit
    {
        public string Nimi { get; set; }
        public string Kirjeldus { get; set; }
        public string PiltUrl { get; set; }
        public string Koostisosad { get; set; }
    }

    private CarouselView carouselView;
    private ObservableCollection<Toit> toidud;
    private int currentPosition = 0;

    public ItaaliaRestoran()
    {
        // Pealkiri ressursside failist (AppResources.resx)
        Title = AppResources.RestoranTitle;

        // 2. Andmestik (Itaalia köök kolmes keeles)
        toidud = new ObservableCollection<Toit>
        {
            new Toit {
                Nimi = "Pasta Carbonara",
                Kirjeldus = "Classic Roman dish / Klassikaline Rooma roog / Piatto tipico romano",
                PiltUrl = "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/recipe-image-legacy-id-1001491_11-2e0ca5a.jpg",
                Koostisosad = "Guanciale, Pecorino Romano, muna, pipar"
            },
            new Toit {
                Nimi = "Pizza Margherita",
                Kirjeldus = "Simple and delicious / Lihtne ja maitsev / Semplice e deliziosa",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a3/Eq_it-na_pizza-margherita_sep2005_smn2.jpg",
                Koostisosad = "Pomodoro, Mozzarella, basilico, olio d'oliva"
            },
            new Toit {
                Nimi = "Tiramisu",
                Kirjeldus = "Coffee dessert / Kohvi-magustoit / Dolce al caffè",
                PiltUrl = "https://www.flavoursholidays.co.uk/wp-content/uploads/2020/07/Tiramisu.jpg",
                Koostisosad = "Mascarpone, savoiardi, caffè, cacao"
            },
            new Toit {
                Nimi = "Risotto alla Milanese",
                Kirjeldus = "Creamy saffron rice / Kreemine safraniriis / Riso cremoso allo zafferano",
                PiltUrl = "https://www.vincenzosplate.com/wp-content/uploads/2021/04/Risotto-alla-Milanese-Step-by-step-Recipe.jpg",
                Koostisosad = "Riso Arborio, zafferano, brodo, parmigiano"
            },
            new Toit {
                Nimi = "Lasagne alla Bolognese",
                Kirjeldus = "Layered pasta / Kihiline pasta / Pasta a strati",
                PiltUrl = "https://www.modernhoney.com/wp-content/uploads/2019/08/The-Best-Lasagna-Recipe-main.jpg",
                Koostisosad = "Ragù, besciamella, sfoglia, formaggio"
            }
        };

        InitializeUI();
        StartAutoScroll();
    }

    private void InitializeUI()
    {
        // 3. CarouselView seadistamine
        carouselView = new CarouselView
        {
            ItemsSource = toidud,
            HeightRequest = 450,
            PeekAreaInsets = new Thickness(30),
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 25,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(10),
                    Content = CreateCardLayout()
                };

                // Interaktiivsus: Klikk kaardil
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += async (s, e) =>
                {
                    var valitudToit = (Toit)((VisualElement)s).BindingContext;
                    // Kuvab infot kasutades tõlgitud pealkirju
                    await DisplayAlert(valitudToit.Nimi,
                        $"{AppResources.Ingredients}: {valitudToit.Koostisosad}",
                        AppResources.Close);
                };
                frame.GestureRecognizers.Add(tapGesture);

                return frame;
            })
        };

        // 4. IndicatorView (täpid all)
        var indicatorView = new IndicatorView
        {
            HorizontalOptions = LayoutOptions.Center,
            IndicatorColor = Colors.LightGray,
            SelectedIndicatorColor = Colors.White,
            Margin = new Thickness(0, 10)
        };
        carouselView.IndicatorView = indicatorView;

        // 5. Keele muutmise nupud (EN, ET, IT)
        var btnEn = new Button { Text = "EN 🇬🇧", Margin = 5, BackgroundColor = Colors.White.WithAlpha(0.2f), TextColor = Colors.White };
        btnEn.Clicked += (s, e) => ChangeLanguage("en");

        var btnEt = new Button { Text = "ET 🇪🇪", Margin = 5, BackgroundColor = Colors.White.WithAlpha(0.2f), TextColor = Colors.White };
        btnEt.Clicked += (s, e) => ChangeLanguage("et");

        var btnIt = new Button { Text = "IT 🇮🇹", Margin = 5, BackgroundColor = Colors.White.WithAlpha(0.2f), TextColor = Colors.White };
        btnIt.Clicked += (s, e) => ChangeLanguage("it");

        var langStack = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Children = { btnEn, btnEt, btnIt }
        };

        // Kokkupanek gradient-taustaga (Itaalia lipu värvide meenutuseks punakas-oranž)
        Content = new Grid
        {
            Background = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#11998e"), 0), // Rohekas (Itaalia lipp)
                    new GradientStop(Color.FromArgb("#38ef7d"), 1)
                }
            },
            Children = {
                new VerticalStackLayout {
                    Padding = 20,
                    Children = {
                        new Label {
                            Text = AppResources.RestoranTitle,
                            FontSize = 28,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Center,
                            TextColor = Colors.White,
                            Margin = new Thickness(0,0,0,10)
                        },
                        carouselView,
                        indicatorView,
                        langStack
                    }
                }
            }
        };
    }

    private View CreateCardLayout()
    {
        var grid = new Grid();

        var image = new Image { Aspect = Aspect.AspectFill };
        image.SetBinding(Image.SourceProperty, "PiltUrl");

        // Tume varjustus teksti all, et see oleks loetav
        var overlay = new BoxView
        {
            Background = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection {
                    new GradientStop(Colors.Transparent, 0.4f),
                    new GradientStop(Colors.Black.WithAlpha(0.9f), 1)
                }
            }
        };

        var labelStack = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.End,
            Padding = 20,
            Children = {
                new Label { TextColor = Colors.White, FontSize = 24, FontAttributes = FontAttributes.Bold }
                    .Bind(Label.TextProperty, "Nimi"),
                new Label { TextColor = Colors.LightGray, FontSize = 14, FontAttributes = FontAttributes.Italic }
                    .Bind(Label.TextProperty, "Kirjeldus")
            }
        };

        grid.Children.Add(image);
        grid.Children.Add(overlay);
        grid.Children.Add(labelStack);

        return grid;
    }

    // 6. Automaatne kerimine
    private void StartAutoScroll()
    {
        Device.StartTimer(TimeSpan.FromSeconds(4), () =>
        {
            if (carouselView == null) return false;
            currentPosition = (carouselView.Position + 1) % toidud.Count;
            carouselView.Position = currentPosition;
            return true;
        });
    }

    // 7. Keele vahetamise loogika
    private void ChangeLanguage(string langCode)
    {
        var culture = new CultureInfo(langCode);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        AppResources.Culture = culture;

        // Värskendab lehte, et uue keele ressursid laetaks
        Application.Current.MainPage = new NavigationPage(new ItaaliaRestoran());
    }
}