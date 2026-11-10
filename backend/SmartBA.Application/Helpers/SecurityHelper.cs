using System.Security.Cryptography;
using System.Text;

namespace SmartBA.Application.Helpers;

public static class SecurityHelper
{
    public static string HashOtp(string otp)
    {
        return Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(otp)));
    }

    public static string HashToken(string token)
    {
        using var sha = SHA256.Create();

        var bytes =
            sha.ComputeHash(
                Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(bytes);
    }

    public static string HashRefreshToken(string token)
    {
            return Convert.ToHexString(
                SHA256.HashData(
                Encoding.UTF8.GetBytes(token)));
    }

    public static string GenerateRefreshToken()
    {
            var bytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes);     
    }

    public static string GenerateSessionId()
    {
        return "ok";
    }

}