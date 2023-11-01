using System.ComponentModel.DataAnnotations;
using UserWalletApplication.Models;

namespace Contracts.Request;

public class UpdateUserRequest
{
    [Required] public string UserName { get; set; }

    [Key] [Required] public string PhoneNumber { get; set; }

    public List<Wallet> UserWallets { get; set; } = new();
}
