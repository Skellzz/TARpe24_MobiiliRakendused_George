using System;
using Microsoft.Maui.Controls;

namespace Naidis_TARpe24;

public partial class Popupaknad : ContentPage
{
    public Popupaknad()
    {

        // UUS NUPP: Korrutustabeli test
        Button mathTestButton = new Button
        {
            Text = "Alusta korrutustabeli testi",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.LightBlue,
            TextColor = Colors.Black
        };
        mathTestButton.Clicked += MathTestButton_Clicked;

        Content = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = new Thickness(0, 50, 0, 0),
            Children = { mathTestButton }
        };
    }

    private async void MathTestButton_Clicked(object? sender, EventArgs e)
    {
        Random random = new Random();
        int oigeidVastuseid = 0;
        int kusimusteArv = 10;

        for (int i = 1; i <= kusimusteArv; i++)
        {
            int arv1 = random.Next(1, 11); // Arvud 1-10
            int arv2 = random.Next(1, 11);
            int oigeVastus = arv1 * arv2;

            // Küsime kasutajalt vastust
            string vastus = await DisplayPromptAsync(
                $"Küsimus {i}/{kusimusteArv}",
                $"Palju on {arv1} x {arv2}?",
                initialValue: "",
                keyboard: Keyboard.Numeric);

            // Kontrollime, kas vastus on korrektne
            if (int.TryParse(vastus, out int kasutajaVastus))
            {
                if (kasutajaVastus == oigeVastus)
                {
                    oigeidVastuseid++;
                }
            }

            if (vastus == null) break;
        }
        await DisplayAlert("Test on läbis", $"Sinu tulemus: {oigeidVastuseid} / {kusimusteArv}", "OK");
    }
}
