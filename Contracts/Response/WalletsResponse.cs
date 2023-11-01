using System.ComponentModel.DataAnnotations;

namespace Contracts.Response;

public class WalletsResponse
{
    [Required] public IEnumerable<WalletResponse> Wallet { get; init; } = Enumerable.Empty<WalletResponse>();
}
