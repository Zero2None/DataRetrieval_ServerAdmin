using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;

namespace DataRetrieval.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenRecordingsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ScreenRecordingsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ScreenRecording>>>> GetAll()
        {
            try
            {
                var recordings = await _dataService.GetScreenRecordingsAsync();
                return Ok(new ApiResponse<IEnumerable<ScreenRecording>>
                {
                    Success = true,
                    Data = recordings,
                    Count = recordings.Count(),
                    Message = "Screen recordings retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<ScreenRecording>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving screen recordings: {ex.Message}"
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ScreenRecording>>> GetById(string id)
        {
            try
            {
                var recording = await _dataService.GetScreenRecordingByIdAsync(id);
                if (recording == null)
                {
                    return NotFound(new ApiResponse<ScreenRecording>
                    {
                        Success = false,
                        Data = null,
                        Message = "Screen recording not found"
                    });
                }

                return Ok(new ApiResponse<ScreenRecording>
                {
                    Success = true,
                    Data = recording,
                    Count = 1,
                    Message = "Screen recording retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ScreenRecording>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving screen recording: {ex.Message}"
                });
            }
        }
    }
}