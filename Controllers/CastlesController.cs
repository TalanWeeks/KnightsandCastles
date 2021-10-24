using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using KnightsandCastles.Models;
using KnightsandCastles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightsandCastles.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CastleController : ControllerBase
  {
    private readonly CastlesService _castlesService;
    private readonly KnightsService _knightsService;

    public CastleController(CastlesService castlesService, KnightsService knightsService)
    {
      _castlesService = castlesService;
      _knightsService = knightsService;
    }

    [HttpGet]
    public ActionResult<List<Castle>> Get()
    {
      try
      {
        return Ok(_castlesService.Get());
      }
      catch (System.Exception error)
      {          
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{castleId}")]
    public ActionResult<Castle> Get(int castleId)
    {
      try
      {
        return Ok(_knightsService.Get(castleId));
      }
      catch (System.Exception e)
      {          
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Castle>> Post([FromBody] Castle data)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        Castle createdCastle = _castlesService.Post(data);
        createdCastle.CreatorId = userInfo.Id;
        return createdCastle;
      }
      catch (System.Exception e)
      {          
        return BadRequest(e.Message);
      }
    }
    
    [Authorize]
    [HttpDelete("{castleId}")]

    public async Task<ActionResult<string>> RemoveCastle(int castleId)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          _castlesService.RemoveCastle(castleId, userInfo.Id);
          return Ok("Castle was delorted mi lord");
      }
      catch (System.Exception e)
      {          
        return BadRequest(e.Message);
      }
    }
  }
}