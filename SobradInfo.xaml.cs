using Microsoft.Maui.ApplicationModel.Communication;

namespace Naidis_TARpe24;

public partial class SobradInfo : ContentPage
{
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

    private async void Saada_sms_Clicked(object sender, EventArgs e)
    {
      
        string phone = entry_phone.Text;

        if (string.IsNullOrWhiteSpace(phone))
        {
            await DisplayAlert("Hoiatus", "Telefoni number on puudu!", "OK");
            return;
        }

        var random = new Random();
        string messageBody = tervitused[random.Next(tervitused.Count)];

        try
        {
          
            var sms = new Microsoft.Maui.ApplicationModel.Communication.SmsMessage(messageBody, phone);
            await Microsoft.Maui.ApplicationModel.Communication.Sms.Default.ComposeAsync(sms);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", "SMS-i avamine feilis.", "OK");
        }
    }

    private async void Saada_email_Clicked(object sender, EventArgs e)
    {
        
        string emailAddr = entry_email.Text;

        if (string.IsNullOrWhiteSpace(emailAddr))
        {
            await DisplayAlert("Hoiatus", "E-mail on puudu!", "OK");
            return;
        }

        var random = new Random();
        string messageBody = tervitused[random.Next(tervitused.Count)];

        var email = new EmailMessage
        {
            Subject = "Tervitus!",
            Body = messageBody,
            To = new List<string> { emailAddr }
        };

        try
        {
            if (Email.Default.IsComposeSupported)
            {
                await Email.Default.ComposeAsync(email);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", "E-maili avamine feilis.", "OK");
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
