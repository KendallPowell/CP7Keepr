namespace CP7Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal VaultKeep Create(VaultKeep vaultkeepData)
  {
    VaultKeep vaultor = _repo.Create(vaultkeepData);
    return vaultor;
  }

  internal string Remove(int VaultKeepId, string userId)
  {
    VaultKeep vaultkeep = _repo.GetByVaultId(VaultKeepId, userId);
    if (vaultkeep == null) throw new Exception($"There is no Keep at this id:{VaultKeepId}");

    Vault vault = _vaultsService.GetOne(vaultkeep.VaultId, userId);
    if (userId != vault.CreatorId) throw new Exception("This Vault does not belong to you.");

    bool result = _repo.Remove(VaultKeepId);
    if (result == false) throw new Exception($"There is no keep at this id: {VaultKeepId}");

    return "keep removed from vault";
  }
}
