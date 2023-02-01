namespace CP7Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep Create(VaultKeep vaultkeepData)
  {
    Vault vault = _vaultsService.GetOne(vaultkeepData.VaultId, vaultkeepData.CreatorId);
    VaultKeep vaultor = _repo.Create(vaultkeepData);
    return vaultor;
  }

  internal List<VaultKeeps> GetKeeps(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetOne(vaultId, userId);
    List<VaultKeeps> keeps = _repo.GetKeeps(vaultId);
    return keeps;
  }

  internal string Remove(int VaultKeepId, string userId)
  {
    VaultKeep vaultkeep = _repo.GetOne(VaultKeepId);
    if (vaultkeep == null) throw new Exception($"There is no Keep at this id:{VaultKeepId}");

    Vault vault = _vaultsService.GetOne(vaultkeep.VaultId, userId);
    if (userId != vault.CreatorId) throw new Exception("This Vault does not belong to you.");

    bool result = _repo.Remove(VaultKeepId);
    if (result == false) throw new Exception($"There is no keep at this id: {VaultKeepId}");

    return "keep removed from vault";
  }
}
