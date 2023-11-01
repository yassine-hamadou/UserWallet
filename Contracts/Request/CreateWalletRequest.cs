using System.ComponentModel.DataAnnotations;

namespace Contracts.Request;

public class CreateWalletRequest
{
    [Required] public string Name { get; set; }

    [Required] public string Type { get; set; }

    [Key] [Required] public string AccountNumber { get; set; }

    [Required] public string AccountScheme { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public string Owner { get; set; }

    public Guid UserId { get; set; }
}
