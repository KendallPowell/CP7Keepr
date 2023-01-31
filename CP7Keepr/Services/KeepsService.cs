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

  internal Keep Update(Keep updateData)
  {
    Keep original = GetOne(updateData.Id, updateData.CreatorId);
    if (original.CreatorId != updateData.CreatorId) throw new Exception("This is not yours to edit");
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;

    _repo.Update(original);
    return original;
  }

  internal string Remove(int id, string userId)
  {
    Keep keep = GetOne(id, userId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("This Keep does not belong to you");
    }
    _repo.Delete(id);
    return $"'{keep.Name}' has been taken care of";
  }
}
