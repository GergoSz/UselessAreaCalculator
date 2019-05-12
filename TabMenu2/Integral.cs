using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2
{

    //Calc all Fx with yield return

    public class Integral
    {

        public static double Sinx( double x ) {
            return Math.Sin(x);
        }

        public static double Sqrtx( double x ) {
            return Math.Sqrt(x);
        }

        public static double Sechx(double x) {
            return (1 / Math.Cosh(x));
        }

        public static IEnumerable<double> GetFx( Func<double , double> func , double from, double to, double step) {
            
            double y = 0;

            for (double i = from; i < to; i += step) {

                y = func(i);

                yield return y;

            }

        }

        public static IEnumerable<Tuple<double, double>> GetFxPairs( double[] FxArray) {

            int prevIndex = 0;

            for (int i = 1; i < FxArray.Length - 1; i++) {

                yield return new Tuple<double, double>(FxArray[prevIndex], FxArray[i]);
                prevIndex++;
            }

        }

        public static double GetArea( double fromFx, double toFx, double step) {

            double fromSquareArea = step * fromFx;
            double toSquareArea = step * toFx;

            if (fromSquareArea > toSquareArea)
            {
                return fromSquareArea - (fromSquareArea - toSquareArea);
            }
            else {
                return toSquareArea - (toSquareArea - fromSquareArea);                
            }
            
        }

        public static double GetStep(double from, double to, long div) {
            return (to - from) / div;
        }

    }

}
