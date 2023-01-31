namespace CP7Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0provider;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth0provider)
  {
    _vaultsService = vaultsService;
    _auth0provider = auth0provider;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.Create(vaultData);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Vault>> GetOne(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetOne(id, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/keeps")]
  public async Task<ActionResult<List<Keep>>> GetKeepsInVault(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keep = _vaultsService.GetKeeps(id, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault updateData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      updateData.CreatorId = userInfo.Id;
      updateData.Id = id;
      Vault vault = _vaultsService.Update(updateData, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Remove(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.Remove(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
