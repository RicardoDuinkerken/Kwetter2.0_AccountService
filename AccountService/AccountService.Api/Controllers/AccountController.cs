using AccountService.Api.Mappers;
using AccountService.Core.Services.Interfaces;
using AccountService.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AccountService.Api.Controllers;

public class AccountController : Grpc.AccountService.AccountServiceBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }
    
    public override async Task<AccountResponse> CreateAccount(CreateAccountRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("CreateAccount invoked");
        
        try
        {
            return AccountMapper.AccountToAccountResponse(
                await _accountService.CreateAccountAsync(
                    AccountMapper.CreateAccountRequestToAccount(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
    public override async Task<AccountResponse> UpdateAccount(UpdateAccountRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("UpdateAccount invoked");
        
        try
        {
            return AccountMapper.AccountToAccountResponse(
                await _accountService.UpdateAccountAsync(
                    AccountMapper.UpdateAccountRequestToAccount(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
    public override async Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("UpdateAccount invoked");
        
        try
        {
            return AccountMapper.AccountToDeleteAccountResponse(
                await _accountService.DeleteAccountAsync(
                    AccountMapper.DeleteAccountRequestToId(request)));
        }
        catch (Exception e)
        {
            _logger.LogError("{E}", e);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
    
}