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
        double accuracyCoef;
        Func<double, double> func;

        public SimpleIterations(double leftBound, double rightBound, Func<double, double> func, Func<double, double> dfunc, Func<double, double> ddfunc, int accuracyOrder) 
            : base(accuracyOrder)
        {
            //Bounds are legal
            //Coef == 1 / first Derivative of Function 
            double dFunkLeftValue = dfunc(leftBound);
            double dFunkRightValue = dfunc(rightBound);
            double derivativeMin = Math.Min(dFunkLeftValue, dFunkRightValue);
            double derivativeMax = Math.Max(dFunkLeftValue, dFunkRightValue);
            this.coef = 2 / (derivativeMin + derivativeMax);
            this.accuracyCoef = (derivativeMax - derivativeMin) / (derivativeMax + derivativeMin);
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
            return Math.Abs(this.x - this.previosRoot) < Math.Abs((1 - this.accuracyCoef) / this.accuracyCoef) * Math.Pow(10, this.accuracyOrder);
        }

        protected override double GetRoot()
        {
            return this.x;
        }

        protected override void CheckLog(MainWindow mw)
        {
            string str = "q = " 
                + this.accuracyCoef
                + "f(x " + this.iterationCounter 
                + ") < |(1 - q) / q| * 10^(" 
                + this.accuracyOrder 
                + ") =>  " 
                + this.Check() 
                + "\n"
                + Math.Abs(this.x - this.previosRoot) 
                + " < " 
                + Math.Abs((1 - this.accuracyCoef) / this.accuracyCoef) * Math.Pow(10, this.accuracyOrder) 
                + ") =>  " + this.Check() + "\n";
            mw.output.Text += str;

        }
    }
}
