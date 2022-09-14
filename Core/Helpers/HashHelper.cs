using System.Security.Cryptography;

namespace Core.Helpers;

/// <summary>
/// Hashing extension methods
/// </summary>
public static class HashHelper
{
    private const int SaltByteSize = 24;
    private const int HashByteSize = 24;
    private const int HasingIterationsCount = 10101;

    /// <summary>
    /// Hash a string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Hash(this string value)
    {
        byte[] salt;
        byte[] buffer2;
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(value, SaltByteSize, HasingIterationsCount))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(HashByteSize);
        }
        byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
        Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
        Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
        return Convert.ToBase64String(dst);
    }

    /// <summary>
    /// Compare a string with some hash
    /// </summary>
    /// <param name="value"></param>
    /// <param name="compareHash"></param>
    /// <returns></returns>
    public static bool HashMatch(this string value, string compareHash)
    {
        byte[] _valueHashBytes;

        int _arrayLen = (SaltByteSize + HashByteSize) + 1;
        byte[] src = Convert.FromBase64String(compareHash);

        if ((src.Length != _arrayLen) || (src[0] != 0))
        {
            return false;
        }

        byte[] _currentSaltBytes = new byte[SaltByteSize];
        Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

        byte[] _currentHashBytes = new byte[HashByteSize];
        Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(value, _currentSaltBytes, HasingIterationsCount))
        {
            _valueHashBytes = bytes.GetBytes(SaltByteSize);
        }

        return AreHashesEqual(_currentHashBytes, _valueHashBytes);
    }

    /// <summary>
    /// Compare to hash
    /// </summary>
    /// <param name="firstHash"></param>
    /// <param name="secondHash"></param>
    /// <returns></returns>
    private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
    {
        int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
        var xor = firstHash.Length ^ secondHash.Length;
        for (int i = 0; i < _minHashLength; i++)
            xor |= firstHash[i] ^ secondHash[i];
        return 0 == xor;
    }
}
