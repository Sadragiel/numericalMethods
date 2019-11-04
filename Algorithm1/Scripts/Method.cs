using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1.Scripts
{
    abstract class Method
    {
        protected int accuracyOrder;

        protected int iterationCounter;

        public int IterationCounter
        {
            get => this.iterationCounter;
        }

        public Method(int accuracyOrder)
        {
            this.accuracyOrder = accuracyOrder;
        }

        private void Log(MainWindow mw)
        {
            mw.output.Text += String.Format(
                "Ітерація № {{0}}: \n  " +
                "x{{0}} = {{1}} \n  " +
                "Перевіримо критерій зупинки: \n" +
                "f(x{{0}}) < {{2}} => {{3}}\n", this.iterationCounter, this.GetRoot(), this.accuracyOrder, this.Check().ToString());
            if (this.Check())
                mw.output.Text += "Можемо зупинити ітераційний процес \n";
            else
                mw.output.Text += "Продовжуємо ітераційний процес \n";
        }

        public double Process(MainWindow mw)
        {
            this.iterationCounter = 0;
            do
            {
                this.Iteration();
                this.Log(mw);
            } while (!this.Check());
            return this.GetRoot();
        }
        protected abstract void Iteration();
        protected abstract double GetRoot();
        protected abstract bool Check();
    }
}
