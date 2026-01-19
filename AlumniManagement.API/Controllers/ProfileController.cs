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
    [Authorize(Roles = "Alumni")]
    public class ProfileController : ControllerBase
    {
        private readonly IAlumniService _alumniService;

        public ProfileController(IAlumniService alumniService)
        {
            _alumniService = alumniService;
        }

        /// <summary>
        /// Xem hồ sơ cá nhân
        /// </summary>
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var alumniIdClaim = User.FindFirst("AlumniId")?.Value;
                if (string.IsNullOrEmpty(alumniIdClaim))
                    return BadRequest(ApiResponse<object>.ErrorResponse("Alumni ID not found"));

                var alumniId = int.Parse(alumniIdClaim);
                var result = await _alumniService.GetByIdAsync(alumniId);
                return Ok(ApiResponse<AlumniDto>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật hồ sơ cá nhân
        /// </summary>
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateAlumniRequest request)
        {
            try
            {
                var alumniIdClaim = User.FindFirst("AlumniId")?.Value;
                if (string.IsNullOrEmpty(alumniIdClaim))
                    return BadRequest(ApiResponse<object>.ErrorResponse("Alumni ID not found"));

                var alumniId = int.Parse(alumniIdClaim);
                var result = await _alumniService.UpdateAsync(alumniId, request);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Profile updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
