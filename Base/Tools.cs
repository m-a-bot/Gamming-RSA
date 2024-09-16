using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Tools
    {
        public static Encoding encoding = Encoding.UTF8;

        public static int[] Sample(int[] range, int N)
        {
            int[] result = new int[N];

            int lengthRange = range.Length;

            int[] cRange = new int[lengthRange];

            Array.Copy(range, cRange, lengthRange);

            for (int i = 0, lastIndex = lengthRange; i < N; i++, lastIndex--) 
            {
                int randIndex = new Random().Next(lastIndex);

                result[i] = cRange[randIndex];

                cRange[randIndex] = cRange[lastIndex - 1];

                cRange[lastIndex - 1] = result[i];
            }

            return result;
        }

        public static int[] Range(int start=0, int end=0) 
        {
            int[] r = new int[end - start];

            for (int i = start; i < end; i++)
            {
                r[i] = i;
            }

            return r;
        }

        public static void GetBytes(string source, out byte[] data)
        {
            data = encoding.GetBytes(source);
        }

        public static void GetBytes(string source, out byte[] data, string alphabet)
        {
            List<byte> bytes = new List<byte>();

            for (int i=0; i<source.Length; i++)
            {
                int index = alphabet.IndexOf(source[i]);

                if (index != -1)
                    bytes.Add((byte)index);
            }

            data = bytes.ToArray();
        }

        public static string GetStringFromBytes(byte[] bytes)
        {
            return encoding.GetString(bytes);
        }

        public static string GetStringFromBytes(byte[] bytes, string alphabet)
        {
            string result = "";

            foreach (var item in bytes)
            {
                result += alphabet[(int)item];
            }

            return result;
        }

        public static string GetBitsString(byte[] data) 
        {
            string bits = "";


            foreach (var item in data)
            {
                bits += Convert.ToString(item, 2).PadLeft(8, '0');
            }

            return bits;
        }

        public static byte[] BitsStringToBytes(string bits)
        {
            int lengthBits = 8;

            List<byte> bytes = new List<byte>();

            for (int index = 0; index < bits.Length; index += lengthBits)

                bytes.Add(Convert.ToByte(bits.Substring(index, lengthBits), 2));

            return bytes.ToArray();
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
    }
}
