using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueApp_Fedde_en_Siem.Onion.Domain {
    public interface IHueService {
        Task<string> GetLampStateAsync(string bridgeIp, string username, string lampId);
        Task SendCommandAsync(string bridgeIp, string username, string lampId, string jsonPayload);
    }
}
