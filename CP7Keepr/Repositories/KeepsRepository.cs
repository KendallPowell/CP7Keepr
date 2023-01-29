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
    (@name, @img, @creatorId, description);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  public bool Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<Keep> Get()
  {
    string sql = @"
    SELECT
    *
    FROM keeps;
    ";
    List<Keep> keeps = _db.Query<Keep>(sql).ToList();
    return keeps;
  }

  public Keep GetOne(int id)
  {
    throw new NotImplementedException();
  }

  public bool Update(Keep update)
  {
    throw new NotImplementedException();
  }
}
