using System.Text;
using System.Security.Cryptography;

using Core.Models;

namespace Core.Helpers;

/// <summary>
/// <see cref="IEncryptDecrypt"/> implementation
/// </summary>
public sealed class EncryptDecrypt : IEncryptDecrypt
{
    private readonly EncryptSettings _encryptSettings;

    #region Ctor

    public EncryptDecrypt(EncryptSettings encriptSettings)
    {
        _encryptSettings = encriptSettings;
    }

    #endregion

    /// <inheritdoc/>
    public string Encrypt(string encryptString)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptSettings.PrivateKey, new byte[] {
        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
    });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    /// <inheritdoc/>
    public string Decrypt(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptSettings.PrivateKey, new byte[] {
        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
    });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}

/// <summary>
/// <see cref="IEncryptDecrypt"/> contracts
/// </summary>
public interface IEncryptDecrypt
{
    /// <summary>
    /// Encrypt some string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    string Encrypt(string input);

    /// <summary>
    /// Decrypt some value
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    string Decrypt(string input);
}
