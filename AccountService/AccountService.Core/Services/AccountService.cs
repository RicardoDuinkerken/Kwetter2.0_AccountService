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
        account.ProfileId = -1;
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
    
    public async Task<Account> ChangeUsernameAsync(Account account)
    {
        Account accountToChange = await _accountRepository.GetByIdAsync(account.Id);
        accountToChange.Username = account.Username;
        
        await _accountRepository.UpdateAsync(accountToChange);
        _logger.LogInformation("Username changed for account with id: {Id}", account.Id);
        
        return accountToChange;
    }

    public async Task<bool> CheckAvailableUsernameAsync(string username)
    {
        var accounts = await _accountRepository.GetWhereAsync(a => a.Username == username);
        bool available = accounts.Count == 0;
        _logger.LogInformation("checked if {Username} is available: {Available}", username, available);
        
        return available;
    }
    
    public async Task<bool> HasProfileAsync(long accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        bool hasProfile = account.ProfileId != -1;

        _logger.LogInformation("checked if {Username} with id {ID} has profile {HasProfile}", account.Username, accountId, hasProfile);
        return hasProfile;
    }
}