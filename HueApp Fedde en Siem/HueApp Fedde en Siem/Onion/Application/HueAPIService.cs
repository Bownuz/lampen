using HueApp_Fedde_en_Siem.Onion.Domain;
using System.Diagnostics;
using System.Text;

namespace HueApp_Fedde_en_Siem.Onion.Application
{
    internal class HueAPIService : IHueService
    {
        HttpClient client;

        public HueAPIService(HttpClient client)
        {
            this.client = client;
        }

        public async Task SendCommandAsync(string bridgeIp, string username, string lampId, string jsonPayload)
        {
            try
            {
                string url = $"http://{bridgeIp}/api/{username}/lights/{lampId}/state";

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response: {responseBody}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Fout bij het versturen van het commando: {e.Message}");
            }
        }

        public async Task<string> GetLampStateAsync(string bridgeIp, string username, string lampId)
        {
            try
            {
                string url = $"http://{bridgeIp}/api/{username}/lights/{lampId}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Fout bij het ophalen van de lampstatus: {e.Message}");
                return null;
            }
        }
    }
}
