using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HueApp_Fedde_en_Siem.Onion.Application;
using HueApp_Fedde_en_Siem.Onion.Domain;
using System.Text.Json;
using System.Windows.Input;

namespace HueApp_Fedde_en_Siem.Onion.Presentation.ViewModels
{
    public partial class PopupTestViewModel : ObservableObject {
        private readonly IHueService hueService;
        private readonly HueConfiguration hueConfig;
        private string bridgeIp;
        private string username;
        private string lampId;

        public string LampToggleText => IsLampOn ? "Lamp On" : "Lamp Off";

        [ObservableProperty]
        private bool isLampOn;

        [ObservableProperty]
        private int brightness;

        [ObservableProperty]
        private int hue;

        [ObservableProperty]
        private int saturation;

        [ObservableProperty]
        private Color selectedColor;

        public ICommand ToggleLampCommand { get; }
        public ICommand UpdateLampSettingsCommand { get; }

        public PopupTestViewModel() {
            hueConfig = new HueConfiguration();
            hueService = new HueAPIService(new HttpClient());
            ToggleLampCommand = new RelayCommand(async () => await ToggleLampAsync());
            UpdateLampSettingsCommand = new RelayCommand(async () => await UpdateLampSettingsAsync());
            PropertyChanged += OnPropertyChanged;
        }

        public void Initialize(string lampId) {
            this.lampId = lampId;
            bridgeIp = hueConfig.BridgeIp;
            username = hueConfig.Username;
            LoadLampState();
        }

        partial void OnIsLampOnChanged(bool value) {
            OnPropertyChanged(nameof(LampToggleText));
        }

        private async void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Brightness) || e.PropertyName == nameof(Hue) || e.PropertyName == nameof(Saturation)) {
                UpdateSelectedColor();
                await UpdateLampSettingsAsync();
            }
        }

        private void UpdateSelectedColor() {
            float normalizedHue = Hue / 65535f;
            float normalizedSaturation = Saturation / 254f;
            float normalizedBrightness = Brightness / 254f;

            SelectedColor = Color.FromHsv(normalizedHue, normalizedSaturation, normalizedBrightness);
        }

        public async Task LoadLampState() {
            try {
                var response = await hueService.GetLampStateAsync(bridgeIp, username, lampId);
                if (!string.IsNullOrEmpty(response)) {
                    var lampState = JsonSerializer.Deserialize<HueLampResponse>(response);
                    IsLampOn = lampState.State.On;
                    Brightness = lampState.State.Bri;
                    Hue = lampState.State.Hue;
                    Saturation = lampState.State.Sat;

                    UpdateSelectedColor();
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading lamp state: {ex.Message}");
            }
        }

        private async Task ToggleLampAsync() {
            try {
                IsLampOn = !IsLampOn;
                var payload = JsonSerializer.Serialize(new { on = IsLampOn });
                await hueService.SendCommandAsync(bridgeIp, username, lampId, payload);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error toggling lamp: {ex.Message}");
            }
        }

        private async Task UpdateLampSettingsAsync() {
            try {
                var payload = JsonSerializer.Serialize(new {
                    bri = Brightness,
                    hue = Hue,
                    sat = Saturation
                });
                await hueService.SendCommandAsync(bridgeIp, username, lampId, payload);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error updating lamp settings: {ex.Message}");
            }
        }
    }
}
