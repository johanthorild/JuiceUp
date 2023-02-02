using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

internal static class PasswordHelper
{
    private static Encoding PasswordEncoding { get; } = Encoding.UTF8;

    /// <summary>
    /// Used to verify that a user has provided correct password
    /// </summary>
    /// <param name="passwordBase64">the input password as Base64</param>
    /// <param name="encryptedPassword">password related to the user</param>
    /// <param name="salt">the salt related to the user</param>
    /// <returns>True if Match Else false</returns>
    public static bool VerifyPassword(string passwordBase64, string encryptedPassword, string salt)
    {
        var password = FromBase64Password(passwordBase64);
        var regeneratedPassword = GetHashPassword(password, salt);
        return regeneratedPassword == encryptedPassword;
    }

    /// <summary>
    /// Generates a Hashed password
    /// </summary>
    /// <param name="password">password that we want to hash</param>
    /// <param name="salt">the salt to be used. If not provided we call <see cref="GetRandomSalt(int)"/> with input 64</param>
    /// <returns>Returns a hased password</returns>
    public static string GetHashPassword(string password, string? salt = null)
    {
        return salt == null ? HashPassword(password, GetRandomSalt(64)) : HashPassword(password, salt);
    }

    /// <summary>
    /// Generates a Hashed password for a user 
    /// </summary>
    /// <param name="password">Password to be hashed</param>
    /// <returns>Return both the salt and the hashed password (salt,hashedPassword)</returns>
    public static (string salt, string hashedPassword) GenerateHashedPasswordWithSalt(string passwordBase64)
    {
        var password = FromBase64Password(passwordBase64);
        var salt = GetRandomSalt(64);

        var hashedPassword = HashPassword(password, salt);
        return (salt, hashedPassword);
    }

    public static string FromBase64Password(string passwordBase64)
    {
        try
        {
            return PasswordEncoding.GetString(Convert.FromBase64String(passwordBase64));
        }
        catch (FormatException ex)
        {
            throw new FormatException($"Cannot decode password as provided from client. Expecting Base64 input (from {PasswordEncoding.WebName} encoded data).", ex);
        }
    }

    /// <summary>
    /// Hashes a password with provided salt
    /// </summary>
    /// <param name="password">The password to be hashed</param>
    /// <param name="salt">Salt provided to be used with the hash</param>
    /// <returns>Returns ha hashed password</returns>
    private static string HashPassword(string password, string salt)
    {
        var bytes = PasswordEncoding.GetBytes(string.Concat(password, salt));
        var hash = SHA512.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Generates a salt to be used when hashing a password
    /// </summary>
    /// <param name="size">The size</param>
    /// <returns>returns the generated salt</returns>
    private static string GetRandomSalt(int size)
    {
        var salt = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }
}
