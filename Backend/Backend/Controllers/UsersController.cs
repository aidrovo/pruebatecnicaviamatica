using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Users.Where(x => !x.IsDeleted));
    }

    [HttpPost]
    public IActionResult Create(User u)
    {
        _context.Users.Add(u);
        _context.SaveChanges();
        return Ok(u);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var u = _context.Users.Find(id);
        u.IsDeleted = true;
        _context.SaveChanges();
        return Ok();
    }
}