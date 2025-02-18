using System.Text.Json.Serialization;

namespace HueApp_Fedde_en_Siem.Onion.Domain
{
    public class HueLampResponse
    {
        [JsonPropertyName("state")]
        public LampState State { get; set; }

        public class LampState
        {
            [JsonPropertyName("on")]
            public bool On { get; set; }

            [JsonPropertyName("bri")]
            public int Bri { get; set; }

            [JsonPropertyName("hue")]
            public int Hue { get; set; }

            [JsonPropertyName("sat")]
            public int Sat { get; set; }
        }
    }

    public class HueConfiguration
    {
        public string BridgeIp { get; set; }
        public string Username { get; set; }

        public HueConfiguration()
        {
            //lampen lokaal la134
            //BridgeIp = "192.168.1.179";
            //Username = "68rsJFBpkbVcMNHd9v2aELDrnOl3XYlZbczgxfMM";

            //lampen emulator
            BridgeIp = "localhost";
            Username = "newdeveloper";
        }
    }
}
