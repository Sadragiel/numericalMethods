using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class Fourier
    {
        public static bool Rule(Func<double, double> f, Func<double, double> ddf, double point) 
        {
            return f(point) * ddf(point) > 0;
        }
    }
}
