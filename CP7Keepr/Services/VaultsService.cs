namespace CP7Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault Create(Vault vaultData)
  {
    Vault vault = _repo.Create(vaultData);
    return vault;
  }

  internal List<Vault> Get(string userId)
  {
    List<Vault> vaults = _repo.Get();
    List<Vault> filtered = vaults.FindAll(f => f.IsPrivate == false || f.CreatorId == userId);
    return filtered;
  }
}
