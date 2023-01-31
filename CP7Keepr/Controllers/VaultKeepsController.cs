namespace CP7Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly VaultKeepsService _vaultkeepsService;

  public VaultKeepsController(Auth0Provider auth0provider, VaultKeepsService vaultkeepsService)
  {
    _auth0provider = auth0provider;
    _vaultkeepsService = vaultkeepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultkeepData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultkeepData.CreatorId = userInfo.Id;
      VaultKeep vaultkeep = _vaultkeepsService.Create(vaultkeepData);
      vaultkeepData.KeepId = userInfo;
      return Ok(vaultkeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
