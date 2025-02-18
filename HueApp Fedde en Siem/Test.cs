//using Moq;
//using Moq.Contrib.HttpClient;
//using System.Net.Http;
//using Xunit;
//using System.Net;
//using System.Threading.Tasks;
//using HueApp_Fedde_en_Siem.Onion.Application;

//namespace HueApp_Fedde_en_Siem.Tests {
//    public class HueAPIServiceTests {
//        private readonly Mock<HttpMessageHandler> _mockHandler;
//        private readonly HttpClient _mockHttpClient;
//        private readonly HueAPIService _hueAPIService;

//        public HueAPIServiceTests() {
//            _mockHandler = new Mock<HttpMessageHandler>();
//            _mockHttpClient = _mockHandler.CreateClient();
//            _hueAPIService = new HueAPIService(_mockHttpClient);
//        }

//        // Happy Path: Test dat het commando succesvol wordt verzonden
//        [Fact]
//        public async Task SendCommandAsync_ShouldReturnSuccess_WhenResponseIsSuccessful() {
//            // Arrange
//            var bridgeIp = "localhost";
//            var username = "testusername";
//            var lampId = "10";
//            var jsonPayload = "{\"on\": true}";

//            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) {
//                Content = new StringContent("{\"success\": true}")
//            };

//            _mockHandler
//                .SetupRequest(HttpMethod.Put, $"http://{bridgeIp}/api/{username}/lights/{lampId}/state")
//                .ReturnsResponse(responseMessage.StatusCode, responseMessage.Content);

//            // Act
//            await _hueAPIService.SendCommandAsync(bridgeIp, username, lampId, jsonPayload);

//            // Assert
//            _mockHandler.VerifyRequest(HttpMethod.Put, $"http://{bridgeIp}/api/{username}/lights/{lampId}/state");
//        }

//        // Unhappy Path: Test dat er een foutmelding wordt weergegeven bij een mislukte HTTP-aanroep
//        [Fact]
//        public async Task SendCommandAsync_ShouldHandleError_WhenResponseIsNotSuccessful() {
//            // Arrange
//            var bridgeIp = "localhost";
//            var username = "testusername";
//            var lampId = "10";
//            var jsonPayload = "{\"on\": true}";

//            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest) {
//                Content = new StringContent("{\"error\": \"Invalid request\"}")
//            };

//            _mockHandler
//                .SetupRequest(HttpMethod.Put, $"http://{bridgeIp}/api/{username}/lights/{lampId}/state")
//                .ReturnsResponse(responseMessage.StatusCode, responseMessage.Content);

//            await _hueAPIService.SendCommandAsync(bridgeIp, username, lampId, jsonPayload);
//        }

//        // Happy Path: Test voor het ophalen van de lampstatus
//        [Fact]
//        public async Task GetLampStateAsync_ShouldReturnLampState_WhenResponseIsSuccessful() {
//            // Arrange
//            var bridgeIp = "localhost";
//            var username = "testusername";
//            var lampId = "10";
//            var jsonResponse = "{\"state\": {\"on\": true, \"bri\": 254, \"hue\": 10000, \"sat\": 254}}";

//            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) {
//                Content = new StringContent(jsonResponse)
//            };

//            _mockHandler
//                .SetupRequest(HttpMethod.Get, $"http://{bridgeIp}/api/{username}/lights/{lampId}")
//                .ReturnsResponse(responseMessage.StatusCode, responseMessage.Content);

//            // Act
//            var result = await _hueAPIService.GetLampStateAsync(bridgeIp, username, lampId);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Contains("on", result);
//        }

//        // Unhappy Path: Test voor het ophalen van de lampstatus bij een mislukte response
//        [Fact]
//        public async Task GetLampStateAsync_ShouldHandleError_WhenResponseIsNotSuccessful() {
//            // Arrange
//            var bridgeIp = "localhost";
//            var username = "testusername";
//            var lampId = "10";
//            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest) {
//                Content = new StringContent("{\"error\": \"Lamp not found\"}")
//            };

//            _mockHandler
//                .SetupRequest(HttpMethod.Get, $"http://{bridgeIp}/api/{username}/lights/{lampId}")
//                .ReturnsResponse(responseMessage.StatusCode, responseMessage.Content);

//            // Act
//            var result = await _hueAPIService.GetLampStateAsync(bridgeIp, username, lampId);

//            // Assert
//            Assert.Null(result);
//        }
//    }
//}
