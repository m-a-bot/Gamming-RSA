using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class RSACryptographer
    {
        public static BigInteger Encrypt(BigInteger source, BigInteger publicE, BigInteger N)
        {
            return Tools.RapidExponentiation(source, publicE, N);
        }

        public static BigInteger Decrypt(BigInteger c, BigInteger d, BigInteger N) 
        {
            return Tools.RapidExponentiation(c, d, N);
        }
    }
}
