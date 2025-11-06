using System;
using System.Diagnostics;
using System.Numerics;

namespace Fibonacci
{
    internal class Program
    {
        public class Mat2x2Fib
        {
            public Mat2x2Fib(BigInteger x, BigInteger yz, BigInteger w) // y and z are always the same so can use same value
            {
                this.x = x;
                this.yz = yz;
                this.w = w;
            }

            public BigInteger x, yz, w;

            public Mat2x2Fib Squared()
            {
                BigInteger x = this.x * this.x + this.yz * this.yz;
                BigInteger yz = this.x * this.yz + this.yz * this.w;
                BigInteger w = this.yz * this.yz + this.w * this.w;
                return new Mat2x2Fib(x, yz, w);
            }

            public static Vec2 operator *(Mat2x2Fib mat, Vec2 vec)
            {
                return new Vec2(mat.x * vec.x + mat.yz * vec.y, mat.yz * vec.x + mat.w * vec.y);
            }
        }

        public class Vec2
        {
            public Vec2(BigInteger x, BigInteger y)
            {
                this.x = x;
                this.y = y;
            }

            public BigInteger x, y;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Enter an integer to choose the term of the fibonacci sequence to calculate: ");
            
            BigInteger toCountTo = Convert.ToUInt64(Console.ReadLine());
            toCountTo -= 3;
            
            Console.WriteLine("Calculating...");
            var s = new Stopwatch();
            s.Start();
            
            var mat = new Mat2x2Fib(0, 1, 1);
            var matPowers = new List<Mat2x2Fib>();
            matPowers.Add(mat);

            var nums = new Vec2(1, 2);
           
            var pow = 0;
            while (toCountTo != 0)
            {
                var bit = BigInteger.Pow(2, pow);

                if (pow != 0)
                {
                    matPowers.Add(matPowers.ElementAt(pow-1).Squared());
                }

                if ((toCountTo & bit) != 0)
                {
                    nums = matPowers.ElementAt(pow) * nums;
                    toCountTo -= bit;
                }

                pow++;
            }

            s.Stop();
            Console.WriteLine("Done");
            TimeSpan ts = s.Elapsed;
            
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Time to calculate: " + elapsedTime);
            
            var snum = nums.y.ToString();
            Console.WriteLine("This fibonacci term has " + Convert.ToString(snum.Length) + " digits.");
        }
    }
}