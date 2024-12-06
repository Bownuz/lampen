namespace HueApp_Fedde_en_Siem;
using CommunityToolkit.Maui.Views;
using System.Text.Json;

public partial class PopupTest : Popup {
    private string bridgeIp;
    private string username;
    private string lampId;
    private bool on_off;
    private int brightness;
    private int hue;
    private int saturation;

    private HueAPI HueAPI = new HueAPI();
    public PopupTest(string bridgeIp, string username, string lampId) {
        InitializeComponent();

        // Initialiseer de lamp-informatie
        //this.bridgeIp = bridgeIp;
        this.bridgeIp = "localhost";
        //this.username = username;
        this.username = "newdeveloper";
        this.lampId = lampId;

        // Haal status van de lamp op
        InitializeLampState();
    }

    private async void InitializeLampState() {
        try {
            string lampStateJson = await HueAPI.GetLampState(bridgeIp, username, lampId);
            Console.WriteLine(lampStateJson);
            if (!string.IsNullOrEmpty(lampStateJson)) {
                // Parse JSON naar een object
                var lampData = JsonSerializer.Deserialize<HueLampResponse>(lampStateJson);

                // Stel waarden in
                on_off = lampData.State.On;
                brightness = lampData.State.Bri;
                hue = lampData.State.Hue;
                saturation = lampData.State.Sat;

                // Update de UI
                On_OffButton.Text = on_off ? "Lamp on" : "Lamp off";
                Brightness_Slider.Value = brightness;
                Hue_Slider.Value = hue;
                Saturation_Slider.Value = saturation;
                Lamp_Number_Label.Text = "Lamp " + lampId;

                Brightness_Label.Text = $"The brightness value is {brightness}";
                Hue_Label.Text = $"The hue value is {hue}";
                Saturation_Label.Text = $"The saturation value is {saturation}";
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Fout bij ophalen lampstatus: {ex.Message}");
        }
    }

    private async void Button_Pressed(object sender, EventArgs e) {
        Close();
    }

    private async void Slider_ValueChanged(object sender, ValueChangedEventArgs e) {
        brightness = (int)e.NewValue;
        Brightness_Label.Text = $"The brightness value is {brightness}";

        // Roep de centrale methode aan
        await UpdateLampState();
    }

    private async void Slider_ValueChanged_1(object sender, ValueChangedEventArgs e) {
        hue = (int)e.NewValue;
        Hue_Label.Text = $"The hue value is {hue}";

        // Roep de centrale methode aan
        await UpdateLampState();
    }

    private async void Slider_ValueChanged_2(object sender, ValueChangedEventArgs e) {
        saturation = (int)e.NewValue;
        Saturation_Label.Text = $"The saturation value is {saturation}";

        // Roep de centrale methode aan
        await UpdateLampState();
    }

    private async void Button_Pressed_1(object sender, EventArgs e) {
        try {
            // Draai de aan/uit-status om
            on_off = !on_off;

            // Update de knoptekst
            On_OffButton.Text = on_off ? "Lamp on" : "Lamp off";

            // Roep de centrale methode aan
            await UpdateLampState();
        }
        catch (Exception ex) {
            Console.WriteLine($"Fout bij het aan-/uitzetten van de lamp: {ex.Message}");
        }
    }

    private async Task UpdateLampState() {
        try {
            // Bouw de JSON-payload met de huidige waarden
            string jsonPayload = $"{{\"on\": {on_off.ToString().ToLower()}, \"bri\": {brightness}, \"hue\": {hue}, \"sat\": {saturation}}}";

            // Stuur het commando naar de Hue API
            await HueAPI.SendCommand(bridgeIp, username, lampId, jsonPayload);

            // Log voor debuggen
            Console.WriteLine($"Lampstate updated with: {jsonPayload}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Error updating lamp state: {ex.Message}");
        }
    }
}