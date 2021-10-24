using System.Threading.Tasks;
using System.Collections.Generic;
using CodeWorks.Auth0Provider;
using KnightsandCastles.Models;
using KnightsandCastles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnightsandCastles.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KnightsController : ControllerBase
  {
    private readonly KnightsService _knightsSerivice;

    public KnightsController(KnightsService knightsService)
    {
      _knightsService = knightsService;
    }

    [HttpGet]
    public ActionResult<List<Knight>> Get()
    {
      try
      {
        return Ok(_knightsService.Get());
      }
      catch (System.Exception e)
      {          
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{knightId}")]

    public ActionResult<Knight> Get(int knightId)
    {
      try
      {
        return Ok(_knightsService.Get(knightId));
      }
      catch (System.Exception e)
      {          
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{knightId")]

    public async Task<ActionResult<Knight>> Update(int knightId, [FromBody] Knight updatedKnight)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedKnight.Id = knightId;
        return _knightsService.Update(updatedKnight, userInfo.Id);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}