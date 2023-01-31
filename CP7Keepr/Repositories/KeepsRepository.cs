using CP7Keepr.Interfaces;

namespace CP7Keepr.Repositories;

public class KeepsRepository : IRepository<Keep, int>
{
  private readonly IDbConnection _db;
  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Keep Create(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (name, img, creatorId, description)
    VALUES
    (@name, @img, @creatorId, @description);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  public bool Delete(int id)
  {
    string sql = @"
    DELETE FROM keeps
    where id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows > 0;
  }

  public List<Keep> Get()
  {
    string sql = @"
    SELECT
    k.*,
    ac.*
    FROM keeps k
    JOIN accounts ac ON ac.id = k.creatorId;
    ";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal List<Keep> GetKeeps(int vaultId)
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON k.creatorId = a.id
    WHERE k.vaultId = @vaultId;
    ";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { vaultId }).ToList();
    return keeps;
  }

  public Keep GetOne(int id)
  {
    string sql = @"
    SELECT
    k.*,
    COUNT(vk.id) AS kept,
    ac.*
    FROM keeps k
    JOIN accounts ac ON ac.id = k.creatorId
    LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
    WHERE k.id = @id;
  ";
    return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { id }).FirstOrDefault();
  }

  public bool Update(Keep original)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @name,
    description = @description
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }

  internal List<Keep> GetKeepsInProfile(string id)
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON k.accountId = a.id
    WHERE k.keepId = @keepId;
    ";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }
}