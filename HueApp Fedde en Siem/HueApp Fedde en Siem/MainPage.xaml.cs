using CommunityToolkit.Maui.Views;
using System.Linq.Expressions;

namespace HueApp_Fedde_en_Siem {
    public partial class MainPage : ContentPage {
        string bridgeIp = "192.168.1.179";
        string username = "68rsJFBpkbVcMNHd9v2aELDrnOl3XYlZbczgxfMM";
        string lampId = "0";

        public MainPage() {
            InitializeComponent();
        }

        private void Lamp10Pressed(object sender, EventArgs e) {
            lampId = "10";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp11Pressed(object sender, EventArgs e) {
            lampId = "11";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp12Pressed(object sender, EventArgs e) {
            lampId = "12";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp14Pressed(object sender, EventArgs e) {
            lampId = "14";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp15Pressed(object sender, EventArgs e) {
            lampId = "15";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp16Pressed(object sender, EventArgs e) {
            lampId = "16";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp17Pressed(object sender, EventArgs e) {
            lampId = "17";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }

        private void Lamp18Pressed(object sender, EventArgs e) {
            lampId = "18";
            this.ShowPopup(new PopupTest(bridgeIp, username, lampId));
        }
    }

}
