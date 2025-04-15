using Application.Interfaces.Services.Security;
using BCrypt.Net;

public class AuthService : IAuthPasswordService
{
    public bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(inputPassword))
            return false;

        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }


    public string HashPassword(string plainPassword)
    { 
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }
       
}

