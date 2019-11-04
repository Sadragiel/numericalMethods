using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class SimpleIterations : Method
    {
        double x;
        double coef;
        Func<double, double> func;

        public SimpleIterations(double leftBound, double rightBound, Func<double, double> func, Func<double, double> ddfunc, int accuracyOrder) 
            : base(accuracyOrder)
        {
            //Bounds are legal
            //Coef == 1 / first Derivative of Function 
            this.coef =  (rightBound - leftBound) / (func(rightBound) - func(leftBound));
            this.x = Fourier.Rule(func, ddfunc, leftBound) ? leftBound : rightBound;
            this.func = func;
        }

        protected override void Iteration()
        {
            iterationCounter++;
            this.x = this.x - this.coef * this.func(this.x) ; 
        }

        protected override bool Check()
        {
            return Math.Abs(this.func(this.x)) < Math.Pow(10, this.accuracyOrder);
        }

        protected override double GetRoot()
        {
            return this.x;
        }
    }
}
