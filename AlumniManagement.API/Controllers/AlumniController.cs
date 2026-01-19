using AlumniManagement.Shared.DTOs.Alumni;
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
    [Authorize]
    public class AlumniController : ControllerBase
    {
        private readonly IAlumniService _alumniService;

        public AlumniController(IAlumniService alumniService)
        {
            _alumniService = alumniService;
        }

        /// <summary>
        /// Lấy danh sách tất cả cựu sinh viên (có phân trang)
        /// </summary>
        [HttpGet("ListAllAlumni")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _alumniService.GetAllAsync(pageNumber, pageSize);
                return Ok(ApiResponse<PagedResult<AlumniDto>>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Lấy thông tin cựu sinh viên theo ID
        /// </summary>
        [HttpGet("GetAlumniById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _alumniService.GetByIdAsync(id);
                return Ok(ApiResponse<AlumniDto>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Thêm cựu sinh viên mới (Admin only)
        /// </summary>
        [HttpPost("AddAlumni")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAlumniRequest request)
        {
            try
            {
                var result = await _alumniService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.AlumniId }, 
                    ApiResponse<AlumniDto>.SuccessResponse(result, "Alumni created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin cựu sinh viên (Admin only)
        /// </summary>
        [HttpPut("UpdateAlumni/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAlumniRequest request)
        {
            try
            {
                var result = await _alumniService.UpdateAsync(id, request);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Alumni updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Xóa cựu sinh viên (soft delete) (Admin only)
        /// </summary>
        [HttpDelete("DeleteAlumni/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _alumniService.DeleteAsync(id);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Alumni deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Tìm kiếm cựu sinh viên
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] AlumniSearchRequest request)
        {
            try
            {
                var result = await _alumniService.SearchAsync(request);
                return Ok(ApiResponse<PagedResult<AlumniDto>>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Lọc theo năm tốt nghiệp
        /// </summary>
        [HttpGet("searchByYear")]
        public async Task<IActionResult> Filter([FromQuery] int? graduationYear, [FromQuery] string major)
        {
            try
            {
                if (graduationYear.HasValue)
                {
                    var result = await _alumniService.GetByGraduationYearAsync(graduationYear.Value);
                    return Ok(ApiResponse<object>.SuccessResponse(result));
                }
                else if (!string.IsNullOrEmpty(major))
                {
                    var result = await _alumniService.GetByMajorAsync(major);
                    return Ok(ApiResponse<object>.SuccessResponse(result));
                }

                return BadRequest(ApiResponse<object>.ErrorResponse("Please provide graduationYear or major"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}