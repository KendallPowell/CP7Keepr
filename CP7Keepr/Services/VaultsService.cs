namespace CP7Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;
  private readonly KeepsRepository _keepsRepository;

  public VaultsService(VaultsRepository repo, KeepsRepository keepsRepository)
  {
    _repo = repo;
    _keepsRepository = keepsRepository;
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

  internal Vault GetOne(int id, string userId)
  {
    Vault vault = _repo.GetOne(id);
    if (vault == null)
    {
      throw new Exception($"No Vault at this id:{id}");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("This Vault does not belong to you.");
    }
    return vault;
  }

  internal Vault Update(Vault updateData, string userId)
  {
    Vault original = GetOne(updateData.Id, userId);
    if (original.CreatorId != updateData.CreatorId) throw new Exception("This does not belong to you");
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;

    _repo.Update(original);
    return original;
  }

  internal string Remove(int id, string userId)
  {
    Vault original = GetOne(id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("This Vault does not belong to you..");
    }
    _repo.Remove(id);
    return $"{original.Name} is no longer a thing";
  }

  internal List<Vault> GetVaultsInProfile(string id)
  {
    List<Vault> vaults = _repo.GetVaultsInProfile(id);
    List<Vault> filtered = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == id);
    return vaults;
  }
}
