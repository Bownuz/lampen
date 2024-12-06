using HueApp_Fedde_en_Siem.Onion.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HueApp_Fedde_en_Siem.Onion.Application {
    internal class HueAPIService : IHueService {
        static readonly HttpClient client = new HttpClient();

        public async Task SendCommandAsync(string bridgeIp, string username, string lampId, string jsonPayload) {
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

        public async Task<string> GetLampStateAsync(string bridgeIp, string username, string lampId) {
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
}
