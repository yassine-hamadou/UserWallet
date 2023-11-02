using UserWalletApplication.Models;

namespace UserWalletApplication.Services.Wallet;

public class WalletService : IWalletService
{
    private readonly IUserRepository _userRepository;

    public WalletService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool UserWalletExists(Guid userId, string accountNumber, CancellationToken token = default)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null)
        {
            var walletExists = user?.Result.UserWallets;
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
        var user = _userRepository.GetUserById(userId);
        if (user == null) throw new Exception("User not found");
        var numberOfWallet = user.Result.UserWallets.Count;

        return numberOfWallet >= 5;
    }
}
