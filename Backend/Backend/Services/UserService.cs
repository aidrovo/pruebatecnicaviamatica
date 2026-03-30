using Backend.Data;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public string GenerateEmail(string name, string lastName)
    {
        var baseEmail = (name[0] + lastName).ToLower();
        var email = baseEmail + "@mail.com";
        int i = 1;

        while (_context.Users.Any(x => x.Email == email))
        {
            email = baseEmail + i + "@mail.com";
            i++;
        }
        return email;
    }
}