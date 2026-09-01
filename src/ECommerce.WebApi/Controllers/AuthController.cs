using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "El usuario ya existe." });
        }

        var role = string.Equals(request.Role, Roles.Admin, StringComparison.OrdinalIgnoreCase) 
            ? Roles.Admin 
            : Roles.Customer;

        var user = new User
        {
            Email = request.Email,
            PasswordHash = request.Password,
            Role = role
        };

        await _userRepository.AddAsync(user);
        return Ok(new { message = $"Usuario registrado con rol '{user.Role}' exitosamente." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || user.PasswordHash != request.Password)
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

        return Ok(new { token, role = user.Role });
    }
}

// DTOs definidos en el mismo archivo
public record RegisterRequest(string Email, string Password, string Role = Roles.Customer);
public record LoginRequest(string Email, string Password);