using Contracts.Request;
using Contracts.Response;
using UserWalletApplication.Models;

namespace UserWallet.Mappings;

public static class UserMapping
{
    public static User MapToUser(this CreateUserRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName
        };
    }

    public static UserResponse MapsToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            UserWallets = user.UserWallets
        };
    }
}
