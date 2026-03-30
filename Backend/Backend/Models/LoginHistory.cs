namespace Backend.Models;

public class LoginHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime LoginDate { get; set; }
    public DateTime? LogoutDate { get; set; }
    public bool Success { get; set; }
}