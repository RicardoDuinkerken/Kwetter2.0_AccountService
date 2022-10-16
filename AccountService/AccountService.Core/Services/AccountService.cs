using AccountService.Dal.Context;
using AccountService.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Core.Services;

public class AccountService : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public AccountService(DbContextOptions<AccountServiceContext> options) : base(options)
    {
        
    }
}