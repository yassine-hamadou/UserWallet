using Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using UserWallet.Mappings;
using UserWalletApplication.Models;
using UserWalletApplication.Services.Wallet;

namespace UserWallet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;

    public WalletsController(IWalletRepository walletRepository, IWalletService walletService)
    {
        _walletRepository = walletRepository;
        _walletService = walletService;
    }

    //GET all Wallets
    [HttpGet] // "api/wallets
    public async Task<IActionResult> GetWallets(CancellationToken token)
    {
        var wallets = await _walletRepository.GetWalletsAsync(token);

        return Ok(wallets.MapsToResponse());
    }

    //GET WalletById
    [HttpGet("{id}")] // "api/wallets/{id}
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var wallet = await _walletRepository.GetWalletByWalletId(id, token);
        if (wallet == null)
            return NotFound("Wallet not found.");
        return Ok(wallet.MapsToResponse());
    }

    //POST Wallet
    [HttpPost] // "api/wallets
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request,
        CancellationToken token)
    {
        if (request == null)
            return BadRequest("Wallet data is invalid.");
        if (!ModelState.IsValid)
            return BadRequest("Validation failed.");

        var maxedWalletsReached = _walletService.HasReachedWalletsLimit(request.UserId);
        var doesWalletExists = _walletService.UserWalletExists(request.UserId, request.AccountNumber);
        if (maxedWalletsReached is false)
        {
            if (doesWalletExists is false)
            {
                var mapToWallet = request.MapToWallet();
                await _walletRepository.CreateWallet(mapToWallet ?? throw new InvalidOperationException(),
                    token);

                return CreatedAtAction(nameof(Get), new { id = mapToWallet.Id }, mapToWallet.MapsToResponse());
            }

            return BadRequest($"Wallet already exists. {request.AccountNumber} ");
        }


        return BadRequest("Wallets limit reached.");
    }


    //DELETE Wallet
    [HttpDelete("{id:guid}")] // "api/wallets/{id}
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _walletRepository.WalletExists(id, token);
        var deleteWallet = await _walletRepository.DeleteWallet(id, token);
        if (!deleteWallet)
            return NotFound("Wallet not found.");

        return Ok("Wallet successfully deleted.");
    }
}
