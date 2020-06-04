using AI.NeuralNetworks;
using Labs.Agents.Forms;
using Labs.Agents.NeuralNetworks.Properties;
using Labs.Agents.Simulations.AStar;
using Labs.Agents.Simulations.Dijkstra;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(HandleUIException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var workspace = Workspace.Instance;
            var form = new LabForm();
            form.Workspace = workspace;
            var extension1 = new NeuralLabFormExtension(form, workspace);
            var extension2 = new DijkstraLabFormExtension(form, workspace);
            var extension3 = new AStarLabFormExtension(form, workspace);
            Application.Run(form);
        }

        private static void HandleUIException(object sender, ThreadExceptionEventArgs args)
        {
            MessageBox.Show(args.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
