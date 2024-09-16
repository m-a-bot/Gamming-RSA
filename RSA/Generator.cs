using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class Generator
    {
        Random random;
        public Generator()
        {
            random = new Random();
        }

        public Generator(int seed)
        {
            random = new Random(seed);
        }

        public BigInteger GetRand(int n = 512)
        {
            BigInteger baseInt = 2;
            BigInteger result = 0;

            for (int i = 0; i < n-1; i++)
            {
                int r = random.Next(0, 1000_000) & 1;
                if (r == 1)
                    result += baseInt;

                baseInt *= 2;
            }

            return result + 1;
        }
    }
}
