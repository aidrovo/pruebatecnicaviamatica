using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserService _service;

    public UsersController(AppDbContext context)
    {
        _context = context;
        _service = new UserService(context);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Users.Where(x => !x.IsDeleted));
    }

    [HttpGet("search")]
    public IActionResult Search(string term)
    {
        var users = _context.Users
            .Where(x => !x.IsDeleted &&
                (x.Name.Contains(term) || x.Username.Contains(term)))
            .ToList();

        return Ok(users);
    }

    [HttpPost]
    public IActionResult Create(User u)
    {
        if (_context.Users.Any(x => x.Identification == u.Identification))
            return BadRequest("Identificación ya existe");

        // Username
        if (u.Username.Length < 8 || u.Username.Length > 20 ||
            !u.Username.Any(char.IsUpper) ||
            !u.Username.Any(char.IsDigit) ||
            u.Username.Any(ch => !char.IsLetterOrDigit(ch)))
            return BadRequest("Username inválido");

        // Password
        if (!u.Password.Any(char.IsUpper) ||
            !u.Password.Any(ch => !char.IsLetterOrDigit(ch)) ||
            u.Password.Contains(" "))
            return BadRequest("Password inválido");

        // Identificación
        if (u.Identification.Length != 10 || !u.Identification.All(char.IsDigit))
            return BadRequest("Identificación inválida");

        u.Email = _service.GenerateEmail(u.Name, u.LastName);
        u.Status = "ACTIVE";
        u.FailedAttempts = 0;
        u.IsDeleted = false;

        _context.Users.Add(u);
        _context.SaveChanges();

        return Ok(u);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User updated)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        user.Name = updated.Name;
        user.LastName = updated.LastName;

        _context.SaveChanges();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var u = _context.Users.Find(id);
        if (u == null) return NotFound();

        u.IsDeleted = true;
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("sp-search")]
    public IActionResult SearchSP(string term)
    {
        var users = _context.Users
            .FromSqlRaw("EXEC GetUsersByName @p0", term)
            .ToList();

        return Ok(users);
    }
}