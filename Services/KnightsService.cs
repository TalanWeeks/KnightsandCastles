using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace KnightsandCastles.Services
{
  public class KnightsService
  {
    private readonly KnightsRepository _knightsRepository;

    public KnightsService(KnightsRepository knightsRepository)
    {
      _knightsRepository = knightsRepository;
    }

    public Knight Get()
    {
      return _knightsRepository.Get();
    }

    public Knight Get(int knightId)
    {
      Knight foundKnight = _knightsRepository.Get(knightId);
      if(foundKnight == null)
      {
        throw new Exception("Didn't find a knight mi lord");
      }
      return foundKnight;
    }
    public List<Knight> GetKnightsByCastle(int castleId)
    {
      return _knightsRepository.GetKnightsByCastle(castleId);
    }

    internal ActionResult<Knight> Update(Knight updatedKnight, string  userId)
    {
      Knight foundKnight = Get(updatedKnight.Id);
      if(foundKnight.CastleId != null)
      {
        throw new  Exception("This knight already belongs to a castle mi lord");
      }
      foundKnight.CastleId =  updatedKnight.CastleId;
      return _knightsRepository.Update(foundKnight);
    }
    internal List<Knight> GetKnightsByAccount(string id)
    {
      return _knightsRepository.GetKnightsByAccount(id);
    }
  }
}