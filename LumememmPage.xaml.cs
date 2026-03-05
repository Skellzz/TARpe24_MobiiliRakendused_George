
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
            Text = "..."
        };
        valitudTegevus = new Label
        {
            Text = "..."
        };
        amber = new BoxView
        {
            Color = Color.FromRgb(0, 0, 0),
            WidthRequest = 30,
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        pall1 = new BoxView
        {
            Color = Color.FromRgb(73, 252, 3),
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 100,

        };
        pall2 = new BoxView
        {
            Color = Color.FromRgb(73, 252, 3),
            WidthRequest = 150,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 150,

        };
        pall3 = new BoxView
        {
            Color = Color.FromRgb(73, 252, 3),
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
        picker.Items.Add("Sula");
        picker.Items.Add("Tantsi");

        tegevus = new Button
        {
            Text = "Käivita",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.Black,
            BackgroundColor = Colors.GreenYellow,
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
            Maximum = 180,
            Increment = 10,
            Value = 10,
            HorizontalOptions = LayoutOptions.Center
        };
        kiirus.ValueChanged += Kiirus;



        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 0,
            Children = { valitudTegevus, amber, pall1, pall2, pall3, picker, heledus, kiirus, sulamisKiirusLabel, tegevus },
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;
    }
    private void Kiirus(object? sender, ValueChangedEventArgs e)
    {
        sulamisKiirus = (uint)e.NewValue;
        sulamisKiirusLabel.Text = "Sulamis kiirus: " + sulamisKiirus;
    }

    private async void Tegevus(object? sender, EventArgs e)
    {
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == 0)
        {
            amber.Opacity = 0;
            pall1.Opacity = 0;
            pall2.Opacity = 0;
            pall3.Opacity = 0;
            valitudTegevus.Text = "Peida";
        }
        else if (selectedIndex == 1)
        {
            amber.IsVisible = true;
            amber.Opacity = 1;
            pall1.IsVisible = true;
            pall1.Opacity = 1;
            pall2.IsVisible = true;
            pall2.Opacity = 1;
            pall3.IsVisible = true;
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
            await SulataAsync();
            valitudTegevus.Text = "Sula";
        }
        else if (selectedIndex == 4)
        {
            await AnimateAsync();
        }
    }
    private async Task SulataAsync()
    {
        uint speed = sulamisKiirus > 0 ? sulamisKiirus : 500;
        var parts = new View[] { amber, pall1, pall2, pall3 };
        for (int i = 0; i < 5; i++)
        {
            await Task.WhenAll(parts.Select(p => p.FadeToAsync(p.Opacity - 0.2, speed / 5)));
            await Task.WhenAll(parts.Select(p => p.ScaleToAsync(p.Scale - 0.04, speed / 5)));
        }
        foreach (var part in parts)
        {
            part.IsVisible = false;
            part.Opacity = 1.0;
            part.Scale = 1.0;
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
        double heledus2 = e.NewValue / 100;

        pall1.Opacity = heledus2;
        pall2.Opacity = heledus2;
        pall3.Opacity = heledus2;
    }
}