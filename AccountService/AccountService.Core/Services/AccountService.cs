using AccountService.Core.Services.Interfaces;
using AccountService.Dal.Context;
using AccountService.Dal.Models;
using GenericDal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Core.Services;

public class AccountService : IAccountService
{
    private ILogger<AccountService> _logger;
    private readonly IAsyncRepository<Account, long> _accountRepository;

    public AccountService(ILogger<AccountService> logger, IAsyncRepository<Account, long> accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
    }
    
    public async Task<Account> CreateAccountAsync(Account account)
    {
        account = await _accountRepository.CreateAsync(account);
        _logger.LogInformation("Account created with id: {Id}", account.Id);
        
        return account;
    }
    
    public async Task<Account> UpdateAccountAsync(Account account)
    {
        account = await _accountRepository.UpdateAsync(account);
        _logger.LogInformation("Account updated with id: {Id}", account.Id);
        
        return account;
    }

    public async Task<long> DeleteAccountAsync(long accountId)
    {
        if (!await _accountRepository.DeleteAsync(accountId))
            throw new ArgumentException($"No account found with id: {accountId}");
        
        return accountId;
    }
}