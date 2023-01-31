namespace CP7Keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep Create(VaultKeep vaultkeepData)
  {
    string sql = @"
    INSERT INTO vaultkeeps
    (keepId, vaultId, creatorId)
    VALUES
    (@KeepId, @VaultId, @CreatorId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultkeepData);
    vaultkeepData.Id = id;
    return vaultkeepData;
  }

  internal List<KeepinsideVaults> GetByVaultId(int id, string userId)
  {
    string sql = @"
    SELECT
    k.*,
    vk.Id AS VaultKeepId
    FROM vaultkeeps vk
    JOIN keeps k ON vk.keepId = k.id
    JOIN vaults v ON vk.vaultId = v.id
    WHERE v.id = @id
    AND (v.isPrivate = false OR v.creatorId = @userId);
    ";
    List<KeepinsideVaults> res = _db.Query<KeepinsideVaults>(sql, new { id, userId }).ToList();
    return res;
  }

  internal bool Remove(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultkeeps
    WHERE id = @vaultkeepsId;
    ";
    int rows = _db.Execute(sql, new { vaultKeepId });
    return rows > 0;
  }
}
