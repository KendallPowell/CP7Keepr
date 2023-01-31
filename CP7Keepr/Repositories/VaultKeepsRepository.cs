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

  internal List<VaultKeeps> GetKeeps(int vaultId)
  {
    string sql = @"
    SELECT
    vk.*,
    k.*,
    a.*
    FROM vaultkeeps vk
    JOIN keeps k ON vk.keepId = k.id
    JOIN accounts a ON k.creatorId =a.id
    WHERE vk.vaultId = @vaultId
    AND (v.isPrivate = false OR v.creatorId = @userId);
    ";
    List<VaultKeeps> keeps = _db.Query<VaultKeep, VaultKeeps, Account, VaultKeeps>(sql, (vaultkeeps, keeps, account) =>
    {
      keeps.VaultKeepId = vaultkeeps.Id;
      keeps.Creator = account;
      return keeps;
    }, new { vaultId }).ToList();
    return keeps;

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
