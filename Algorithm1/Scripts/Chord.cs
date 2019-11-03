using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class Chord
    {
        double leftBound;
        double rightBound; 
        Func<double, double> func;
        int accuracyOrder;

        public int iterationCounter;

        public Chord(double leftBound, double rightBound, Func<double, double> func, Func<double, double> ddfunc, int accuracyOrder)
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
            this.accuracyOrder = accuracyOrder;
        }

        public double Process()
        {
            this.iterationCounter = 0;
            do
            {
                this.Iteration();
            } while (!this.Check());
            return this.leftBound;
        }

        public void Iteration()
        {
            iterationCounter++;
            this.leftBound = this.leftBound -
                (this.func(this.leftBound) * (this.rightBound - this.leftBound))
                / (this.func(this.rightBound) - this.func(this.leftBound));
        }

        private bool Check()
        {
            double res = this.func(this.leftBound);
            return Math.Abs(res) < Math.Pow(10, this.accuracyOrder);
        }


    }
}
