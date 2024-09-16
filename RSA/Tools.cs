using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class Tools
    {
        private static Generator generator = new Generator();

        public static BigInteger RapidExponentiation(BigInteger a, BigInteger n, BigInteger mod)
        {
            BigInteger result = 1;
            
            BigInteger z = a;

            string bin = GetBinary(n);

            foreach (var item in bin) 
            {
                if (item == '1')
                    result = Mod(result * z, mod);

                z = Mod(z*z, mod);
            }

            return result;
        }

        public static string GetBinary(BigInteger n) 
        {
            string bin = "";
            BigInteger integer = n;
            
            while (integer > 0)
            {
                bin = Mod(integer, 2) == 0 ? bin+ "0" :  bin+ "1";
                
                integer /= 2;
            }

            return bin;
        }

        public static BigInteger GetIntegerFromBinary(string binaryString)
        {
            BigInteger baseInt = 1;
            BigInteger result = 0;

            for (int i=binaryString.Length-1; i >= 0; i--)
            {
                if (binaryString[i] == '1')
                    result += baseInt;

                baseInt *= 2;
            }

            return result;
        }

        public static BigInteger Rand(int n)
        {
            return generator.GetRand(n);
        }

        // n > 3, нечетное
        // true - число простое
        // false - число составное
        public static bool TestMillerRabin(BigInteger n, int length=512, int t = 512)
        {
            if (n == 2 || n == 3)
                return true;

            if (Mod(n, 2) == 0)
                return false;

            var decompose = (BigInteger n, BigInteger a) =>
            {
                BigInteger source = n;

                BigInteger degree = 0;

                BigInteger[] result = new BigInteger[2];
                
                while (Mod(source, a) == 0)
                {
                    source /= a;
                    degree++;
                }

                result[0] = degree;
                result[1] = source;

                return result;
            };

            BigInteger s, r;

            var decomposeByDegreeOfTwo = decompose(n - 1, 2);
            s = decomposeByDegreeOfTwo[0];
            r = decomposeByDegreeOfTwo[1];

            for (int i=0; i < t; i++)
            {
                BigInteger b = Rand(length);

                while (b < 2 || b > n - 2)
                    b = Rand(length);

                BigInteger y = RapidExponentiation(b, r, n);

                if (y != 1 && y != n - 1)
                {
                    BigInteger j = 1;

                    while (j < s && y != n - 1)
                    {
                        y = RapidExponentiation(y, 2, n);

                        if (y == 1)
                            return false;
                        j++;
                    }

                    if (y != n - 1)
                        return false;
                }
            }

            return true;
        }

        public static bool AllCharactersOfStringBelongToAlphabet(string line, string alphabet)
        {

            foreach (var character in line)
            {
                if (alphabet.IndexOf(character) == -1)
                    return false;
            }

            return true;
        }

        public static bool AnyCharacterOfStringBelongToAlphabet(string line, string alphabet)
        {
            foreach (var character in line)
            {
                if (alphabet.IndexOf(character) != -1)
                    return true;

            }
            return false;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b) 
        {
            BigInteger integer1 = a,
                integer2 = b;

            
            while (integer1 != integer2)
            {
                if (integer1 > integer2)
                {
                    integer1 -= integer2;
                }
                else
                {
                    integer2 -= integer1;
                }

            }

            return integer1;
        }

        public static BigInteger EnhancedGCD(BigInteger a, BigInteger b, out BigInteger x)
        {
            BigInteger s = 0, r = b,
                old_s = 1, old_r = a;

            BigInteger temp1, temp2, q;

           
            while (r != 0)
            {
                q = old_r / r;

                temp1 = r;
                temp2 = old_r;

                old_r = temp1;
                r = temp2 - q * temp1;

                temp1 = s;
                temp2 = old_s;

                old_s = temp1;
                s = temp2 - q * temp1;
            }

            x = old_s;

            return old_r;

        }

        public static BigInteger Mod(BigInteger a, BigInteger b) 
        {
            BigInteger result = a % b;

            return result < 0 ? result + b : result;
        }

        public static string BigIntFromString(string s, string alphabet, int length=2) 
        {
            string result = "1";

            foreach (char c in s)
            {
                int index = alphabet.IndexOf(c);

                if (index == -1)
                    continue;

                result += index.ToString().PadLeft(length, '0');
            }

            return result;
        }

        
    }
}
