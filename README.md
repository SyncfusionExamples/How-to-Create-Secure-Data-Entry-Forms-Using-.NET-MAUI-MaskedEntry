# How-to-Create-Secure-Data-Entry-Forms-Using-.NET-MAUI-MaskedEntry

This section explains how to build a secure data entry form in .NET MAUI using the SfMaskedEntry control. By applying predefined masks for credit card numbers, expiry dates, CVV codes, phone numbers, Social Security Numbers (SSN), and ZIP codes, you can ensure that users enter sensitive information in a consistent and validated format. Password masking can also be used to hide confidential data such as CVV and SSN values while users type.

# XAML

Define multiple SfMaskedEntry controls with appropriate masks to securely collect user information. In this example, separate sections are used for payment details, contact information, and identification data. Standard Entry controls are combined with masked inputs where formatting restrictions are not required.

```xaml
<ScrollView>
       <VerticalStackLayout Padding="20,20"
                            Spacing="20">

           <!-- Title -->
           <Label Text="Secure Data Entry Form"
                  FontSize="24"
                  FontAttributes="Bold"
                  HorizontalTextAlignment="Center"/>

           <Label Text="Enter your information in the masked fields below"
                  FontSize="14"
                  HorizontalTextAlignment="Center"/>

           <!-- Credit Card Section -->
           <Frame BorderColor="#E0E0E0"
                  CornerRadius="10"
                  Padding="15"
                  HasShadow="True">
               <VerticalStackLayout Spacing="15">
                   <Label Text="Credit Card Information"
                          FontSize="16"
                          FontAttributes="Bold"/>

                   <!-- Credit Card Number -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="Card Number (16 digits)"
                              FontSize="12"/>
                       <editors:SfMaskedEntry x:Name="CardNumberEntry"
                                              Mask="0000 0000 0000 0000"
                                              MaskType="Simple"
                                              Placeholder="0000 0000 0000 0000"
                                              TextColor="#333333"
                                              Stroke="#CCCCCC"/>
                   </VerticalStackLayout>

                   <!-- Cardholder Name -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="Cardholder Name"
                              FontSize="12"/>
                       <Entry x:Name="CardholderEntry"
                              Placeholder="John Doe"
                              TextColor="#333333"
                              BackgroundColor="#FFFFFF"/>
                   </VerticalStackLayout>

                   <!-- Expiry Date -->
                   <HorizontalStackLayout Spacing="15">
                       <VerticalStackLayout Spacing="5" 
                                            FlexLayout.Basis="50%">
                           <Label Text="Expiry Date (MM/YY)"
                                  FontSize="12"/>
                           <editors:SfMaskedEntry x:Name="ExpiryEntry"
                                                  Mask="00/00"
                                                  MaskType="Simple"
                                                  Placeholder="MM/YY"
                                                  TextColor="#333333"
                                                  WidthRequest="60"/>
                       </VerticalStackLayout>

                       <!-- CVV -->
                       <VerticalStackLayout Spacing="5" 
                                            FlexLayout.Basis="50%">
                           <Label Text="CVV (3 digits)"
                                  FontSize="12"/>
                           <editors:SfMaskedEntry x:Name="CVVEntry"
                                                  Mask="000"
                                                  MaskType="Simple"
                                                  Placeholder="000"
                                                  TextColor="#333333"
                                                  PasswordChar="*"
                                                  WidthRequest="40"/>
                       </VerticalStackLayout>
                   </HorizontalStackLayout>
               </VerticalStackLayout>
           </Frame>

           <!-- Contact Information Section -->
           <Frame BorderColor="#E0E0E0"
                  CornerRadius="10"
                  Padding="15"
                  HasShadow="True">
               <VerticalStackLayout Spacing="15">
                   <Label Text="Contact Information"
                          FontSize="16"
                          FontAttributes="Bold"/>

                   <!-- Phone Number -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="Phone Number"
                              FontSize="12"/>
                       <editors:SfMaskedEntry x:Name="PhoneEntry"
                                              Mask="(000) 000-0000"
                                              MaskType="Simple"
                                              Placeholder="(000) 000-0000"
                                              TextColor="#333333"
                                              Stroke="#CCCCCC"/>
                   </VerticalStackLayout>

                   <!-- Email -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="Email Address"
                              FontSize="12"/>
                       <Entry x:Name="EmailEntry"
                              Placeholder="user@example.com"
                              Keyboard="Email"
                              TextColor="#333333"
                              BackgroundColor="#FFFFFF"/>
                   </VerticalStackLayout>
               </VerticalStackLayout>
           </Frame>

           <!-- Identification Section -->
           <Frame BorderColor="#E0E0E0"
                  CornerRadius="10"
                  Padding="15"
                  HasShadow="True">
               <VerticalStackLayout Spacing="15">
                   <Label Text="Identification"
                          FontSize="16"
                          FontAttributes="Bold"/>

                   <!-- SSN -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="Social Security Number (SSN)"
                              FontSize="12"/>
                       <editors:SfMaskedEntry x:Name="SSNEntry"
                                              Mask="000-00-0000"
                                              MaskType="Simple"
                                              Placeholder="000-00-0000"
                                              PasswordChar="*"
                                              TextColor="#333333"/>
                   </VerticalStackLayout>

                   <!-- ZIP Code -->
                   <VerticalStackLayout Spacing="5">
                       <Label Text="ZIP Code"
                              FontSize="12"/>
                       <editors:SfMaskedEntry x:Name="ZIPEntry"
                                              Mask="00000"
                                              MaskType="Simple"
                                              Placeholder="00000"
                                              TextColor="#333333"/>
                   </VerticalStackLayout>
               </VerticalStackLayout>
           </Frame>

           <!-- Buttons -->
           <HorizontalStackLayout Spacing="10" 
                                  Margin="0,10">
               <Button x:Name="SubmitButton"
                       Text="Submit"
                       BackgroundColor="#512BD4"
                       TextColor="White"
                       FontAttributes="Bold"
                       Padding="15"
                       CornerRadius="5"
                       FlexLayout.Basis="50%"
                       Clicked="OnSubmitClicked"/>

               <Button x:Name="ClearButton"
                       Text="Clear"
                       BackgroundColor="#999999"
                       TextColor="White"
                       FontAttributes="Bold"
                       Padding="15"
                       CornerRadius="5"
                       FlexLayout.Basis="50%"
                       Clicked="OnClearClicked"/>
           </HorizontalStackLayout>

           <!-- Status Message -->
           <Label x:Name="StatusLabel"
                  Text=""
                  FontSize="12"
                  TextColor="#4CAF50"
                  HorizontalTextAlignment="Center"
                  Margin="0,10"/>

       </VerticalStackLayout>
   </ScrollView> 
```

# C#

Handle the Submit and Clear button events to validate form input and manage user data. The entered values are checked to ensure that all required fields contain data before submission. Sensitive information such as the CVV and SSN is masked during entry, while only the last four digits of the credit card number are displayed in the confirmation message.

```csharp
private void OnSubmitClicked(object sender, EventArgs e)
{
    if (ValidateForm())
    {
        StatusLabel.Text =
            "✓ Form submitted successfully!";

        StatusLabel.TextColor =
            Colors.Green;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await DisplayAlert(
                "Success",
                "Your secure data has been submitted.\n\n" +
                $"Last 4 digits of card: " +
                $"{GetLast4Digits(CardNumberEntry.Value?.ToString())}\n" +
                $"Phone: {PhoneEntry.Value}\n" +
                $"Cardholder: {CardholderEntry.Text}",
                "OK");
        });
    }
    else
    {
        StatusLabel.Text =
            "✗ Please fill in all required fields";

        StatusLabel.TextColor =
            Colors.Red;
    }
}

private void OnClearClicked(
    object sender,
    EventArgs e)
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
    return !string.IsNullOrWhiteSpace(
               CardNumberEntry.Value?.ToString()) &&
           !string.IsNullOrWhiteSpace(
               CardholderEntry.Text) &&
           !string.IsNullOrWhiteSpace(
               ExpiryEntry.Value?.ToString()) &&
           !string.IsNullOrWhiteSpace(
               CVVEntry.Value?.ToString()) &&
           !string.IsNullOrWhiteSpace(
               PhoneEntry.Value?.ToString()) &&
           !string.IsNullOrWhiteSpace(
               EmailEntry.Text);
}

private string GetLast4Digits(string value)
{
    string digitsOnly =
        value?.Replace(" ", "") ??
        string.Empty;

    return digitsOnly.Length >= 4
        ? digitsOnly.Substring(
            digitsOnly.Length - 4)
        : "****";
}
```

The SfMaskedEntry control helps secure sensitive user information by enforcing predefined input formats and restricting invalid entries. It automatically formats values such as credit card numbers, expiry dates, phone numbers, SSNs, and ZIP codes while masking confidential fields like CVV and SSN values. This approach reduces input errors, improves data consistency, and provides a more secure and user-friendly data entry experience.