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

  internal List<Keep> Get(string id)
  {
    List<Keep> keeps = _repo.Get();
    return keeps;
  }

  internal Keep GetOne(int id, string userId)
  {
    Keep keep = _repo.GetOne(id);
    if (keep == null)
    {
      throw new Exception("No Keep at this id");
    }
    return keep;
  }
}
