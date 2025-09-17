using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Controllers;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DataRetrieval.Tests.ControllerTests
{
    public class ScreenRecordingsControllerTests
    {
        private readonly Mock<IDataService> _mockDataService;
        private readonly ScreenRecordingsController _controller;

        public ScreenRecordingsControllerTests()
        {
            _mockDataService = new Mock<IDataService>();
            _controller = new ScreenRecordingsController(_mockDataService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithData()
        {
            var mockRecordings = new List<ScreenRecording>
            {
                new ScreenRecording { Id = "test1", FileName = "test1.mp4", Resolution = "1920x1080", Status = "Active" },
                new ScreenRecording { Id = "test2", FileName = "test2.mp4", Resolution = "1920x1080", Status = "Active" }
            };

            _mockDataService.Setup(s => s.GetScreenRecordingsAsync())
                        .ReturnsAsync(mockRecordings);

            var result = await _controller.GetAll();

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var apiResponse = okResult?.Value as ApiResponse<IEnumerable<ScreenRecording>>;
            
            apiResponse?.Success.Should().BeTrue();
            apiResponse?.Count.Should().Be(2);
            apiResponse?.Data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAll_WhenServiceThrows_ShouldReturnInternalServerError()
        {
            _mockDataService.Setup(s => s.GetScreenRecordingsAsync())
                           .ThrowsAsync(new Exception("Service error"));

            var result = await _controller.GetAll();

            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOkWithData()
        {
            var mockRecording = new ScreenRecording { Id = "test1", FileName = "test1.mp4", Resolution = "1920x1080", Status = "Active" };
            
            _mockDataService.Setup(s => s.GetScreenRecordingByIdAsync("test1"))
                        .ReturnsAsync(mockRecording);

            var result = await _controller.GetById("test1");

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var apiResponse = okResult?.Value as ApiResponse<ScreenRecording>;
            
            apiResponse?.Success.Should().BeTrue();
            apiResponse?.Data?.Id.Should().Be("test1");
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            _mockDataService.Setup(s => s.GetScreenRecordingByIdAsync("invalid"))
                        .ReturnsAsync((ScreenRecording?)null);

            var result = await _controller.GetById("invalid");

            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetById_WhenServiceThrows_ShouldReturnInternalServerError()
        {
            _mockDataService.Setup(s => s.GetScreenRecordingByIdAsync("test1"))
                           .ThrowsAsync(new Exception("Service error"));

            var result = await _controller.GetById("test1");

            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
}