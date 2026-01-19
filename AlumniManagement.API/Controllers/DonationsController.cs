using AlumniManagement.Shared.DTOs.Donation;
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
    public class DonationsController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationsController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        /// <summary>
        /// Tạo quyên góp mới
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Alumni")]
        public async Task<IActionResult> Create([FromBody] CreateDonationRequest request)
        {
            try
            {
                var alumniIdClaim = User.FindFirst("AlumniId")?.Value;
                if (string.IsNullOrEmpty(alumniIdClaim))
                    return BadRequest(ApiResponse<object>.ErrorResponse("Alumni ID not found"));

                var alumniId = int.Parse(alumniIdClaim);
                var result = await _donationService.CreateDonationAsync(request, alumniId);
                return Ok(ApiResponse<DonationDto>.SuccessResponse(result, "Donation created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Lấy tất cả quyên góp (Admin only)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _donationService.GetAllDonationsAsync();
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Lấy lịch sử quyên góp của cựu sinh viên
        /// </summary>
        [HttpGet("alumni/{id}")]
        public async Task<IActionResult> GetByAlumni(int id)
        {
            try
            {
                var result = await _donationService.GetDonationsByAlumniAsync(id);
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
