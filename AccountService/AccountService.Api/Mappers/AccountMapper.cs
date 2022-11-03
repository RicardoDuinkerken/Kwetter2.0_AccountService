using AccountService.Dal.Models;
using AccountService.Grpc;
namespace AccountService.Api.Mappers;

public static class AccountMapper
{
    public static Account CreateAccountRequestToAccount(CreateAccountRequest request)
    {
        return new()
        {
            Email = request.Email,
            Username = request.Username
        };
    }
    
    public static Account UpdateAccountRequestToAccount(UpdateAccountRequest request)
    {
        return new()
        {
            Id = request.Id,
            ProfileId = request.ProfileId,
            Email = request.Email,
            Username = request.Username
        };
    }

    public static AccountResponse AccountToAccountResponse(Account account)
    {
        return new()
        {
            Id = account.Id,
            ProfileId = account.ProfileId,
            Email = account.Email,
            Username = account.Username
        };
    }

    public static long DeleteAccountRequestToId(DeleteAccountRequest request)
    {
        return request.Id;
    }

    public static DeleteAccountResponse AccountToDeleteAccountResponse(long accountId)
    {
        return new()
        {
            Id = accountId
        };
    }

    public static Account ChangeUsernameRequestToAccount(ChangeUsernameRequest request)
    {
        return new Account()
        {
            Id = request.Id,
            Username = request.Username
        };
    }


}