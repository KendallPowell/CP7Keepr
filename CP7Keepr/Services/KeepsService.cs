namespace CP7Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;
  private readonly VaultKeepsService _vaultkeepsService;

  public KeepsService(KeepsRepository repo, VaultKeepsService vaultkeepsService)
  {
    _repo = repo;
    _vaultkeepsService = vaultkeepsService;
  }

  internal Keep Create(Keep keepData)
  {
    Keep keep = _repo.Create(keepData);
    return keep;
  }
}
