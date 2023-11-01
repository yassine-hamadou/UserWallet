using System.ComponentModel.DataAnnotations;

namespace Contracts.Response;

public class WalletResponse
{
    [Required] public Guid Id { get; init; }

    [Required] public string Name { get; init; }

    [Required] public string Type { get; init; }

    [Key] [Required] public string AccountNumber { get; init; }

    [Required] public string AccountScheme { get; init; }

    [Required] public DateTime CreatedAt { get; init; }

    [Required] public string Owner { get; init; }

    public Guid UserId { get; init; }
}
