namespace POSInventorySystem.Models;

public class User
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string FullName { get; private set; }
    public string Role { get; private set; }

    public User(
        string username,
        string password,
        string fullName,
        string role)
    {
        Username = username;
        Password = password;
        FullName = fullName;
        Role = role;
    }
}