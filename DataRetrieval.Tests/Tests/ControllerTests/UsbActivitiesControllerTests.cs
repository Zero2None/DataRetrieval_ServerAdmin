using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Controllers;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DataRetrieval.Tests.ControllerTests
{
    public class UsbActivitiesControllerTests
    {
        private readonly Mock<IDataService> _mockDataService;
        private readonly UsbActivitiesController _controller;

        public UsbActivitiesControllerTests()
        {
            _mockDataService = new Mock<IDataService>();
            _controller = new UsbActivitiesController(_mockDataService.Object);
        }

       [Fact]
public async Task GetAll_ShouldReturnOkWithData()
{
    var mockActivities = new List<UsbActivity>
    {
        new UsbActivity { Id = "usb1", DeviceName = "USB Drive", DeviceId = "USB001", Action = "Connected", VendorId = "1234", ProductId = "5678", SerialNumber = "ABC123" },
        new UsbActivity { Id = "usb2", DeviceName = "Mouse", DeviceId = "USB002", Action = "Connected", VendorId = "1234", ProductId = "5678", SerialNumber = "DEF456" }
    };

    _mockDataService.Setup(s => s.GetUsbActivitiesAsync())
                   .ReturnsAsync(mockActivities);

    var result = await _controller.GetAll();

    result.Result.Should().BeOfType<OkObjectResult>();
    var okResult = result.Result as OkObjectResult;
    var apiResponse = okResult?.Value as ApiResponse<IEnumerable<UsbActivity>>;
    
    apiResponse?.Success.Should().BeTrue();
    apiResponse?.Count.Should().Be(2);
    apiResponse?.Data.Should().HaveCount(2);
}

[Fact]
public async Task GetById_WithValidId_ShouldReturnOkWithData()
{
    var mockActivity = new UsbActivity { Id = "usb1", DeviceName = "USB Drive", DeviceId = "USB001", Action = "Connected", VendorId = "1234", ProductId = "5678", SerialNumber = "ABC123" };
    
    _mockDataService.Setup(s => s.GetUsbActivityByIdAsync("usb1"))
                   .ReturnsAsync(mockActivity);

    var result = await _controller.GetById("usb1");

    result.Result.Should().BeOfType<OkObjectResult>();
    var okResult = result.Result as OkObjectResult;
    var apiResponse = okResult?.Value as ApiResponse<UsbActivity>;
    
    apiResponse?.Success.Should().BeTrue();
    apiResponse?.Data?.Id.Should().Be("usb1");
}

[Fact]
public async Task GetById_WithInvalidId_ShouldReturnNotFound()
{
    _mockDataService.Setup(s => s.GetUsbActivityByIdAsync("invalid"))
                   .ReturnsAsync((UsbActivity?)null);

    var result = await _controller.GetById("invalid");

    result.Result.Should().BeOfType<NotFoundObjectResult>();
}
    }
}