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
            mw.output.Text +=
                "\nІтерація № " + this.iterationCounter + ": \n" +
                "x " + this.iterationCounter + " =  " + this.GetRoot() + " \n  " +
                "Перевіримо критерій зупинки: \n";
            this.CheckLog(mw);
            if (this.Check())
                mw.output.Text += "Можемо зупинити ітераційний процес \n";
            else
                mw.output.Text += "Продовжуємо ітераційний процес \n \n";
        }

        public double Process(MainWindow mw)
        {
            this.iterationCounter = 0;
            Func<bool> simpleCheck;
            do
            {

                simpleCheck  = this.SimpleCheck(this.GetRoot());
                this.Iteration();
                this.Log(mw);
            } while (!this.Check() && !simpleCheck());
            return this.GetRoot();
        }
        protected abstract void Iteration();
        protected abstract double GetRoot();
        protected abstract bool Check();

        //This function should stop the iteration process   
        //in the case when the root cannot be changed       
        //because of machine limitation 
        protected Func<bool> SimpleCheck(double previosRoot)
        {
            int finalNumberOfIterations = 500;
            return () => this.iterationCounter > finalNumberOfIterations
                && Math.Abs(previosRoot - this.GetRoot()) < Math.Pow(10, this.accuracyOrder);
        }

        protected abstract void CheckLog(MainWindow mw);
    }
}
