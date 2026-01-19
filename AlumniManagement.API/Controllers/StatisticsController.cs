using AlumniManagement.Shared.DTOs.Common;
using AlumniManagement.BUS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AlumniManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Thống kê số lượng cựu sinh viên theo năm tốt nghiệp
        /// </summary>
        [HttpGet("alumni-by-year")]
        public async Task<IActionResult> GetAlumniByYear()
        {
            try
            {
                var result = await _statisticsService.GetAlumniByYearAsync();
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Thống kê số lượng cựu sinh viên theo ngành
        /// </summary>
        [HttpGet("alumni-by-major")]
        public async Task<IActionResult> GetAlumniByMajor()
        {
            try
            {
                var result = await _statisticsService.GetAlumniByMajorAsync();
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Thống kê top công ty có nhiều cựu sinh viên nhất
        /// </summary>
        [HttpGet("top-companies")]
        public async Task<IActionResult> GetTopCompanies([FromQuery] int top = 10)
        {
            try
            {
                var result = await _statisticsService.GetTopCompaniesAsync(top);
                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Tổng tiền quyên góp toàn hệ thống
        /// </summary>
        [HttpGet("total-donations")]
        public async Task<IActionResult> GetTotalDonations()
        {
            try
            {
                var result = await _statisticsService.GetTotalDonationsAsync();
                return Ok(ApiResponse<decimal>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}