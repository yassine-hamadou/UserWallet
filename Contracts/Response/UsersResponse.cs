using System.ComponentModel.DataAnnotations;

namespace Contracts.Response;

public class UsersResponse
{
    [Required] public IEnumerable<UserResponse> Users { get; init; } = Enumerable.Empty<UserResponse>();
}
