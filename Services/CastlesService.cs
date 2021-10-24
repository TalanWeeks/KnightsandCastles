using System;
using System.Collections.Generic;
using KnightsandCastles.Models;
using KnightsandCastles.Repositories;

namespace KnightsandCastles.Services
{
  public class CastlesService
  {
    private readonly CastlesRepository _castlesRepository;

    public CastlesService(CastlesRepository castlesRepository)
    {
      _castlesRepository = castlesRepository;
    }

    public List<Castle> Get()
    {
      return _castlesRepository.Get();
    }

    public Castle Get(int castleId)
    {
      Castle foundCastle = _castlesRepository.Get(castleId);
      if(foundCastle == null)
      {
        throw new Exception("Unable to find Castle");
      }
      return foundCastle;
    }

    public Castle Post(Castle data)
    {
      return _castlesRepository.Post(data);
    }

    public void RemoveCastle(int castleId, string userId)
    {
      Castle foundCastle = Get(castleId);
      if(foundCastle.CreatorId != userId)
      {
        throw new Exception("You dont own this castle");
      }
      _castlesRepository.RemoveCastle(castleId);
    }

    public List<Castle> GetCastleByAccountId(string userId)
    {
      return _castlesRepository.GetCastleByAccount(userId);
    }
  }
}