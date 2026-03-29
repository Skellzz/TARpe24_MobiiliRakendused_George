namespace Naidis_TARpe24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(), new FigurePage(), new ValgusfoorPage(), new DateTimePage(), new StepperSliderPage(), new SliderRgbPage(),new LumememmPage(),new Popupaknad(), new TripsTrapsTrull() };
	public List<string> tekstid = new List<string> { "TextPage", "FigurePage","ValgusfoorPage","DateTimePage","StepperSliderPage","SliderRgbPage","Lumememmpage","PopUpAknad", "TripsTrapsTrull" };

	ScrollView sv;
	VerticalStackLayout vst;
	public StartPage()
	{

		Title = "Avaleht";
		vst = new VerticalStackLayout { BackgroundColor = Color.FromRgb(255, 105, 255) };
		for (int i = 0; i < tekstid.Count; i++)
		{
			Button nupp = new Button
			{
				Text = tekstid[i],
				BackgroundColor = Color.FromRgb(21,101,0),
				TextColor = Color.FromRgb(126,147,129),
				FontFamily = "Socafe 400",
				ZIndex = i
			};
			vst.Add(nupp);
			nupp.Clicked += Nupp_Clicked;
		}
		sv = new ScrollView { Content = vst };
		Content = sv;
	}
	private async void Nupp_Clicked(object? sender, EventArgs e)
	{
		Button btn = sender as Button;
		await Navigation.PushAsync(lehed[btn.ZIndex]);
	}
}