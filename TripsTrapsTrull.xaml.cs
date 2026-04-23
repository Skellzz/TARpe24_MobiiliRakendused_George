using System;
using Microsoft.Maui.Controls;

namespace Naidis_TARpe24;

public partial class TripsTrapsTrull : ContentPage
{
    private string[,] box = new string[3, 3];
    private bool Xplayer = true;
    private int MovesMade = 0;
    private Grid cube;
    private Label StateLabel;

    public TripsTrapsTrull()
    {
        StateLabel = new Label
        {
            Text = "Mängija X kord",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 24,
            Margin = new Thickness(0, 20, 0, 10)
        };

        cube = new Grid
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
            cube.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            cube.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
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
            Children = { StateLabel, cube, uusMangBtn, kesAlustabBtn }
        };

       
        AlgistaLaud();
    }

    private void AlgistaLaud()
    {
        cube.Children.Clear();
        box = new string[3, 3];
        MovesMade = 0;
        Xplayer = true;
        StateLabel.Text = "Mängija X kord";

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Button ruut = new Button
                {
                    Text = "",
                    FontSize = 32 ,
                    BackgroundColor = Colors.Black
                };

                int rida = r;
                int veerg = c;
                ruut.Clicked += (s, e) => RuuduVajutus(ruut, rida, veerg);

                cube.Add(ruut, veerg, rida);
            }
        }
    }

    private async void RuuduVajutus(Button nupp, int r, int c)
    {
        // kui ruudus on juba tähis ei juhhtu midagi
        if (!string.IsNullOrEmpty(nupp.Text)) return;

        string player = Xplayer ? "X" : "O";
        nupp.Text = player;
        nupp.TextColor = Xplayer ? Colors.Blue : Colors.Red;
        box[r, c] = player;
        MovesMade++;

        if (KontrolliVoitu())
        {
            await DisplayAlert("Võit!", $"Mängija {player} võitis!", "Uus mäng");
            AlgistaLaud();
        }
        else if (MovesMade == 9)
        {
            await DisplayAlert("Viik", "Mäng jäi viiki!", "Uus mäng");
            AlgistaLaud();
        }
        else
        {
            Xplayer = !Xplayer;
            StateLabel.Text = $"Mängija {(Xplayer ? "X" : "O")} kord";
        }
    }

    private bool KontrolliVoitu()
    {
        for (int i = 0; i < 3; i++)
        {
            //read ja veerud
            if (OnSama(box[i, 0], box[i, 1], box[i, 2])) return true;
            if (OnSama(box[0, i], box[1, i], box[2, i])) return true;
        }
        // diagonaal
        if (OnSama(box[0, 0], box[1, 1], box[2, 2])) return true;
        if (OnSama(box[0, 2], box[1, 1], box[2, 0])) return true;

        return false;
    }

    private bool OnSama(string a, string b, string c)
    {
        return !string.IsNullOrEmpty(a) && a == b && b == c;
    }

    private async void KesAlustab_Clicked(object sender, EventArgs e)
    {
        string valik = await DisplayActionSheet("Kes alustab uut mängu?", "Loobu", null, "Mängija X", "Mängija O", "Suvaline");

        if (valik == "Mängija X") Xplayer = true;
        else if (valik == "Mängija O") Xplayer = true;
        else if (valik == "Suvaline") Xplayer = new Random().Next(2) == 0;

        if (valik != "Loobu" && valik != null) AlgistaLaud();
    }
}
