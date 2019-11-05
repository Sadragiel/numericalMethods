using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class Chord : Method
    {
        double leftBound;
        double rightBound; 
        Func<double, double> func;
        public Chord(double leftBound, double rightBound, Func<double, double> func, Func<double, double> ddfunc, int accuracyOrder)
            :base(accuracyOrder)
        {
            this.rightBound = rightBound;
            this.leftBound = leftBound;
            if(Fourier.Rule(func, ddfunc, this.leftBound))
            {
                this.rightBound += this.leftBound;
                this.leftBound = this.rightBound - this.leftBound;
                this.rightBound -= this.leftBound;
            }
            this.func = func;
        }

        protected override void Iteration()
        {
            iterationCounter++;
            this.leftBound = this.leftBound -
                (this.func(this.leftBound) * (this.rightBound - this.leftBound))
                / (this.func(this.rightBound) - this.func(this.leftBound));
        }

        protected override bool Check()
        {
            return Math.Abs(this.func(this.leftBound)) < Math.Pow(10, this.accuracyOrder);
        }

        protected override double GetRoot()
        {
            return this.leftBound;
        }
        protected override void CheckLog(MainWindow mw)
        {
            string str = "f(x " + this.iterationCounter + ") <  10^(" + this.accuracyOrder + ") =>  " + this.Check() + "\n";
            str += Math.Abs(this.func(this.leftBound)) + " < " + Math.Pow(10, this.accuracyOrder) + ") =>  " + this.Check() + "\n";
            mw.output.Text += str;

        }
    }
}
