using Microsoft.Maui.ApplicationModel.Communication;

namespace Naidis_TARpe24;

public partial class SobradInfo : ContentPage
{
    // Juhendi järgi tervituste nimekiri
    List<string> tervitused = new List<string>
    {
        "Palju õnne sünnipäevaks!",
        "Kuidas sul läheb?",
        "Oled täna väga tubli!",
        "Häid pühi sulle ja su perele!",
        "Millal me jälle kokku saame?"
    };

    public SobradInfo()
    {
        InitializeComponent();
    }

    // Pildil olev SMS-i saatmise meetod (kohandatud MAUI jaoks)
    private async void Saada_sms_Clicked(object sender, EventArgs e)
    {
        // Eeldame, et sul on XAML-is Entry nimega: email_phone
        string phone = email_phone.Text;

        var random = new Random();
        string messageBody = tervitused[random.Next(tervitused.Count)];

        if (!string.IsNullOrWhiteSpace(phone))
        {
            try
            {
                var sms = new SmsMessage(messageBody, phone);
                await Sms.Default.ComposeAsync(sms);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", "SMS-i saatmine ebaõnnestus või pole toetatud.", "OK");
            }
        }
    }

    private async void Saada_email_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(email_phone.Text)) return;

        var random = new Random();
        string messageBody = tervitused[random.Next(tervitused.Count)];

        var email = new EmailMessage
        {
            Subject = "Tervitus!",
            Body = messageBody,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string> { email_phone.Text }
        };

        try
        { 
            if (Email.Default.IsComposeSupported)
            {
                await Email.Default.ComposeAsync(email);
            }
            else
            {
                await DisplayAlert("Viga", "E-maili saatmine pole selles seadmes toetatud", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", "E-maili avamine ebaõnnestus.", "OK");
        }
    }

    private async void OnTeeFotoClicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
}
