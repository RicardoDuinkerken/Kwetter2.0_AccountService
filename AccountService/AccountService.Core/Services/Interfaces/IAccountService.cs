﻿using AccountService.Dal.Models;

namespace AccountService.Core.Services.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<long> DeleteAccountAsync(long accountId);
}