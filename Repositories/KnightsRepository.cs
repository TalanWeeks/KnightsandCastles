using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using Dapper;
using KnightsandCastles.Models;
using Microsoft.AspNetCore.Mvc;

namespace KnightsandCastles.Repositories
{
  public class KnightsRepository
  {
    private readonly IDbConnection _db;

    public KnightsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Knight> Get()
    {
      string sql = "SELECT * FROM knights";
      return _db.Query<Knight>(sql).ToList();
    }

    internal Knight Get(int knightId)
    {
      string sql = @"
      FROM knights k
      LEFT JOIN castles c ON c.id = k.castleId
      LEFT JOIN account a ON a.id = c.creatorId
      WHERE k.id = @knightId;
      ";
      return  _db.Query<Knight, Castle, Account, Knight>(sql, (k,c,a) => 
      {
        if(c != null)
        {
          c.Creator = a;
          k.Castle = c;
        }
        return k;
      }, new{knightId}).FirstOrDefault();
    }

    internal List<Knight> GetKnightsByAccount(string userId)
    {
      string sql = @"
      SELECT 
      k.*,
      c.*
      FROM castles c
      JOIN knights k ON k.castleId = c.id
      WHERE c.creatorId = @userId;
      ";
      return _db.Query<Knight, Castle, Knight>(sql, (k, c) =>
      {
        k.Castle = c;
        return k;
      }, new {userId} ).ToList();
    }

    internal ActionResult<Knight> Update(Knight foundKnight)
    {
      string sql = @"
      UPDATE knights
      SET
      castleId = @castleId
      WHERE id = @id;
      ";
      var rowsAffected = _db.Execute(sql, foundKnight);
      if(rowsAffected == 0)
      {
        throw new Exception("updating the knight failed mi lord");
      }
      return foundKnight;
    }
  }
}