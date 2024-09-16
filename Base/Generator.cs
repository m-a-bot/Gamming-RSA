using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Generator
    {
        private int[] bits;

        public Generator(int N) 
        {
            int[] range = Tools.Range(0, N);

            int[] indexOfZeros = Tools.Sample(range, N / 2);

            bits = new int[N];

            foreach (var index in range)
            {
                bits[index] = 1;
            }

            foreach (var index in indexOfZeros)
            {
                bits[index] = 0;
            }
        }

        public string Bits
        {
            get
            {
                string result = "";

                foreach (var item in bits)
                {
                    result += item;
                }

                return result;
            }
        }
    }
}
