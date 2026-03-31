using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest req)
    {
        var user = _context.Users
            .FirstOrDefault(u =>
                (u.Username == req.User || u.Email == req.User)
                && !u.IsDeleted);

        if (user == null) return BadRequest("Usuario no existe");

        if (user.Status == "BLOCKED")
            return BadRequest("Usuario bloqueado");

        if (_context.Sessions.Any(s => s.UserId == user.Id && s.Active))
            return BadRequest("Ya tiene sesión activa");

        if (user.Password != req.Password)
        {
            user.FailedAttempts++;

            if (user.FailedAttempts >= 3)
                user.Status = "BLOCKED";

            _context.SaveChanges();
            return BadRequest("Credenciales incorrectas");
        }

        user.FailedAttempts = 0;

        _context.Sessions.Add(new Session
        {
            UserId = user.Id,
            LoginDate = DateTime.Now,
            Active = true
        });

        _context.SaveChanges();

        var token = _jwt.Generate(user);

        return Ok(new { token });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var userIdClaim = User?.FindFirst("id");

        if (userIdClaim == null)
        {
            return Unauthorized("Token inválido o sin claim 'id'");
        }

        if (!int.TryParse(userIdClaim.Value, out int userId))
        {
            return BadRequest("ID inválido en el token");
        }

        var session = _context.Sessions
            .FirstOrDefault(s => s.UserId == userId && s.Active);

        if (session != null)
        {
            session.Active = false;
            session.LogoutDate = DateTime.Now;
            _context.SaveChanges();
        }

        return Ok(new { message = "Logout correcto" });
    }
}