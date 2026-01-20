using AlumniManagement.Shared.DTOs.Alumni;
using AlumniManagement.Shared.DTOs.Common;
using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Entities;
using AlumniManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Services
{
    public class AlumniService : IAlumniService
    {
        private readonly IAlumniRepository _alumniRepository;

        public AlumniService(IAlumniRepository alumniRepository)
        {
            _alumniRepository = alumniRepository;
        }

        public async Task<PagedResult<AlumniDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var alumni = await _alumniRepository.SearchAsync(null, null, null, pageNumber, pageSize);
            var totalCount = await _alumniRepository.GetSearchCountAsync(null, null, null);

            return new PagedResult<AlumniDto>
            {
                Items = alumni.Select(MapToDto),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<AlumniDto> GetByIdAsync(int id)
        {
            var alumni = await _alumniRepository.GetByIdAsync(id);
            if (alumni == null)
                throw new InvalidOperationException("Alumni not found");

            return MapToDto(alumni);
        }

        public async Task<AlumniDto> CreateAsync(CreateAlumniRequest request)
        {
            // Validate student code
            var existingCode = await _alumniRepository.GetByStudentCodeAsync(request.StudentCode);
            if (existingCode != null)
                throw new InvalidOperationException("Student code already exists");

            // Validate email
            var existingEmail = await _alumniRepository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
                throw new InvalidOperationException("Email already exists");

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
                CurrentJob = request.CurrentJob,
                Company = request.Company,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var created = await _alumniRepository.AddAsync(alumni);
            return MapToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAlumniRequest request)
        {
            var alumni = await _alumniRepository.GetByIdAsync(id);
            if (alumni == null)
                throw new InvalidOperationException("Alumni not found");

            if (!string.IsNullOrEmpty(request.FullName))
                alumni.FullName = request.FullName;

            if (request.DateOfBirth.HasValue)
                alumni.DateOfBirth = request.DateOfBirth.Value;

            if (!string.IsNullOrEmpty(request.Gender))
                alumni.Gender = request.Gender;

            if (!string.IsNullOrEmpty(request.Phone))
                alumni.Phone = request.Phone;

            if (!string.IsNullOrEmpty(request.CurrentJob))
                alumni.CurrentJob = request.CurrentJob;

            if (!string.IsNullOrEmpty(request.Company))
                alumni.Company = request.Company;

            if (!string.IsNullOrEmpty(request.Address))
                alumni.Address = request.Address;

            alumni.UpdatedAt = DateTime.Utc.Now;

            await _alumniRepository.UpdateAsync(alumni);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alumni = await _alumniRepository.GetByIdAsync(id);
            if (alumni == null)
                throw new InvalidOperationException("Alumni not found");

            alumni.IsActive = false;
            alumni.UpdatedAt = DateTime.Now;

            await _alumniRepository.UpdateAsync(alumni);
            return true;
        }

        public async Task<PagedResult<AlumniDto>> SearchAsync(AlumniSearchRequest request)
        {
            var alumni = await _alumniRepository.SearchAsync(
                request.Keyword,
                request.GraduationYear,
                request.Major,
                request.PageNumber,
                request.PageSize);

            var totalCount = await _alumniRepository.GetSearchCountAsync(
                request.Keyword,
                request.GraduationYear,
                request.Major);

            return new PagedResult<AlumniDto>
            {
                Items = alumni.Select(MapToDto),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }

        public async Task<IEnumerable<AlumniDto>> GetByGraduationYearAsync(int year)
        {
            var alumni = await _alumniRepository.GetByGraduationYearAsync(year);
            return alumni.Select(MapToDto);
        }

        public async Task<IEnumerable<AlumniDto>> GetByMajorAsync(string major)
        {
            var alumni = await _alumniRepository.GetByMajorAsync(major);
            return alumni.Select(MapToDto);
        }

        private AlumniDto MapToDto(Alumni alumni)
        {
            return new AlumniDto
            {
                AlumniId = alumni.AlumniId,
                StudentCode = alumni.StudentCode,
                FullName = alumni.FullName,
                DateOfBirth = alumni.DateOfBirth,
                Gender = alumni.Gender,
                Email = alumni.Email,
                Phone = alumni.Phone,
                GraduationYear = alumni.GraduationYear,
                Major = alumni.Major,
                CurrentJob = alumni.CurrentJob,
                Company = alumni.Company,
                Address = alumni.Address,
                IsActive = alumni.IsActive,
                CreatedAt = alumni.CreatedAt,
                UpdatedAt = alumni.UpdatedAt
            };
        }
    }
}