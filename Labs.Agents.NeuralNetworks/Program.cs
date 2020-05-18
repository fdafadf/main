using Labs.Agents.ComponentModel;
using Labs.Agents.Dijkstra;
using Labs.Agents.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
            var extension1 = new NeuralAgentExtension(form, workspace);
            var extension2 = new DijkstraAgentExtension(form, workspace);
            Application.Run(form);
        }

        private static void HandleUIException(object sender, ThreadExceptionEventArgs args)
        {
            MessageBox.Show(args.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
