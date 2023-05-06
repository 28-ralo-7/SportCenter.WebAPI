using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.User;
using static SportCenter.WebAPI.Converters.UserConverter;

namespace SportCenter.WebAPI.Controllers;

[ApiController]

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet("get")]
    public ActionResult Get()
    {
        try
        {
            List<UserView> users = new List<UserView>();
            foreach (var userDomain in _userService.Get())
            {
                users.Add(ConvertFromDomain(userDomain));
            }
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("getByEmail/{email}")]
    public ActionResult GetByEmail(string email)
    {
        try
        {
            var user = ConvertFromDomain(_userService.GetByEmail(email));
            return Ok(user);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public ActionResult Create([FromBody] UserView user)
    {
        return Ok(_userService.Create(user));
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{email}")]
    public ActionResult<bool> DeleteUser(string email)
    {
        return Ok(_userService.Delete(email));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("update/{email}")]
    public ActionResult UpdateUser(string email, [FromBody] UserView user)
    {
        return Ok(ConvertFromDomain(_userService.Update(email, user)));   
    }
}