using Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using UserWallet.Mappings;
using UserWalletApplication.Models;

namespace UserWallet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //GET all Users
    [HttpGet] // "api/users
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var user = await _userRepository.GetUserAsync(token);

        return Ok(user);
    }

    //GET UserById
    [HttpGet("{id}")] // "api/users/{id}
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetUserById(id, token);
        if (user == null) return NotFound();
        return Ok(user);
    }


    //POST User
    [HttpPost] // "api/users
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token)
    {
        if (request == null) return BadRequest("User data is invalid.");
        if (!ModelState.IsValid) return BadRequest("Validation failed.");

        var mapToUser = request.MapToUser();
        await _userRepository.CreateUser(mapToUser ?? throw new InvalidOperationException(), token);

        return CreatedAtAction(nameof(Get), new { id = mapToUser.Id }, mapToUser.MapsToResponse());
    }


    //DELETE User
    [HttpDelete("{id}")] // "api/users/{id}
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _userRepository.UserExists(id);
        var deleteUser = await _userRepository.DeleteUser(id, token);
        if (!deleteUser)
            return NotFound();
        return Ok();
    }
}