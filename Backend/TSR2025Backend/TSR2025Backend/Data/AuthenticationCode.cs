namespace TSR2025Backend.Data;

public class AuthenticationCode
{
    public int Id { get; set; }
    public string Value { get; set; }
    public int UserId { get; set; }
}