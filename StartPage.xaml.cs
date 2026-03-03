namespace Naidis_TARpe24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(), new FigurePage(), new Timer_Page(), new ValgusfoorPage(), new DateTimePage(), new StepperSliderPage(), new SliderRgbPage(),new LumememmPage() };
	public List<string> tekstid = new List<string> { "Tee lahti TextPage", "Tee lahti FigurePage", "Tee lahti TimerPage","Tee lahti ValgusfoorPage","Tee lahti DateTimePage","Tee lahti StepperSliderPage","Tee lahti SliderRgbPage","Tee lahti Lumememmpage" };
	ScrollView sv;
	VerticalStackLayout vst;
	public StartPage()
	{
		//InitializeComponent();
		Title = "Avaleht";
		vst = new VerticalStackLayout { BackgroundColor = Color.FromRgb(180, 100, 20) };
		for (int i = 0; i < tekstid.Count; i++)
		{
			Button nupp = new Button
			{
				Text = tekstid[i],
				BackgroundColor = Color.FromRgb(20,100,200),
				TextColor = Color.FromRgb(10,20,15),
				FontFamily = "Socafe 400",
				BorderWidth = 10,
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