using Contracts.Request;
using Contracts.Response;
using UserWalletApplication.Models;

namespace UserWallet.Mappings;

public static class WalletMapping
{
    public static Wallet
        MapToWallet(this CreateWalletRequest request)
    {
        return new Wallet
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Type = request.Type,
            AccountNumber = request.AccountNumber.Substring(0, 6),
            AccountScheme = request.AccountScheme,
            CreatedAt = request.CreatedAt,
            Owner = request.Owner,
            UserId = request.UserId
        };
    }

    public static Wallet
        MapToWallet(this UpdateWalletRequest request,
            Guid id)
    {
        return new Wallet
        {
            Id = id,
            Name = request.Name,
            Type = request.Type,
            AccountNumber = request.AccountNumber,
            AccountScheme = request.AccountScheme,
            CreatedAt = request.CreatedAt,
            Owner = request.Owner,
            UserId = request.UserId
        };
    }

    public static WalletResponse
        MapsToResponse(this Wallet wallet)
    {
        return new WalletResponse
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Type = wallet.Type,
            AccountNumber = wallet.AccountNumber,
            AccountScheme = wallet.AccountScheme,
            CreatedAt = wallet.CreatedAt,
            Owner = wallet.Owner,
            UserId = wallet.UserId
        };
    }

    public static WalletsResponse
        MapsToResponse(
            this IEnumerable<Wallet> wallets)
    {
        return new WalletsResponse
        {
            Wallet = wallets.Select(MapsToResponse)
        };
    }
}
