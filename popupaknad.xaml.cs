using System.Threading.Tasks;

namespace Naidis_TARpe24;

public partial class Popupaknad : ContentPage
{
	public Popupaknad()
	{
		Button alertButton = new Button
		{
			Text = "Mis muusika stiil on agresiivne ja muhe",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center

		};
		alertButton.Clicked += AlertButton_Clicked;

        Button alertListButton = new Button 
		{
			Text = "Valik",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertListButton.Clicked += AlertListButton_Clicked;

        Button alertQuestButton = new Button
        {
            Text = "Küsimus",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        alertQuestButton.Clicked += AlertQuestButton_Clicked;


        Content = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = new Thickness(0, 50, 0, 0),
            Children = { alertButton, alertListButton, alertQuestButton }
        };


        Content = new StackLayout
        {
            Children =
        {alertButton,alertListButton,alertQuestButton }
        };

	}
    private async void AlertButton_Clicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync("Teade", "Teil on uus tead", "OK");
    }

	private async void AlertYesNoButton_Clicked(object? sender, EventArgs e)
	{
		bool result = await DisplayAlertAsync("Kinnitus", "Kas Crust punk või Kpop", "Crust punkl", "Kpop");

		await DisplayAlertAsync("Teade", "Teie valik on: " + (result ? "Crust punk" : "Kpop"), "OK");
	}

	private async void AlertListButton_Clicked(object? sender, EventArgs e)
	{
		string action = await DisplayActionSheetAsync("Mida teha?", "Loobu", "Kustutada", "Tantsida", "Laulda", "Joonestada");
		if (action != null && action != "Loobu")
		{
			await DisplayAlertAsync("Valik", "Sa valisid tegevuse: " + action, "OK");
		}
		
	}
	private async void AlertQuestButton_Clicked(object sender, EventArgs e)
	{
		string result1 = await DisplayPromptAsync("Küsimus", "Kuidas läheb?", placeholder: "Tore!");
		string result2 = await DisplayPromptAsync("Vasta", "millega võrdub 10 X 20", initialValue:
			"0", maxLength: 2, keyboard: Keyboard.Numeric);
	}

}
