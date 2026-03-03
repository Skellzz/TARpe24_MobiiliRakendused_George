namespace Naidis_TARpe24;

public partial class DateTimePage : ContentPage
{
	DatePicker datePicker;
	TimePicker timePicker;
	Picker picker;
	Label datetimeLabel;
	AbsoluteLayout al;
	public DateTimePage()
	{
		datePicker = new DatePicker
		{
			MinimumDate = DateTime.Now.AddDays(-15),
			MaximumDate = DateTime.Now.AddDays(15),
			Date = DateTime.Now,
			HorizontalOptions = LayoutOptions.Center,
			Format = "D"
		};
		datePicker.DateSelected += (sender, e) =>
		{
			datetimeLabel.Text = $"Valitud kuupäev:\n{datePicker.Date:D}";
		};
		timePicker = new TimePicker
		{
			Time=DateTime.Now.TimeOfDay,
			HorizontalOptions = LayoutOptions.Center,
			Format = "T"
		};
		timePicker.PropertyChanged += (sender, e) =>
		{
			datetimeLabel.Text = $"Valitud kellaaeg: \n{timePicker.Time:T}";
		};
		datetimeLabel = new Label
		{
			Text="Vali kuupäev või aeg",
			FontSize = 24,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		};
		picker = new Picker
		{
			Title = "Vali värv",
			ItemsSource = new List<String> { "Sinine","Must","Valge"},
			HorizontalOptions = LayoutOptions.Center,
		};
		picker.SelectedIndexChanged += (sender, e) =>
		{
			switch (picker.SelectedIndex)
			{
				case 0:
					this.BackgroundColor = Colors.LightBlue;
					break;
				case 1:
					this.BackgroundColor = Colors.DarkGray;
					break;
				case 2:
					this.BackgroundColor = Colors.White;
					break;
			}
		};
		al = new AbsoluteLayout { Children = { datePicker, timePicker, datetimeLabel,picker } };
		List<View> controls = new List<View> { datePicker, timePicker, datetimeLabel,picker };
		for (int i = 0; i < controls.Count; i++)
		{
			double yKoht = 0.2 + i * 0.2;
			AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(controls[i], Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);

		}
		Content = al;
	}
}