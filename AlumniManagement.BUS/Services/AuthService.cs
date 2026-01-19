using AlumniManagement.Shared.DTOs.Auth;
using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Entities;
using AlumniManagement.DAL.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace AlumniManagement.BUS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAlumniRepository _alumniRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IAccountRepository accountRepository,
            IAlumniRepository alumniRepository,
            ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _alumniRepository = alumniRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var username = request.Username?.Trim();
            var account = await _accountRepository.GetByUsernameAsync(username);
            if (account == null)
                throw new UnauthorizedAccessException("Invalid  or password");

            if (account.IsLocked)
                throw new UnauthorizedAccessException("Account is locked");
            var hash = BCrypt.Net.BCrypt.HashPassword("Admin123@");
            var verify = BCrypt.Net.BCrypt.Verify("Admin123@", hash);

            Console.WriteLine(hash);
            Console.WriteLine(verify); // PHẢI TRUE
            if (!VerifyPassword(request.Password, account.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password");

            var token = _tokenService.GenerateToken(account);

            return new LoginResponse
            {
                Token = token,
                Role = account.Role,
                AlumniId = account.AlumniId,
                FullName = account.Alumni?.FullName
            };
        }
        

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // 1. Check username
                if (await _accountRepository.GetByUsernameAsync(request.Username) != null)
                    throw new InvalidOperationException("Username already exists");

                // 2. Check student code
                if (await _alumniRepository.GetByStudentCodeAsync(request.StudentCode) != null)
                    throw new InvalidOperationException("Student code already exists");

                // 3. Check email
                if (await _alumniRepository.GetByEmailAsync(request.Email) != null)
                    throw new InvalidOperationException("Email already exists");

                // 4. Create Alumni
                var alumni = new Alumni
                {
                    StudentCode = request.StudentCode,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    Email = request.Email,
                    Phone = request.Phone,
                    GraduationYear = request.GraduationYear,
                    Major = request.Major,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = true,
                    Address = request.Address,
                    Company = request.Company,        
                    CurrentJob = request.CurrentJob

                };

                var createdAlumni = await _alumniRepository.AddAsync(alumni);

                // ⚠️ CỰC KỲ QUAN TRỌNG
                if (createdAlumni.AlumniId <= 0)
                    throw new Exception("Failed to create Alumni");

                // 5. Create Account
                var newaccount = new Account
                {
                    Username = request.Username,
                    PasswordHash = HashPassword(request.Password),
                    Role = "Alumni",
                    AlumniId = createdAlumni.AlumniId,
                    IsLocked = false,
                    CreatedAt = DateTime.Now
                };

                await _accountRepository.AddAsync(newaccount);

                return true;
            }
            catch (DbUpdateException ex)
            {
                // 🔥 DÒNG QUYẾT ĐỊNH
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> ChangePasswordAsync(int accountId, ChangePasswordRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new InvalidOperationException("Account not found");

            if (!VerifyPassword(request.OldPassword, account.PasswordHash))
                throw new UnauthorizedAccessException("Old password is incorrect");

            account.PasswordHash = HashPassword(request.NewPassword);
            await _accountRepository.UpdateAsync(account);

            return true;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
