namespace SecureDataEntryForm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                StatusLabel.Text = "✓ Form submitted successfully!";
                StatusLabel.TextColor = Colors.Green;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert( "Success", "Your secure data has been submitted.\n\n" + $"Last 4 digits of card: {GetLast4Digits(CardNumberEntry.Value?.ToString())}\n" + $"Phone: {PhoneEntry.Value}\n" + $"Cardholder: {CardholderEntry.Text}", "OK");
                });
            }
            else
            {
                StatusLabel.Text = "✗ Please fill in all required fields";
                StatusLabel.TextColor = Colors.Red;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            CardNumberEntry.Value = string.Empty;
            CardholderEntry.Text = string.Empty;
            ExpiryEntry.Value = string.Empty;
            CVVEntry.Value = string.Empty;
            PhoneEntry.Value = string.Empty;
            EmailEntry.Text = string.Empty;
            SSNEntry.Value = string.Empty;
            ZIPEntry.Value = string.Empty;
            StatusLabel.Text = string.Empty;
        }

        private bool ValidateForm()
        {
           return !string.IsNullOrWhiteSpace(CardNumberEntry.Value?.ToString()) &&
                   !string.IsNullOrWhiteSpace(CardholderEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ExpiryEntry.Value?.ToString()) &&
                   !string.IsNullOrWhiteSpace(CVVEntry.Value?.ToString()) &&
                   !string.IsNullOrWhiteSpace(PhoneEntry.Value?.ToString()) &&
                   !string.IsNullOrWhiteSpace(EmailEntry.Text);
        }

        private string GetLast4Digits(string value)
        {
            string digitsOnly = value?.Replace(" ", "") ?? string.Empty;

            return digitsOnly.Length >= 4 ? digitsOnly.Substring(digitsOnly.Length - 4) : "****";
        }  
    }
}
