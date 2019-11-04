using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    class Graeffe
    {
        private List<double> coefficient;
        private List<double> currentCoefficient;
        private List<double> previousCoefficient;
        private List<double> roots;
        private int accuracyOrder;

        public int iterationCounter;

        public Graeffe(List<double> coefficient, int accuracyOrder)
        {
            this.coefficient = coefficient;
            this.accuracyOrder = accuracyOrder;
            this.roots = new List<double>();
            this.currentCoefficient = new List<double>();
            for (int i = 0; i < this.coefficient.Count; i++)
                this.currentCoefficient.Add(this.coefficient[i]);
        }

        private void Log(MainWindow mw)
        {
            mw.output.Text += String.Format("Ітерація № {{0}}: \n  ", this.iterationCounter);
            String str = "";
            for(int i = 0; i < this.currentCoefficient.Count; i++)
            {
                str += this.currentCoefficient[i] + " * x^(" + (this.currentCoefficient.Count - i) + ") + ";
            }
            mw.output.Text += "Рівняння поточної ітерації: \n  " + str;
            str = "";
            for(int i = 0; i < this.roots.Count; i++)
            {
                str += "x" + i + " = " + this.roots[i] + ";\n";
            }

            mw.output.Text += "Корені поточної ітерації: \n  " + str;

            mw.output.Text += "Критерій зупинки: Корені наступного рівняння наближено дорівнюють квадратам кореням попереднього рівняння (через довжину вектора)\n  " + this.Check();
        }

        //Return array of the aquatoon roots
        public List<double> Process(MainWindow mw)
        {
            this.iterationCounter = 0;
            do
            {
                this.Iteration();
                this.CoumputeRootes();
                this.Log(mw);
            }
            while (!this.Check());
            return this.roots;
        }

        private void CoumputeRootes()
        {
            this.roots.Clear();
            for (int i = 0; i < this.currentCoefficient.Count - 1; i++)
            {
                this.roots.Add(
                    -Math.Pow(this.currentCoefficient[i + 1] / this.currentCoefficient[i],
                    Math.Pow(2, -this.iterationCounter)
                    )
                );
                if (Math.Abs(this.GetFuncValue(this.roots[i])) > Math.Abs(this.GetFuncValue(-this.roots[i])))
                {
                    this.roots[i] *= -1;
                }
            }
        }

        private void Iteration()
        {
            this.previousCoefficient = this.currentCoefficient;
            List<double> nextCoefficient = new List<double>();
            for (int i = 0; i < this.currentCoefficient.Count; i++)
            {
                double a = 0;
                for (int offset = 0; i - offset >= 0 && i + offset < this.currentCoefficient.Count; offset++)
                {
                    a = a + Math.Pow(-1, offset) * (offset == 0 ? 1 : 2)  * this.currentCoefficient[i - offset] * this.currentCoefficient[i + offset];
                }
                nextCoefficient.Add(a);
            }
            this.currentCoefficient = nextCoefficient;
            this.iterationCounter++;
        }

        private bool Check()
        {
            double vectorLength = 0;
            for (int i = 0; i < this.currentCoefficient.Count; i++)
            {
                vectorLength += Math.Pow(1 - this.currentCoefficient[i] / Math.Pow(this.previousCoefficient[i], 2), 2);
            }
            vectorLength = Math.Pow(vectorLength, 0.5);
            return vectorLength < Math.Pow(10, this.accuracyOrder);
        }

        public double GetFuncValue(double x)
        {
            double result = 0;
            for (int i = 0; i < this.coefficient.Count; i++)
            {
                result += this.coefficient[i] * Math.Pow(x, this.coefficient.Count - i - 1);
            }
            return result;
        }

        public double getDerivativeValue(double x, int derivative) 
        {
            double result = 0;
            for (int i = 0; i < this.coefficient.Count - derivative; i++)
            {
                int order = this.coefficient.Count - i - 1;
                double coef = this.coefficient[i];
                for(int n = 0; n < derivative ; n++)
                {
                    coef *= order - n;
                }
                result += coef * Math.Pow(x, order - derivative);
            }
            return result;
        }
    }
}
