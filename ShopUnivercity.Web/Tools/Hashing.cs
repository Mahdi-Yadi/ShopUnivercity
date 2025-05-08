using System.Security.Cryptography;
using System.Text;
namespace ShopUnivercity.Web.Tools;
public static class Hashing
{
    public static string EncodePasswordMd5(string pass)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        encodedBytes = md5.ComputeHash(originalBytes);
        return BitConverter.ToString(encodedBytes);
    }

    public static string HashSalt(string password)
    {
        SHA256 hash = SHA256.Create();
        var passwordBytes = Encoding.Default.GetBytes(password);
        var hashPassword = hash.ComputeHash(passwordBytes);
        return Convert.ToHexString(hashPassword);
    }

    public static string HashPassword(string password)
    {
        SHA512 hash = new SHA512Managed();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashPassword = hash.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashPassword);
    }
}