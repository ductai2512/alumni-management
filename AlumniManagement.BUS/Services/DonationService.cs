using AlumniManagement.Shared.DTOs.Donation;
using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Entities;
using AlumniManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IAlumniRepository _alumniRepository;

        public DonationService(
            IDonationRepository donationRepository,
            IAlumniRepository alumniRepository)
        {
            _donationRepository = donationRepository;
            _alumniRepository = alumniRepository;
        }

        public async Task<DonationDto> CreateDonationAsync(CreateDonationRequest request, int alumniId)
        {
            var alumni = await _alumniRepository.GetByIdAsync(alumniId);
            if (alumni == null)
                throw new InvalidOperationException("Alumni not found");

            var donation = new Donation
            {
                AlumniId = alumniId,
                Amount = request.Amount,
                DonationDate = DateTime.Now,
                Note = request.Note
            };

            var created = await _donationRepository.AddAsync(donation);

            return new DonationDto
            {
                DonationId = created.DonationId,
                AlumniId = created.AlumniId,
                AlumniName = alumni.FullName,
                Amount = created.Amount,
                DonationDate = created.DonationDate,
                Note = created.Note
            };
        }

        public async Task<IEnumerable<DonationDto>> GetAllDonationsAsync()
        {
            var donations = await _donationRepository.GetAllAsync();
            var result = new List<DonationDto>();

            foreach (var donation in donations)
            {
                var alumni = await _alumniRepository.GetByIdAsync(donation.AlumniId);
                result.Add(new DonationDto
                {
                    DonationId = donation.DonationId,
                    AlumniId = donation.AlumniId,
                    AlumniName = alumni?.FullName,
                    Amount = donation.Amount,
                    DonationDate = donation.DonationDate,
                    Note = donation.Note
                });
            }

            return result.OrderByDescending(d => d.DonationDate);
        }

        public async Task<IEnumerable<DonationDto>> GetDonationsByAlumniAsync(int alumniId)
        {
            var donations = await _donationRepository.GetByAlumniIdAsync(alumniId);
            var alumni = await _alumniRepository.GetByIdAsync(alumniId);

            return donations.Select(d => new DonationDto
            {
                DonationId = d.DonationId,
                AlumniId = d.AlumniId,
                AlumniName = alumni?.FullName,
                Amount = d.Amount,
                DonationDate = d.DonationDate,
                Note = d.Note
            });
        }

        public async Task<decimal> GetTotalDonationsAsync()
        {
            return await _donationRepository.GetTotalDonationsAsync();
        }

        public async Task<decimal> GetTotalDonationsByYearAsync(int year)
        {
            return await _donationRepository.GetTotalDonationsByYearAsync(year);
        }
    }
}
