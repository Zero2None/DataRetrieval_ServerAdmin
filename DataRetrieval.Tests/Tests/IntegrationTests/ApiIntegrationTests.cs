using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace DataRetrieval.Tests.IntegrationTests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetScreenRecordings_ShouldReturnSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/screenrecordings");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ScreenRecording>>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeEmpty();
            apiResponse.Count.Should().Be(15);
        }

        [Fact]
        public async Task GetEnvironmentRecordings_ShouldReturnSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/environmentrecordings");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<EnvironmentRecording>>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeEmpty();
            apiResponse.Count.Should().Be(12);
        }

        [Fact]
        public async Task GetUsbActivities_ShouldReturnSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/usbactivities");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<UsbActivity>>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeEmpty();
            apiResponse.Count.Should().Be(20);
        }

        [Fact]
        public async Task GetScreenRecordingById_WithValidId_ShouldReturnRecord()
        {
            var response = await _client.GetAsync("/api/screenrecordings/screen_001");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ScreenRecording>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeNull();
            apiResponse.Data.Id.Should().Be("screen_001");
        }

        [Fact]
        public async Task GetScreenRecordingById_WithInvalidId_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("/api/screenrecordings/invalid_id");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetEnvironmentRecordingById_WithValidId_ShouldReturnRecord()
        {
            var response = await _client.GetAsync("/api/environmentrecordings/env_001");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<EnvironmentRecording>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeNull();
            apiResponse.Data.Id.Should().Be("env_001");
        }

        [Fact]
        public async Task GetUsbActivityById_WithValidId_ShouldReturnRecord()
        {
            var response = await _client.GetAsync("/api/usbactivities/usb_001");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UsbActivity>>(content);
            
            apiResponse.Success.Should().BeTrue();
            apiResponse.Data.Should().NotBeNull();
            apiResponse.Data.Id.Should().Be("usb_001");
        }
    }
}