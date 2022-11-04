using AccountService.Dal.Models;

namespace AccountService.Core.Services.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<long> DeleteAccountAsync(long accountId);
    Task<Account> ChangeUsernameAsync(Account account);
    Task<bool> CheckAvailableUsernameAsync(string username);
}