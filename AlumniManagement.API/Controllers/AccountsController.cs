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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        [HttpPut("{id}/lock")]
        public async Task<IActionResult> LockAccount(int id)
        {
            try
            {
                var result = await _accountService.LockAccountAsync(id);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Account locked successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Mở khóa tài khoản
        /// </summary>
        [HttpPut("{id}/unlock")]
        public async Task<IActionResult> UnlockAccount(int id)
        {
            try
            {
                var result = await _accountService.UnlockAccountAsync(id);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Account unlocked successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}