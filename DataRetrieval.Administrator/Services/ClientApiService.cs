using DataRetrieval.Administrator.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DataRetrieval.Administrator.Services
{
    public class ClientApiService : IClientApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ClientApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ClientApi:BaseUrl"] ?? "http://localhost:5000";
        }

        public async Task<IEnumerable<ScreenRecording>> GetScreenRecordingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/screenrecordings");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ScreenRecording>>>(content);
            
            return apiResponse.Success ? apiResponse.Data : new List<ScreenRecording>();
        }

        public async Task<IEnumerable<EnvironmentRecording>> GetEnvironmentRecordingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/environmentrecordings");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<EnvironmentRecording>>>(content);
            
            return apiResponse.Success ? apiResponse.Data : new List<EnvironmentRecording>();
        }

        public async Task<IEnumerable<UsbActivity>> GetUsbActivitiesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/usbactivities");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<UsbActivity>>>(content);
            
            return apiResponse.Success ? apiResponse.Data : new List<UsbActivity>();
        }

        public async Task<ScreenRecording?> GetScreenRecordingByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/screenrecordings/{id}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ScreenRecording>>(content);
            
            return apiResponse?.Success == true ? apiResponse.Data : null;
        }

        public async Task<EnvironmentRecording?> GetEnvironmentRecordingByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/environmentrecordings/{id}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<EnvironmentRecording>>(content);
            
            return apiResponse?.Success == true ? apiResponse.Data : null;
        }

        public async Task<UsbActivity?> GetUsbActivityByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/usbactivities/{id}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UsbActivity>>(content);
            
            return apiResponse?.Success == true ? apiResponse.Data : null;
        }
    }
}