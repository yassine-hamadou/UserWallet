namespace UserWalletApplication.Services.Wallet;

public interface IWalletService
{
    bool HasReachedWalletsLimit(Guid userId, CancellationToken token = default);
    bool WalletExists(Guid userId, string accountNumber, CancellationToken token = default);
}
