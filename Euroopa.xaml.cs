using System.Collections.ObjectModel;
using System.Linq;

namespace Naidis_TARpe24;

public partial class Euroopa : ContentPage
{
    public class Riik
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public string Rahvaarv { get; set; }
        public string Lipp { get; set; }
    }
    public ObservableCollection<Riik> riigid { get; set; }

    ListView listView;
    Entry entryNimi, entryPealinn, entryRahvaarv, entryLipp;

    public Euroopa()
    {
        
        riigid = new ObservableCollection<Riik>
        {
            new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv="1331000", Lipp="eesti.jpg" },
            new Riik { Nimi="Soome", Pealinn="Helsingi", Rahvaarv="5541000", Lipp="soome.png" },
            new Riik { Nimi="Poola", Pealinn="Varssavi", Rahvaarv="37950000", Lipp="poola.png" },
            new Riik { Nimi="Itaalia", Pealinn="Rooma", Rahvaarv="59110000", Lipp="itaalia.png" },
            new Riik { Nimi="Taani", Pealinn="Kopenhaagen", Rahvaarv="5831000", Lipp="taani.png" }
        };

        InitializeUI();
    }

    private void InitializeUI()
    {
        Title = "Euroopa Riigid";

        entryNimi = new Entry { Placeholder = "Nimi" };
        entryPealinn = new Entry { Placeholder = "Pealinn" };
        entryRahvaarv = new Entry { Placeholder = "Rahvaarv", Keyboard = Keyboard.Numeric };
        entryLipp = new Entry { Placeholder = "pildi faili nimi" };

        Button btnLisa = new Button { Text = "Lisa riik", BackgroundColor = Colors.GreenYellow, TextColor = Colors.White };
        btnLisa.Clicked += LisaRiik_Clicked;

        Button btnKustuta = new Button { Text = "Kustuta valitud", BackgroundColor = Colors.PaleVioletRed, TextColor = Colors.White };
        btnKustuta.Clicked += KustutaRiik_Clicked;

        listView = new ListView
        {
            ItemsSource = riigid,
            HasUnevenRows = true,
            ItemTemplate = new DataTemplate(() =>
            {
                Image imgLipp = new Image { HeightRequest = 50, WidthRequest = 70, Aspect = Aspect.AspectFit };
                imgLipp.SetBinding(Image.SourceProperty, "Lipp");

                Label lblNimi = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
                lblNimi.SetBinding(Label.TextProperty, "Nimi");

                Label lblPealinn = new Label { FontSize = 14 };
                lblPealinn.SetBinding(Label.TextProperty, "Pealinn");

                var textLayout = new StackLayout { Children = { lblNimi, lblPealinn } };

                var rowLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = 10,
                    Spacing = 15,
                    Children = { imgLipp, textLayout }
                };

                return new ViewCell { View = rowLayout };
            })
        };

        listView.ItemTapped += ListView_ItemTapped;

        Content = new ScrollView
        {
            Content = new StackLayout
            {
                Padding = 20,
                Spacing = 10,
                Children = {
                    new Label { Text = "Uue riigi lisamine", FontSize = 20, HorizontalOptions = LayoutOptions.Center },
                    entryNimi, entryPealinn, entryRahvaarv, entryLipp,
                    btnLisa,
                    new BoxView { HeightRequest = 1, Color = Colors.Gray, Margin = new Thickness(0, 10) },
                    listView,
                    btnKustuta
                }
            }
        };
    }
    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var riik = e.Item as Riik;
        if (riik != null)
        {
            await DisplayAlert(riik.Nimi, $"Pealinn: {riik.Pealinn}\nRahvaarv: {riik.Rahvaarv:N0} inimest", "Sule");

            entryNimi.Text = riik.Nimi;
            entryPealinn.Text = riik.Pealinn;
            entryRahvaarv.Text = riik.Rahvaarv.ToString();
            entryLipp.Text = riik.Lipp;
        }
    }
    private async void LisaRiik_Clicked(object sender, EventArgs e)
    {
        string uusNimi = entryNimi.Text;
        if (string.IsNullOrWhiteSpace(uusNimi)) return;

        bool onOlemas = riigid.Any(r => r.Nimi.Equals(uusNimi, StringComparison.OrdinalIgnoreCase));

        if (onOlemas)
        {
            await DisplayAlert("Viga", "See riik on juba nimekirjas!", "OK");
        }
        else
        {
           // int.TryParse(entryRahvaarv.Text, out int rahv);
            string pildiNimi = string.IsNullOrWhiteSpace(entryLipp.Text) ? "default_flag.png" : entryLipp.Text;

            riigid.Add(new Riik
            {
                Nimi = uusNimi,
                Pealinn = entryPealinn.Text,
                Rahvaarv = entryRahvaarv.Text,
                Lipp = pildiNimi
            });

            ClearEntries();
        }
    }
    private async void KustutaRiik_Clicked(object sender, EventArgs e)
    {
        var valitudRiik = listView.SelectedItem as Riik;
        if (valitudRiik != null)
        {
            bool vastus = await DisplayAlert("Kinnita", $"Kas soovid kustutada riigi {valitudRiik.Nimi}?", "Jah", "Ei");
            if (vastus)
            {
                riigid.Remove(valitudRiik);
                listView.SelectedItem = null;
            }
        }
    }
    private void ClearEntries()
    {
        entryNimi.Text = entryPealinn.Text = entryRahvaarv.Text = entryLipp.Text = "";
    }
}