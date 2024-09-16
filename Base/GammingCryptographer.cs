using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base;

public class GammingCryptographer
{
    readonly byte[] _key;

    public GammingCryptographer(string encryptionKey)
    {
        Tools.GetBytes(encryptionKey, out _key);
    }

    public GammingCryptographer(byte[] encryptionKey)
    {
        _key = encryptionKey;
    }

    public byte[] Encrypt(byte[] bSource)
    {

        int bLengthString = bSource.Length,
            bLengthKey = _key.Length;

        byte[] result = new byte[bLengthString];

        for (int bIndex = 0; bIndex < bLengthString; bIndex++)
        {
            result[bIndex] = (byte)(bSource[bIndex] ^ _key[bIndex % bLengthKey]);
        }

        return result;
    }

    public byte[] EncryptBlockCouplingMode(byte[] bSource)
    {

        int bLengthString = bSource.Length,
            bLengthKey = _key.Length;

        byte[] blockKey = new byte[bLengthKey];

        Array.Copy(_key, blockKey, bLengthKey);

        byte[] result = new byte[bLengthString];

        for (int bIndex = 0; bIndex < bLengthString; bIndex++)
        {
            result[bIndex] = (byte)(bSource[bIndex] ^ blockKey[bIndex % bLengthKey]);

            blockKey[bIndex % blockKey.Length] = result[bIndex];
        }

        return result;
    }

    public byte[] Decrypt(byte[] bSource)
    {

        int bLengthString = bSource.Length,
            bLengthKey = _key.Length;

        byte[] result = new byte[bLengthString];

        for (int bIndex = 0; bIndex < bLengthString; bIndex++)
        {
            result[bIndex] = (byte)(bSource[bIndex] ^ _key[bIndex % bLengthKey]);
        }

        return result;
    }

    public byte[] DecryptBlockCouplingMode(byte[] bSource)
    {

        int bLengthString = bSource.Length,
            bLengthKey = _key.Length;

        byte[] blockKey = new byte[bLengthKey];

        Array.Copy(_key, blockKey, bLengthKey);

        byte[] result = new byte[bLengthString];

        for (int bIndex = 0; bIndex < bLengthString; bIndex++)
        {
            result[bIndex] = (byte)(bSource[bIndex] ^ blockKey[bIndex % bLengthKey]);

            blockKey[bIndex % blockKey.Length] ^= result[bIndex];
        }

        return result;
    }
}
