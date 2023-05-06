using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Session;
using static SportCenter.WebAPI.Converters.SessionConverter;

namespace SportCenter.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }
    
    [HttpGet("getAll")]
    public ActionResult Get()
    {
        try
        {
            List<SessionView> users = new List<SessionView>();
            foreach (var userDomain in _sessionService.Get())
            {
                users.Add(ConvertToView(userDomain));
            }
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("create")]
    public ActionResult Create([FromBody] SessionView sessionView)
    {
        return Ok(_sessionService.Create(sessionView));
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<bool> DeleteUser(int id)
    {
        return Ok(_sessionService.Delete(id));
    }
    [HttpPut("update/{id}")]
    public ActionResult UpdateUser(int id, [FromBody] SessionView sessionView)
    {
        return Ok(ConvertToView(_sessionService.Update(id, sessionView)));   
    }

}