using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithm1.Scripts;

namespace Algorithm1.Factory
{
    class FactoryMethod
    {
        static List<List<Func<double, double>>> functionsInfo;

        static FactoryMethod()
        {
            functionsInfo = new List<List<Func<double, double>>>();
            functionsInfo.Add(new List<Func<double, double>>());
            functionsInfo.Add(new List<Func<double, double>>());

            functionsInfo[0].Add((double x) 
                => Math.Exp(Math.Pow(x, 5)) 
                + Math.Sin(x) 
                - 10 * x 
                - Math.Pow(x, 9) 
                + Math.Sin(Math.Cos(x)));
            functionsInfo[0].Add((double x) 
                => 5 * Math.Pow(x, 4) * Math.Exp(Math.Pow(x, 5)) 
                + Math.Cos(x) 
                - 10 
                - 9 * Math.Pow(x, 8) 
                - Math.Cos(Math.Cos(x)) * Math.Cos(x) );
            functionsInfo[0].Add((double x)
                => 5 * Math.Pow(x, 3) * Math.Exp(Math.Pow(x, 5)) * (4 - 5 * Math.Pow(x, 5)) 
                - Math.Sin(x) 
                - 72 * Math.Pow(x, 7) 
                - Math.Cos(x) * Math.Cos(Math.Cos(x)) 
                + Math.Sin(Math.Cos(x)) * Math.Pow(Math.Sin(x), 2));

            functionsInfo[1].Add((double x) 
                => Math.Pow(Math.Sin(x), 2) + Math.Pow(x, 4) - Math.Pow(x, 2) - Math.Pow(Math.Cos(x), 2) - 13 * x - 10);
            functionsInfo[1].Add((double x) 
                => 2 * Math.Sin(2 * x) + 4 * Math.Pow(x, 3) - 2 * x - 13);
            functionsInfo[1].Add((double x) 
                => 4 * Math.Cos(2 * x) + 12 * Math.Pow(x, 2) - 2);

        }

        public static void ExecuteMainWindowTask(MainWindow mw)
        {
            

            int taskNumber = mw.r4.IsChecked == true ? 1 : mw.r5.IsChecked == true ? 2 : 3;
            String methodName = mw.r1.IsChecked == true ? (String)mw.r1.Content :
                mw.r2.IsChecked == true ? (String)mw.r2.Content : (String)mw.r3.Content;

            mw.output.Text = "Завдання номер " + taskNumber + "\n";

            Func<double, double> f, df, ddf;
            double lb, rb;
            int accuracyOrder = Int32.Parse(mw.accuracy.Text);

            List<Method> methods = new List<Method>();

            if (taskNumber == 1)
            {
                List<double> coefficient = new List<double>();
                coefficient.Add(double.Parse(mw.a0.Text));
                coefficient.Add(double.Parse(mw.a1.Text));
                coefficient.Add(double.Parse(mw.a2.Text));
                coefficient.Add(double.Parse(mw.a3.Text));
                coefficient.Add(double.Parse(mw.a4.Text));
                coefficient.Add(double.Parse(mw.a5.Text));
                coefficient.Add(double.Parse(mw.a6.Text));
                coefficient.Add(double.Parse(mw.a7.Text));


                //Run Graeffe's method with accurancy 10^(-1)
                Graeffe Graeffe = new Graeffe(coefficient, -1);
                mw.output.Text += "Метод лобачевського: \n";

                List<double> res = Graeffe.Process(mw);
                f = Graeffe.GetFuncValue;
                df = (double x) => Graeffe.getDerivativeValue(x, 1);
                ddf = (double x) => Graeffe.getDerivativeValue(x, 2);

                for(int i = 0; i < res.Count; i++)
                {
                    methods.Add(GetMethod(
                            methodName,
                            res[i] - 0.01, res[i] + 0.01,
                            f, df, ddf, accuracyOrder));
                }
            }
            else
            {
                List<Func<double, double>> functionInfo = functionsInfo[taskNumber - 2];
                double from = taskNumber == 2 ? double.Parse(mw.t2_from.Text) : double.Parse(mw.t3_from.Text);
                double to = taskNumber == 2 ? double.Parse(mw.t2_to.Text) : double.Parse(mw.t3_to.Text);
                methods.Add(GetMethod(methodName, from, to, functionInfo[0], functionInfo[1], functionInfo[2], accuracyOrder));
            }

            for(int i = 0; i < methods.Count; i++)
            {
                mw.output.Text += methodName +  ": \n";
                methods[i].Process(mw);
            }   
        }

        public static Method GetMethod(
            String methodName, 
            double lb, 
            double rb, 
            Func<double, double> f, 
            Func<double, double> df, 
            Func<double, double> ddf, 
            int acc
            )
        {
            switch(methodName)
            {
                case "Метод простих ітерацій":
                    {
                        return new SimpleIterations(lb, rb, f, ddf, acc);
                    }
                case "Метод Хорд":
                    {
                        return new Chord(lb, rb, f, ddf, acc);
                    }
                case "Комбінований метод хорд-дотичних":
                    {
                        return new CombinationMethod(lb, rb, f, df, ddf, acc);
                    }

            }
            return null;
        }
    }
}
