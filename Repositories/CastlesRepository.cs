using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KnightsandCastles.Models;

namespace KnightsandCastles.Repositories
{
  public class CastlesRepository
  {
    private readonly IDbConnection _db;

    public CastlesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Castle> Get()
    {
      string sql = @"
      SELECT * FORM castles;
      ";
      return _db.Query<Castle>(sql).ToList();
    }

    internal Castle Get(Castle castleId)
    {
      string sql = @"
      SELECT
      c.*,
      a.*
      FROM castles c
      JOIN accounts a on c.creatorId = a.id
      WHERE c.id = @castleId;
      ";
      return _db.Query<Castle, Account, Castle>(sql,  (c, a) => 
      {
        c.Creator = a;
        return c;
      }, new{castleId}).FirstOrDefault();
    }

    internal Castle Post(Castle data)
    {
      string sql = @"
      INSERT INTO castles(castlename, creatorId, creator, population)VALUES(@CastleName, @CreatorId, @Creator, @Population);
      SELECT LAST_LAST_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data; 
    }

    internal List<Castle> GetCastleByAccount(string userId)
    {
      string sql = "SELECT * FROM castles c WHERE c.creatorId = @userId";
      return _db.Query<Castle>(sql, new{userId}).ToList();
    }

    internal void RemoveCastle(int castleId)
    {
      string updateSql = @"
      UPDATE knights
      SET
      castleId = null
      WHERE castleId = @castleId;
      ";
      var updatedRows = _db.Execute(updateSql, new {castleId});
      
      string sql = "DELETE FROM castles WHERE id = @castleId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {castleId});
      if(affectedRows == 0)
      {
        throw new System.Exception("Didn't delete this mi lord");
      }
    }
  }
}