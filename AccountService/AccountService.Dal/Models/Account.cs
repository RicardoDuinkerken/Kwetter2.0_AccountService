namespace AccountService.Dal.Models;

public class Account
{
    public long Id { get; set; }
    public long ProfileId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}