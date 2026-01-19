using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> LockAccountAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new InvalidOperationException("Account not found");

            account.IsLocked = true;
            await _accountRepository.UpdateAsync(account);
            return true;
        }

        public async Task<bool> UnlockAccountAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new InvalidOperationException("Account not found");

            account.IsLocked = false;
            await _accountRepository.UpdateAsync(account);
            return true;
        }
    }
}