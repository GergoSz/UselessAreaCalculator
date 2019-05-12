using System;


namespace TabMenu2
{
    public partial class Binomial 
    {
        

        private static long Factorial(long x)
        {
            if (x <= 1)
                return 1;
            else
                return x * Factorial(x - 1);
        }

        private static long Combination(long a, long b)
        {
            if (a <= 1)
                return 1;

            return Factorial(a) / (Factorial(b) * Factorial(a - b));
        }

        private static double BinomialProbability(int trials, int successes, double probabilityOfSuccess)
        {
            double probOfFailures = 1 - probabilityOfSuccess;

            double c = Combination(trials, successes);
            double px = Math.Pow(probabilityOfSuccess, successes);
            double qnx = Math.Pow(probOfFailures, trials - successes);

            return c * px * qnx;
        }

        
    }
}