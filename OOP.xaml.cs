using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Naidis_TARpe24;

public partial class OOP : ContentPage
{
    private GameLogic _game;
    private Player _player;
    private Random _rng = new Random();

    public OOP()
    {
        InitializeComponent();

        _player = new Player("Gamer");
        _game = new GameLogic(_player);

        ThemePicker.ItemsSource = new List<Theme>
        {
            new Theme("punine", Colors.Red, Colors.Black, "OpenSansRegular"),
            new Theme("Black", Colors.Black, Colors.White, "OpenSansRegular"),
            new Theme("Gren", Colors.GreenYellow, Colors.DarkGreen, "OpenSansRegular")
        };

        _game.OnSpawnImage += SpawnImage;
        _game.OnGameEnded += GameEnded;
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem is Theme selectedTheme)
        {
            selectedTheme.Apply(this);
            ScoreLabel.Text = "Punktid: 0";
            _game.StartGame(20);
        }
        else
        {
            DisplayAlert("Viga", "Vali teema!", "OK");
        }
    }

    private void SpawnImage(string imageName)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            TargetImage.Source = imageName;
            TargetImage.IsVisible = true;

            double maxX = Math.Max(GameField.Width - 100, 0);
            double maxY = Math.Max(GameField.Height - 100, 0);

            double x = _rng.NextDouble() * maxX;
            double y = _rng.NextDouble() * maxY;

            AbsoluteLayout.SetLayoutBounds(TargetImage, new Rect(x, y, 100, 100));

            TargetImage.Scale = 0.2;
            await TargetImage.ScaleTo(3.0, 250, Easing.SpringOut);
        });
    }

    private void OnImageTapped(object sender, EventArgs e)
    {
        _player.AddPoint();
        ScoreLabel.Text = $"Punktid: {_player.Score}";
        TargetImage.IsVisible = false;
    }

    private void GameEnded()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            TargetImage.IsVisible = false;
            await DisplayAlert("Mäng läbi!", $"Skoor: {_player.Score}", "OK");
        });
    }
}

public class Theme
{
    public string Name { get; set; }
    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }
    public string FontFamily { get; set; }

    public Theme(string name, Color bg, Color text, string font)
    {
        Name = name; BackgroundColor = bg; TextColor = text; FontFamily = font;
    }

    public void Apply(ContentPage page)
    {
        page.BackgroundColor = BackgroundColor;
        if (Microsoft.Maui.Controls.Application.Current != null)
        {
            Microsoft.Maui.Controls.Application.Current.Resources["DynamicTextColor"] = TextColor;
            Microsoft.Maui.Controls.Application.Current.Resources["DynamicFont"] = FontFamily;
        }
    }
    public override string ToString() => Name;
}

public class Player
{
    public string Name { get; set; }
    private int _score;
    public int Score => _score;
    public Player(string name) => Name = name;
    public void AddPoint() => _score++;
    public void ResetScore() => _score = 0;
}

public class GameLogic
{
    private Player _player;
    private bool _isActive;
    public event Action<string> OnSpawnImage;
    public event Action OnGameEnded;

    private string[] _images = { "locked_in.png", "hello.png", "hi.png", "geeked.png" };
    private Random _rng = new Random();

    public GameLogic(Player player) => _player = player;

    public async void StartGame(int duration)
    {
        if (_isActive) return;
        _isActive = true;
        _player.ResetScore();

        for (int i = 0; i < duration; i++)
        {
            if (!_isActive) break;
            string img = _images[_rng.Next(_images.Length)];
            OnSpawnImage?.Invoke(img);
            await Task.Delay(1000);
        }

        _isActive = false;
        OnGameEnded?.Invoke();
    }
    public void Stop() => _isActive = false;
}