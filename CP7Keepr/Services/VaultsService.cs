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

  internal Vault GetOne(int id)
  {
    Vault vault = _repo.GetOne(id);
    if (vault == null)
    {
      throw new Exception($"No Vault at this id:{id}");
    }
    return vault;
  }
}
