using Microsoft.EntityFrameworkCore;
using UserWalletApplication.Database;
using UserWalletApplication.Models;

// namespace UserWalletApplication.Repository.Wallet;

public class WalletRepository : IWalletRepository
{
    private readonly MyDbContext _context;

    public WalletRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Wallet>> GetWalletsAsync(CancellationToken token = default)
    {
        var wallet = await _context.Wallets
            .OrderBy(createdAt => createdAt.CreatedAt).ToListAsync(token);
        return wallet;
    }

    public async Task<Wallet> GetWalletByWalletId(Guid walletId, CancellationToken token = default)
    {
        var result = await _context.Wallets
            .FirstOrDefaultAsync(wallet => wallet.Id == walletId, token);
        return result ?? throw new InvalidOperationException();
    }

    public async Task<bool> CreateWallet(Wallet wallet, CancellationToken token = default)
    {
        var newWallet = new Wallet
        {
            Id = Guid.NewGuid(),
            Name = wallet.Name,
            Type = wallet.Type,
            AccountNumber = wallet.AccountNumber.Substring(0, 6),
            AccountScheme = wallet.AccountScheme,
            CreatedAt = DateTime.UtcNow,
            Owner = wallet.Owner,
            UserId = wallet.UserId
        };
        await _context.AddAsync(newWallet, token);
        return await Save();
    }

    public async Task<bool> UpdateWallet(Wallet wallet, CancellationToken token = default)
    {
        var result = await _context.Wallets
            .FirstOrDefaultAsync(id => id.Id == wallet.Id, token);
        if (result != null)
        {
            result.Name = wallet.Name;
            result.Type = wallet.Type;
            result.AccountNumber = wallet.AccountNumber;
            result.AccountScheme = wallet.AccountScheme;
            result.CreatedAt = DateTime.UtcNow;
            result.Owner = wallet.Owner;
            result.UserId = wallet.UserId;
        }

        return await Save();
    }

    public async Task<bool> DeleteWallet(Guid walletId, CancellationToken token = default)
    {
        var result = await _context.Wallets
            .FirstOrDefaultAsync(id => id.Id == walletId, token);
        if (result == null) return false; // Wallet not found or already deleted
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> WalletExists(Guid id, CancellationToken token = default)
    {
        var wallet = await _context.Wallets
            .AnyAsync(w => w.Id == id, token);
        return wallet;
    }

    public async Task<bool> WalletExists(string accountNumber, CancellationToken token = default)
    {
        var wallet = await _context.Wallets
            .AnyAsync(an => an.AccountNumber == accountNumber, token);
        return wallet;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved = await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}
