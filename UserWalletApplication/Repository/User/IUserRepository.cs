namespace UserWalletApplication.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUserAsync(CancellationToken token = default);
    Task<User> GetUserById(Guid id, CancellationToken token = default);
    Task<bool> CreateUser(User user, CancellationToken token = default);
    Task<bool> UpdateUser(User user, CancellationToken token = default);
    Task<bool> DeleteUser(Guid id, CancellationToken token = default);
    Task<bool> UserExists(Guid id, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}
