

namespace Naidis_TARpe24;

public partial class ValgusfoorPage : ContentPage
{
    BoxView boxView1;
	BoxView boxView2;
	BoxView boxView3;
    Button sisse;
    Button valja;
    VerticalStackLayout vsl;
    Label label;
    bool bool1 = false;
    public ValgusfoorPage()
	{
        label = new Label
        {
            Text = "Lülita esmalt foor sisse",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.Chocolate,
        };
        boxView1 = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 150,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 150,
            
        };
        boxView2 = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 150,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 150,
        };
        boxView3 = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 150,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 150,
        };
        TapGestureRecognizer tap1 = new TapGestureRecognizer();
        boxView1.GestureRecognizers.Add(tap1);
        tap1.Tapped += (sender, e) =>
        {
            if(bool1 == true)
            {
                label.Text = "Seisa";
                boxView1.Color = Color.FromRgb(255, 0, 0);
                boxView2.Color = Color.FromRgb(128, 128, 128);
                boxView3.Color = Color.FromRgb(128, 128, 128);
            }
        };
        TapGestureRecognizer tap2 = new TapGestureRecognizer();
        boxView2.GestureRecognizers.Add(tap2);
        tap2.Tapped += (sender, e) =>
        {
            if (bool1 == true)
            {
                label.Text = "Valmistu";
                boxView1.Color = Color.FromRgb(128, 128, 128);
                boxView2.Color = Color.FromRgb(255, 255, 0);
                boxView3.Color = Color.FromRgb(128, 128, 128);
            }
        };
        TapGestureRecognizer tap3 = new TapGestureRecognizer();
        boxView3.GestureRecognizers.Add(tap3);
        tap3.Tapped += (sender, e) =>
        {
            if (bool1 == true)
            {
                label.Text = "Sõida";
                boxView1.Color = Color.FromRgb(128, 128, 128);
                boxView2.Color = Color.FromRgb(128, 128, 128);
                boxView3.Color = Color.FromRgb(0, 255, 0);
            }
        };







        sisse = new Button
        {
            Text = "Sisse",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        valja = new Button
        {
            Text="Välja",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        sisse.Clicked += Sisse;
        valja.Clicked += Valja;

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { label, boxView1, boxView2, boxView3, sisse, valja },
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;

    }
    private void Sisse(object? sender, EventArgs e)
    {
        bool1 = true;
        label.Text = "Vali valgus";
        boxView1.Color = Color.FromRgb(255, 0, 0);
        boxView2.Color = Color.FromRgb(255, 255, 0);
        boxView3.Color = Color.FromRgb(0, 255,0);
    }
    private void Valja(object? sender, EventArgs e)
    {
        bool1 = false;
        label.Text = "Lülita esmalt foor sisse";
        boxView1.Color = Color.FromRgb(128,128,128);
        boxView2.Color = Color.FromRgb(128,128,128);
        boxView3.Color = Color.FromRgb(128,128,128);
    }
}