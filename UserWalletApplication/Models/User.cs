using System.ComponentModel.DataAnnotations;

namespace UserWalletApplication.Models;

public class User
{
    public Guid Id { get; set; }

    [Required] public string UserName { get; set; }

    [Key] [Required] public string PhoneNumber { get; set; }

    public List<Wallet> UserWallets { get; set; } = new();
}
