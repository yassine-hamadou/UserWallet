namespace UserWalletApplication.Services.Wallet;

public interface IWalletService
{
    bool HasReachedWalletsLimit(Guid userId, CancellationToken token = default);
    bool UserWalletExists(Guid userId, string accountNumber, CancellationToken token = default);
}
