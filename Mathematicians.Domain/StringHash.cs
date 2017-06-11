using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Mathematicians.Domain
{
    public static class HashExtensions
    {
        public static BigInteger Sha256Hash(this string str)
        {
            if (str == null)
                return BigInteger.Zero;

            using (var digest = SHA256.Create())
            {
                byte[] stringBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = digest.ComputeHash(stringBytes);
                return new BigInteger(hashBytes);
            }
        }

        public static BigInteger Concatenate(this BigInteger start, params BigInteger[] rest)
        {
            using (var digest = SHA256.Create())
            {
                foreach (var b in rest)
                {
                    byte[] blockBytes = b.ToByteArray();
                    digest.TransformBlock(blockBytes, 0, blockBytes.Length, blockBytes, 0);
                }
                byte[] startBytes = start.ToByteArray();
                digest.TransformFinalBlock(startBytes, 0, startBytes.Length);

                byte[] hashBytes = digest.Hash;
                return new BigInteger(hashBytes);
            }
        }

        public static string ToBase64String(this BigInteger hashCode)
        {
            return Convert.ToBase64String(hashCode.ToByteArray());
        }

        public static BigInteger FromBase64String(this string str)
        {
            return new BigInteger(Convert.FromBase64String(str));
        }
    }
}
