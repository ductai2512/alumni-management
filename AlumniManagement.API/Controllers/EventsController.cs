using AlumniManagement.Shared.DTOs.Event;
using AlumniManagement.Shared.DTOs.Alumni;
using AlumniManagement.Shared.DTOs.Common;
using AlumniManagement.BUS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlumniManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Lấy danh sách tất cả sự kiện
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _eventService.GetAllEventsAsync();
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Lấy thông tin sự kiện theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _eventService.GetEventByIdAsync(id);
                return Ok(ApiResponse<EventDto>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Tạo sự kiện mới (Admin only)
        /// </summary>
        [HttpPost("createAlumni")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
        {
            try
            {
                var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _eventService.CreateEventAsync(request, accountId);
                return CreatedAtAction(nameof(GetById), new { id = result.EventId }, 
                    ApiResponse<EventDto>.SuccessResponse(result, "Event created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật sự kiện (Admin only)
        /// </summary>
        [HttpPut("UpdateEvent/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventRequest request)
        {
            try
            {
                var result = await _eventService.UpdateEventAsync(id, request);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Event updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Xóa sự kiện (Admin only)
        /// </summary>
        [HttpDelete("DeleteEvent/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _eventService.DeleteEventAsync(id);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Event deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Đăng ký tham gia sự kiện
        /// </summary>
        [HttpPost("{id}/register")]
        public async Task<IActionResult> RegisterForEvent(int id)
        {
            try
            {
                var alumniIdClaim = User.FindFirst("AlumniId")?.Value;
                if (string.IsNullOrEmpty(alumniIdClaim))
                    return BadRequest(ApiResponse<object>.ErrorResponse("Alumni ID not found"));

                var alumniId = int.Parse(alumniIdClaim);
                var result = await _eventService.RegisterForEventAsync(id, alumniId);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Registered for event successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Xem danh sách người tham gia sự kiện
        /// </summary>
        [HttpGet("{id}/participants")]
        public async Task<IActionResult> GetParticipants(int id)
        {
            try
            {
                var result = await _eventService.GetEventParticipantsAsync(id);
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
