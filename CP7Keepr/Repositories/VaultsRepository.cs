namespace CP7Keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault Create(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vaults
    (creatorId, name, description, img)
    VALUES
    (@creatorId, @name, @description, @img);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = id;
    return vaultData;
  }

  internal Vault GetOne(int id)
  {
    string sql = @"
    SELECT
    v.*,
    ac.*
    FROM vaults v
    JOIN accounts ac ON ac.id = v.creatorId
    WHERE v.id = @id
    ";
    return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { id }).FirstOrDefault();
  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM vaults
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }

  internal bool Update(Vault original)
  {
    string sql = @"
    UPDATE vaults SET
    name = @name,
    description = @description,
    img = @img
    WHERE id = id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }
}
