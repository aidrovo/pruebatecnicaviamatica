namespace Backend.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public string Status { get; set; } = "Active";
    public int FailedAttempts { get; set; } = 0;
    public bool IsLoggedIn { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}