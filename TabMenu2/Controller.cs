using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TabMenu2
{

    public class Controller
    {

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        public static decimal GetNext(decimal numberOfIterations) {

            decimal i, j, f;
            decimal pi = 1;
            decimal n = numberOfIterations;

            //double a1 = Math.Sqrt(2);

            for (i = n; i > 1; i--) {
                f = 2;
                for (j = 1; j < i; j++) {
                    f = 2 + Sqrt(f);
                }
                f = Sqrt(f);
                pi = pi * f / 2;
            }

            pi *= Sqrt(2) / 2;
            pi = 2 / pi;

            return pi;
        }

        public static string CalculatePi(int digits)
        {
            digits++;

            uint[] x = new uint[digits * 10 / 3 + 2];
            uint[] r = new uint[digits * 10 / 3 + 2];

            uint[] pi = new uint[digits];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < digits; i++)
            {
                uint carry = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;

                    x[j] += carry;

                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;

                    carry = q * num;
                }


                pi[i] = (x[x.Length - 1] / 10);


                r[x.Length - 1] = x[x.Length - 1] % 10; ;

                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }

            var result = "";

            uint c = 0;

            for (int i = pi.Length - 1; i >= 0; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;

                result = (pi[i] % 10).ToString() + result;
            }

            return result;
        }

        public static int TestPi(string testPi) {

            int correctTil = 0;
            bool isCorrect = true;
            int i = 0;
            //read piToAMill

            string piToAMill = File.ReadAllText("piToAMill.txt"); ;



            while (isCorrect && correctTil != testPi.Length - 1) {
                if (testPi[i] == piToAMill[i])
                {
                    correctTil++;
                }
                else {
                    isCorrect = false;
                }
            }

            return correctTil;
        }

        public static void FindHighestPowerUnder(ConcurrentBag<Tuple<int, int>> bag, int threshold){

            Tuple<int, int> pair;
     
            // while there are items to take, this will prefer local first, then steal if no local
            while (bag.TryTake(out pair))
            {
                // look at next power
                var result = Math.Pow(pair.Item1, pair.Item2 + 1);
    
               if (result<threshold)
               {
                   // if smaller than threshold bump power by 1
                   bag.Add(Tuple.Create(pair.Item1, pair.Item2 + 1));
               }
               else
               {
                   // otherwise, we're done
                   Console.WriteLine("Highest power of {0} under {3} is {0}^{1} = {2}.",
                       pair.Item1, pair.Item2, Math.Pow(pair.Item1, pair.Item2), threshold);
               }
            }
        }
       


    }

}
