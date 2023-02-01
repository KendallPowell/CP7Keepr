namespace CP7Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountsService;
    private readonly Auth0Provider _auth0provider;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public ProfilesController(AccountService accountsService, Auth0Provider auth0provider, VaultsService vaultsService, KeepsService keepsService)
    {
      _accountsService = accountsService;
      _auth0provider = auth0provider;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetProfileById(string id)
    {
      Account account = _accountsService.GetProfileById(id);
      return Ok(account);
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetProfilesKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetKeepsInProfile(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetProfilesVaults(string id)
    {
      try
      {
        Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
        List<Vault> vaults = _vaultsService.GetVaultsInProfile(id, userInfo?.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}