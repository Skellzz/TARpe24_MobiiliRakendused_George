namespace Naidis_TARpe24;

public partial class SliderRgbPage : ContentPage
{
    Label label;
    Label labelR;
    Label labelG;
    Label labelB;
    Stepper stepper;
    Slider slider1;
    Slider slider2;
    Slider slider3;
    Button juhuslik;
    Random rnd = new Random();
    AbsoluteLayout al;
    public SliderRgbPage()
    {
        label = new Label
        {
            Text = "",
        };
        labelR = new Label
        {
            Text = "",
            BackgroundColor = Colors.Transparent,
        };
        labelG = new Label
        {
            Text = "",
            BackgroundColor = Colors.Transparent,
        };
        labelB = new Label
        {
            Text = "",
            BackgroundColor = Colors.Transparent,
        };
        slider1 = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300,
        };
        slider2 = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300,
        };
        slider3 = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300,
        };
        slider1.ValueChanged += Slider_Color;
        slider2.ValueChanged += Slider_Color;
        slider3.ValueChanged += Slider_Color;
        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 200,
            Increment = 5,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += Stepper_Size;
        juhuslik = new Button
        {
            FontSize = 28,
            FontFamily = "Luffio",
            CornerRadius = 10,
            HeightRequest = 50,
            WidthRequest = 200
        };
        juhuslik.Clicked += juhuslikVarv;

        al = new AbsoluteLayout();

        var controls = new List<View> { label, labelR, labelG, labelB, slider1, slider2, slider3, stepper, juhuslik };

        foreach (var control in controls)
            al.Children.Add(control);

        for (int i = 0; i < controls.Count; i++)
        {
            double yKoht = 0.1 + i * 0.1;
            AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 100, 60));
            AbsoluteLayout.SetLayoutFlags(controls[i], Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
        }
        Content = al;
    }
    private void Slider_Color(object? sender, ValueChangedEventArgs e)
    {
        double r = slider1.Value / 255.0;
        double g = slider2.Value / 255.0;
        double b = slider3.Value / 255.0;
        label.BackgroundColor = Color.FromRgb(r, g, b);
        labelR.BackgroundColor = Color.FromRgb(r, 0, 0);
        labelG.BackgroundColor = Color.FromRgb(0, g, 0);
        labelB.BackgroundColor = Color.FromRgb(0, 0, b);
    }
    private void Stepper_Size(object? sender, ValueChangedEventArgs e)
    {
        double size = stepper.Value;
        label.WidthRequest = size;
    }
    private void juhuslikVarv(object? sender, EventArgs e)
    {
        int r = rnd.Next(256);
        int g = rnd.Next(256);
        int b = rnd.Next(256);
        label.BackgroundColor = Color.FromRgb(r, g, b);
        labelR.BackgroundColor = Color.FromRgb(r, 0, 0);
        labelG.BackgroundColor = Color.FromRgb(0, g, 0);
        labelB.BackgroundColor = Color.FromRgb(0, 0, b);
    }
}