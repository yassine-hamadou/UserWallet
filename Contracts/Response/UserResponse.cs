using UserWalletApplication.Models;

namespace Contracts.Response;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public List<Wallet> UserWallets { get; set; } = new();
}
