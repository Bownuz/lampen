using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HueApp_Fedde_en_Siem {
    internal class HueAPI {
        static readonly HttpClient client = new HttpClient();

        public static async Task SendCommand(string bridgeIp, string username, string lampId, string jsonPayload) {
            try {
                string url = $"http://{bridgeIp}/api/{username}/lights/{lampId}/state";

                // Bouw het HTTP-verzoek met de JSON-payload
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                // Controleer of het verzoek succesvol was
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response: {responseBody}");
            }
            catch (Exception e) {
                Debug.WriteLine($"Fout bij het versturen van het commando: {e.Message}");
            }
        }

        public static async Task<string> GetLampState(string bridgeIp, string username, string lampId) {
            try {
                string url = $"http://{bridgeIp}/api/{username}/lights/{lampId}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e) {
                Debug.WriteLine($"Fout bij het ophalen van de lampstatus: {e.Message}");
                return null;
            }
        }
    }

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
