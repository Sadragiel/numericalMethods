using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Algorithm1.Scripts;

namespace Algorithm1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graeffe Graeffe;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<double> list = new List<double>();
            list.Add(1);
            list.Add(-35);
            list.Add(380);
            list.Add(-1350);
            list.Add(1000);

            this.Graeffe = new Graeffe(list, -1);
            List<double> res = this.Graeffe.Process();
            string resStr = "steps: " + this.Graeffe.iterationCounter + "\n";
            for(int i = 0; i < res.Count; i++)
            {
                resStr += "x" + i.ToString() + " = " + res[i].ToString() + ", \n";
            }
            label.Content = resStr;

            Chord chord = new Chord(
                    res[0] + 0.01, res[0] - 0.01,
                    this.Graeffe.GetFuncValue,
                    (double x) => this.Graeffe.getDerivativeValue(x, 2),
                    -7
                );
            double res2 = chord.Process();
            resStr = "\n Chord steps: " + chord.iterationCounter + "\n";
            resStr += "x1 = " + res2.ToString() + "\n";
            label.Content += resStr;
        }
    }
}
