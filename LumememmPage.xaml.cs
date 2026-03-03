using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.GTKSpecific;
using BoxView = Microsoft.Maui.Controls.BoxView;

namespace Naidis_TARpe24;

public partial class LumememmPage : ContentPage
{
    Label valitudTegevus;
    Label sulamisKiirusLabel;
    BoxView amber;
    BoxView pall1;
	BoxView pall2;
	BoxView pall3;
	Picker picker;
    Button tegevus;
    Slider heledus;
    Stepper kiirus;
    uint sulamisKiirus;
    Random rnd = new Random();
	VerticalStackLayout vsl;
    public LumememmPage()
	{
        sulamisKiirusLabel = new Label
        {
            Text="..."
        };
        valitudTegevus = new Label
        {
            Text="..."
        };
        amber = new BoxView
        {
            Color = Color.FromRgb(150,150,150),
            WidthRequest = 50,
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        pall1 = new BoxView
        {
            Color = Color.FromRgb(180, 180, 180),
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 100,

        };
        pall2 = new BoxView
        {
            Color = Color.FromRgb(180, 180, 180),
            WidthRequest = 150,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 150,

        };
        pall3 = new BoxView
        {
            Color = Color.FromRgb(50, 50, 50),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 200,

        };
        picker = new Picker { Title = "Vali tegevus" };
        picker.Items.Add("Peida");
        picker.Items.Add("Näita");
        picker.Items.Add("Muuda värvi");
        picker.Items.Add("Sulata");
        picker.Items.Add("Tantsi");

        tegevus = new Button
        {
            Text = "Käivita tegevus",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
            WidthRequest = 200
        };
        tegevus.Clicked += Tegevus;

        heledus = new Slider
        {
            Minimum = 0,
            Maximum = 100,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300,
        };
        heledus.ValueChanged += Heledus;

        kiirus = new Stepper
        {
            Minimum = 0,
            Maximum = 1000,
            Increment = 100,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        kiirus.ValueChanged += Kiirus;



        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 0,
            Children = { valitudTegevus,amber,pall1,pall2,pall3,picker,tegevus,heledus,kiirus, sulamisKiirusLabel },
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;
    }
    private void Kiirus(object? sender, ValueChangedEventArgs e)
    {
        kiirus.Value = sulamisKiirus;
        sulamisKiirusLabel.Text ="Sulamis kiirus: "+ Convert.ToString(sulamisKiirus);
    }

    private async void Tegevus(object? sender, EventArgs e)
    {
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == 0)
        {
            pall1.Opacity = 0;
            pall2.Opacity = 0;
            pall3.Opacity = 0;
            valitudTegevus.Text = "Peida";
        }
        else if (selectedIndex == 1)
        {
            pall1.Opacity = 1;
            pall2.Opacity = 1;
            pall3.Opacity = 1;
            valitudTegevus.Text = "Näita";
        }
        else if (selectedIndex == 2)
        {
            int r = rnd.Next(256);
            int g = rnd.Next(256);
            int b = rnd.Next(256);
            pall1.Color = Color.FromRgb(r, g, b);
            pall2.Color = Color.FromRgb(r, g, b);
            pall3.Color = Color.FromRgb(r, g, b);
            valitudTegevus.Text = "Muuda värvi";

        }
        else if (selectedIndex == 3)
        {
            pall1.FadeToAsync(0,sulamisKiirus);
            pall2.FadeToAsync(0,sulamisKiirus);
            pall3.FadeToAsync(0,sulamisKiirus);
            valitudTegevus.Text = "Sulata";
        }
        else if (selectedIndex == 4)
        {
            await AnimateAsync();
        }
        }
        async Task AnimateAsync()
        {
            await Task.WhenAll(
                amber.TranslateToAsync(40, 0, 250),
                pall1.TranslateToAsync(40, 0, 250),
                pall2.TranslateToAsync(40, 0, 250),
                pall3.TranslateToAsync(40, 0, 250)
            );

            await Task.WhenAll(
                amber.TranslateToAsync(0, 0, 250),
                pall1.TranslateToAsync(0, 0, 250),
                pall2.TranslateToAsync(0, 0, 250),
                pall3.TranslateToAsync(0, 0, 250)
            );
        }
    private void Heledus(object? sender, ValueChangedEventArgs e)
    {
        double heledus2 = heledus.Value / 100;
        
        heledus2 = pall1.Opacity;
        heledus2 = pall2.Opacity;
        heledus2 = pall3.Opacity;
    }
    }
