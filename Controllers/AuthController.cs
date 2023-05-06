using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Auth;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;
    
    public AuthController(IConfiguration config, IAuthService authService)
    {
        _authService = authService;
        _config = config;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserView user)
    {
        return  Ok(_authService.Register(user));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Auth model)
    {
        var user = _authService.Login(model);
        if (user.username==null)
        {
            return Unauthorized();
        }
        
        string token = CreateToken(user);


        return Ok(token);
    }
    
    private string CreateToken(UserView user)
    {
        List <Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.Role, user.role)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config.GetSection("Secret").Value!));
        
        var credit = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credit
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return $"Bearer {jwt}";
    }
}