using Microsoft.AspNetCore.Mvc;
using DataRetrieval.Client.Models;
using DataRetrieval.Client.Services;

namespace DataRetrieval.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvironmentRecordingsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public EnvironmentRecordingsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EnvironmentRecording>>>> GetAll()
        {
            try
            {
                var recordings = await _dataService.GetEnvironmentRecordingsAsync();
                return Ok(new ApiResponse<IEnumerable<EnvironmentRecording>>
                {
                    Success = true,
                    Data = recordings,
                    Count = recordings.Count(),
                    Message = "Environment recordings retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<EnvironmentRecording>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving environment recordings: {ex.Message}"
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EnvironmentRecording>>> GetById(string id)
        {
            try
            {
                var recording = await _dataService.GetEnvironmentRecordingByIdAsync(id);
                if (recording == null)
                {
                    return NotFound(new ApiResponse<EnvironmentRecording>
                    {
                        Success = false,
                        Data = null,
                        Message = "Environment recording not found"
                    });
                }

                return Ok(new ApiResponse<EnvironmentRecording>
                {
                    Success = true,
                    Data = recording,
                    Count = 1,
                    Message = "Environment recording retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<EnvironmentRecording>
                {
                    Success = false,
                    Data = null,
                    Message = $"Error retrieving environment recording: {ex.Message}"
                });
            }
        }
    }
}