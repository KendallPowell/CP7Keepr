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
      vaultkeepData.Creator = userInfo;
      return Ok(vaultkeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // [HttpDelete("{id}")]
  // [Authorize]
  // public async Task<ActionResult<string>> Remove(int id)
  // {
  //   try
  //   {
  //     Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
  //     string message = _vaultkeepsService.Remove(id, userInfo.Id);
  //     return Ok(message);
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }
}
