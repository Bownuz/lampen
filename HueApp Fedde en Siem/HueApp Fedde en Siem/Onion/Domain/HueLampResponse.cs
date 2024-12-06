using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HueApp_Fedde_en_Siem.Onion.Domain {
    public class HueLampResponse {
        [JsonPropertyName("state")]
        public LampState State { get; set; }

        public class LampState {
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
}
