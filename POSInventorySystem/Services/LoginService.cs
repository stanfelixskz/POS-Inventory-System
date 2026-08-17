using POSInventorySystem.Data;
using POSInventorySystem.Models;

namespace POSInventorySystem.Services;

public class LoginService
{
    public bool UserExists(string username)
    {
        return DataStore.Users.Any(
            user => user.Username == username
        );
    }

    public bool Login(
        string username,
        string password,
        out string role,
        out string fullName)
    {
        role = "";
        fullName = "";

        User? user = DataStore.Users.FirstOrDefault(
            u => u.Username == username
        );

        if (user == null)
        {
            return false;
        }

        if (user.Password != password)
        {
            return false;
        }

        role = user.Role;
        fullName = user.FullName;

        return true;
    }
}