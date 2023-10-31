using UserWalletApplication.Models;

namespace UserWalletApplication.Services.Wallet;

public class WalletService : IWalletService
{
    private readonly IUserRepository _userRepository;

    public WalletService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool WalletExists(Guid userId, string accountNumber, CancellationToken token = default)
    {
        var result = _userRepository.GetUserById(userId);
        if (result == null)
        {
            var walletExists = result?.Result.UserWallets;
            if (walletExists != null)
            {
                var wallet = walletExists.FirstOrDefault(ac => ac.AccountNumber == accountNumber);
            }

            return true;
        }

        return false;
    }

    public bool HasReachedWalletsLimit(Guid userId, CancellationToken token = default)
    {
        var wallets = _userRepository.GetUserById(userId);
        if (wallets == null) throw new Exception("User not found");
        var numberOfWallet = wallets.Result.UserWallets.Count;

        return numberOfWallet >= 5;
    }
}
