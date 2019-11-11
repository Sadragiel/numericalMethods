using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class CombinationMethod : Method
    {
        double leftBound;
        double rightBound;
        Func<double, double> func;
        Func<double, double> dfunc;

        public CombinationMethod(double leftBound, double rightBound, Func<double, double> func, Func<double, double> dfunc, Func<double, double> ddfunc, int accuracyOrder) 
            : base(accuracyOrder)
        {
            this.rightBound = rightBound;
            this.leftBound = leftBound;
            if (Fourier.Rule(func, ddfunc, this.leftBound))
            {
                this.rightBound += this.leftBound;
                this.leftBound = this.rightBound - this.leftBound;
                this.rightBound -= this.leftBound;
            }
            this.func = func;
            this.dfunc = dfunc;
        }

        protected override double GetRoot()
        {
            return Math.Abs(this.func(this.leftBound)) < Math.Pow(10, this.accuracyOrder)
                 ? this.leftBound 
                 : Math.Abs(this.func(this.leftBound)) < Math.Pow(10, this.accuracyOrder) 
                 ? this.rightBound : (this.leftBound + this.rightBound) / 2;
        }

        protected override void Iteration()
        {
            iterationCounter++;
            this.leftBound = this.leftBound -
                (this.func(this.leftBound) * (this.rightBound - this.leftBound))
                / (this.func(this.rightBound) - this.func(this.leftBound));
            this.rightBound = this.rightBound - this.func(this.rightBound) / this.dfunc(this.rightBound);
        }

        protected override bool Check()
        {
            return Math.Abs(this.func(this.leftBound) - this.func(this.previosRoot)) < Math.Pow(10, this.accuracyOrder)
                || Math.Abs(this.func(this.rightBound) - this.func(this.previosRoot)) < Math.Pow(10, this.accuracyOrder);
        }

        protected override void CheckLog(MainWindow mw)
        {
            string str = "Для лівої межі: \n";
            str +="f(x " + this.iterationCounter + ") <  10^(" + this.accuracyOrder + ") =>  " + this.Check() + "\n";
            str += Math.Abs(this.func(this.leftBound)) + " < " + Math.Pow(10, this.accuracyOrder) + ") =>  " + this.Check() + "\n";
            str += "Для правої межі: \n";
            str += "f(x " + this.iterationCounter + ") <  10^(" + this.accuracyOrder + ") =>  " + this.Check() + "\n";
            str += Math.Abs(this.func(this.rightBound)) + " < " + Math.Pow(10, this.accuracyOrder) + ") =>  " + this.Check() + "\n";
            mw.output.Text += str;
        }
    }
}
