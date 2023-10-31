namespace UserWalletApplication.Models;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetWalletsAsync(CancellationToken token = default);
    Task<Wallet> GetWalletByWalletId(Guid walletId, CancellationToken token = default);
    Task<bool> CreateWallet(Wallet wallet, CancellationToken token = default);
    Task<bool> UpdateWallet(Wallet wallet, CancellationToken token = default);
    Task<bool> DeleteWallet(Guid walletId, CancellationToken token = default);
    Task<bool> WalletExists(Guid walletId, CancellationToken token = default);
    Task<bool> WalletExists(string accountNumber, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}
