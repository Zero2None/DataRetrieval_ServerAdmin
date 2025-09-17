using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;

namespace DataRetrieval.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsbActivitiesController : ControllerBase
    {
        private readonly IDataService _dataService;

        public UsbActivitiesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UsbActivity>>>> GetAll()
        {
            try
            {
                var activities = await _dataService.GetUsbActivitiesAsync();
                return Ok(new ApiResponse<IEnumerable<UsbActivity>>
                {
                    Success = true,
                    Data = activities,
                    Count = activities.Count(),
                    Message = "USB activities retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<UsbActivity>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving USB activities: {ex.Message}"
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UsbActivity>>> GetById(string id)
        {
            try
            {
                var activity = await _dataService.GetUsbActivityByIdAsync(id);
                if (activity == null)
                {
                    return NotFound(new ApiResponse<UsbActivity>
                    {
                        Success = false,
                        Data = null,
                        Message = "USB activity not found"
                    });
                }

                return Ok(new ApiResponse<UsbActivity>
                {
                    Success = true,
                    Data = activity,
                    Count = 1,
                    Message = "USB activity retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UsbActivity>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving USB activity: {ex.Message}"
                });
            }
        }
    }
}