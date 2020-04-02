using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.AI.NeuralNetworks.Car
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var z = new[] { 1.0, 2.0, 3.0, 4.0, 1.0, 2.0, 3.0 };
            var z_exp = z.Select(Math.Exp);
            // [2.72, 7.39, 20.09, 54.6, 2.72, 7.39, 20.09]
            var sum_z_exp = z_exp.Sum();
            // 114.98
            var softmax = z_exp.Select(i => i / sum_z_exp);
            // [0.024, 0.064, 0.175, 0.475, 0.024, 0.064, 0.175]
            var sum = softmax.Sum();


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
