using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Review.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Review.Domain.Models;
using ReviewsWebApplication.Models;
using ConfigurationManager = Review.Domain.Services.ConfigurationManager;

namespace ReviewsWebApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IMapper _mapper;

    public AuthenticationController(ILoginService loginService, IMapper mapper)
    {
        _loginService = loginService;
        _mapper = mapper;
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginViewModel userViewModel)
    {
        if (userViewModel is null)
        {
            return BadRequest("Invalid user request!!!");
        }

        var user = _mapper.Map<Login>(userViewModel);
        var result = _loginService.CheckLogin(user);
        if (result)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new JWTTokenResponse
            {
                Token = tokenString
            });
        }
        return Unauthorized();
    }
}