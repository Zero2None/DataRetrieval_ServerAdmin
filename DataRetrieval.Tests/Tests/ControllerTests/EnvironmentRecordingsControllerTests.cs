using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Controllers;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DataRetrieval.Tests.ControllerTests
{
    public class EnvironmentRecordingsControllerTests
    {
        private readonly Mock<IDataService> _mockDataService;
        private readonly EnvironmentRecordingsController _controller;

        public EnvironmentRecordingsControllerTests()
        {
            _mockDataService = new Mock<IDataService>();
            _controller = new EnvironmentRecordingsController(_mockDataService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithData()
        {
            var mockRecordings = new List<EnvironmentRecording>
            {
                new EnvironmentRecording { Id = "env1", AudioFileName = "env1.wav", AudioQuality = "High", DeviceName = "Internal Mic" },
                new EnvironmentRecording { Id = "env2", AudioFileName = "env2.wav", AudioQuality = "High", DeviceName = "External Mic" }
            };

            _mockDataService.Setup(s => s.GetEnvironmentRecordingsAsync())
                        .ReturnsAsync(mockRecordings);

            var result = await _controller.GetAll();

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var apiResponse = okResult?.Value as ApiResponse<IEnumerable<EnvironmentRecording>>;
            
            apiResponse?.Success.Should().BeTrue();
            apiResponse?.Count.Should().Be(2);
            apiResponse?.Data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOkWithData()
        {
            var mockRecording = new EnvironmentRecording { Id = "env1", AudioFileName = "env1.wav", AudioQuality = "High", DeviceName = "Internal Mic" };
            
            _mockDataService.Setup(s => s.GetEnvironmentRecordingByIdAsync("env1"))
                        .ReturnsAsync(mockRecording);

            var result = await _controller.GetById("env1");

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var apiResponse = okResult?.Value as ApiResponse<EnvironmentRecording>;
            
            apiResponse?.Success.Should().BeTrue();
            apiResponse?.Data?.Id.Should().Be("env1");
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            _mockDataService.Setup(s => s.GetEnvironmentRecordingByIdAsync("invalid"))
                        .ReturnsAsync((EnvironmentRecording?)null);

            var result = await _controller.GetById("invalid");

            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}