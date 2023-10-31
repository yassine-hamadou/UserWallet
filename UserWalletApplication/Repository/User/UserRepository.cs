using Microsoft.EntityFrameworkCore;
using UserWalletApplication.Database;

namespace UserWalletApplication.Models;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUserAsync(CancellationToken token = default)
    {
        var users = await _context.Users
            .Include(wallets => wallets.UserWallets)
            .ToListAsync(token);
        return users;
    }

    public async Task<User> GetUserById(Guid id, CancellationToken token = default)
    {
        var result = await _context.Users
            .Include(wallets => wallets.UserWallets)
            .FirstOrDefaultAsync(c => c.Id == id, token);
        return result ?? throw new InvalidOperationException();
    }


    public async Task<bool> CreateUser(User user, CancellationToken token = default)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName
        };
        await _context.AddAsync(newUser, token);
        return await Save(token);
    }

    public async Task<bool> UpdateUser(User user, CancellationToken token = default)
    {
        var result = await _context.Users.FirstOrDefaultAsync(p =>
            p.Id == user.Id, token);

        if (result != null)
        {
            result.PhoneNumber = user.PhoneNumber;
            result.UserName = user.UserName;
        }

        return await Save(token);
    }

    public async Task<bool> DeleteUser(Guid id, CancellationToken token = default)
    {
        var result = await _context.Users.FirstOrDefaultAsync(i =>
            i.Id == id, token);

        if (result == null) return false;
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> UserExists(Guid id, CancellationToken token = default)
    {
        var user = await _context.Users.AnyAsync(c => c.Id == id, token);
        return user;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved = await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}
