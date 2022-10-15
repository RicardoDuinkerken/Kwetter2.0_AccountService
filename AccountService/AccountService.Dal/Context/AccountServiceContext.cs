using AccountService.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Dal.Context;

public class AccountServiceContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public AccountServiceContext(DbContextOptions<AccountServiceContext> options) : base(options)
    {
                
    }
}