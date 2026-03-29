using System;
using Microsoft.Maui.Controls;

namespace Naidis_TARpe24;

public partial class TripsTrapsTrull : ContentPage
{
    private string[,] manguväli = new string[3, 3];
    private bool xOnKäigul = true;
    private int käikeTehtud = 0;
    private Grid ruudustik;
    private Label staatuseLabel;

    public TripsTrapsTrull()
    {
        staatuseLabel = new Label
        {
            Text = "Mängija X kord",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 24,
            Margin = new Thickness(0, 20, 0, 10)
        };

        ruudustik = new Grid
        {
            Padding = 10,
            ColumnSpacing = 8,
            RowSpacing = 8,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HeightRequest = 350,
            WidthRequest = 350
        };

        for (int i = 0; i < 3; i++)
        {
            ruudustik.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            ruudustik.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        Button uusMangBtn = new Button
        {
            Text = "Uus mäng",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            Margin = new Thickness(20, 10, 20, 0)
        };
        uusMangBtn.Clicked += (s, e) => AlgistaLaud();

        Button kesAlustabBtn = new Button
        {
            Text = "Kes alustab?",
            Margin = new Thickness(20, 5, 20, 0)
        };
        kesAlustabBtn.Clicked += KesAlustab_Clicked;

        Content = new VerticalStackLayout
        {
            Children = { staatuseLabel, ruudustik, uusMangBtn, kesAlustabBtn }
        };

       
        AlgistaLaud();
    }

    private void AlgistaLaud()
    {
        ruudustik.Children.Clear();
        manguväli = new string[3, 3];
        käikeTehtud = 0;
        xOnKäigul = true;
        staatuseLabel.Text = "Mängija X kord";

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Button ruut = new Button
                {
                    Text = "",
                    FontSize = 32,
                    BackgroundColor = Colors.LightGray
                };

                int rida = r;
                int veerg = c;
                ruut.Clicked += (s, e) => RuuduVajutus(ruut, rida, veerg);

                ruudustik.Add(ruut, veerg, rida);
            }
        }
    }

    private async void RuuduVajutus(Button nupp, int r, int c)
    {
        // kui ruudus on juba tähis ei juhhtu midagi
        if (!string.IsNullOrEmpty(nupp.Text)) return;

        string sümbol = xOnKäigul ? "X" : "O";
        nupp.Text = sümbol;
        nupp.TextColor = xOnKäigul ? Colors.Blue : Colors.Red;
        manguväli[r, c] = sümbol;
        käikeTehtud++;

        if (KontrolliVoitu())
        {
            await DisplayAlert("Võit!", $"Mängija {sümbol} võitis!", "Uus mäng");
            AlgistaLaud();
        }
        else if (käikeTehtud == 9)
        {
            await DisplayAlert("Viik", "Mäng jäi viiki!", "Uus mäng");
            AlgistaLaud();
        }
        else
        {
            xOnKäigul = !xOnKäigul;
            staatuseLabel.Text = $"Mängija {(xOnKäigul ? "X" : "O")} kord";
        }
    }

    private bool KontrolliVoitu()
    {
        for (int i = 0; i < 3; i++)
        {
            //read ja veerud
            if (OnSama(manguväli[i, 0], manguväli[i, 1], manguväli[i, 2])) return true;
            if (OnSama(manguväli[0, i], manguväli[1, i], manguväli[2, i])) return true;
        }
        // diagonaal
        if (OnSama(manguväli[0, 0], manguväli[1, 1], manguväli[2, 2])) return true;
        if (OnSama(manguväli[0, 2], manguväli[1, 1], manguväli[2, 0])) return true;

        return false;
    }

    private bool OnSama(string a, string b, string c)
    {
        return !string.IsNullOrEmpty(a) && a == b && b == c;
    }

    private async void KesAlustab_Clicked(object sender, EventArgs e)
    {
        string valik = await DisplayActionSheet("Kes alustab uut mängu?", "Loobu", null, "Mängija X", "Mängija O", "Suvaline");

        if (valik == "Mängija X") xOnKäigul = true;
        else if (valik == "Mängija O") xOnKäigul = false;
        else if (valik == "Suvaline") xOnKäigul = new Random().Next(2) == 0;

        if (valik != "Loobu" && valik != null) AlgistaLaud();
    }
}
