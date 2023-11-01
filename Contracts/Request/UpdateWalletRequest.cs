using System.ComponentModel.DataAnnotations;

namespace Contracts.Request;

public class UpdateWalletRequest
{
    [Required] public Guid Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Type { get; set; }

    [Key] [Required] public string AccountNumber { get; set; }

    [Required] public string AccountScheme { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    [Required] public string Owner { get; set; }

    public Guid UserId { get; set; }
}
