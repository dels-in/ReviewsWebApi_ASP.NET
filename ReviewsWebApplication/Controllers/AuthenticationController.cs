﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Review.Domain.Models;
using Review.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConfigurationManager = Review.Domain.Services.ConfigurationManager;

namespace ReviewsWebApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<ReviewController> _logger;
    private readonly ILoginService _loginService;

    public AuthenticationController(ILogger<ReviewController> logger, ILoginService loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] Login user)
    {
        if (user is null)
        {
            return BadRequest("Invalid user request!!!");
        }

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