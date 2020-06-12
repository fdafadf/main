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

            //NeuralAgentTrainer.TrainAndSimulate(1, new int[] { 18, 9, 1 }, "Empty_Small.png");
            //NeuralAgentTrainer.TrainAndSimulate(4, new int[] { 84, 84, 42, 21, 7, 1 }, "Bottleneck_Small_1.png");
            //NeuralAgentTrainer.TrainAndSimulate(4, new int[] { 84, 84, 42, 21, 7, 1 }, "Example1.png");

            //var obstacles = new bool[,] {
            //    { true, false, false, false, true },
            //    { false, false, false, false, false },
            //    { false, false, true, false, false },
            //    { false, true, false, true, false },
            //    { true, false, false, false, true },
            //};
            //Form form2 = new Form();
            //AnimatedLayersControl grid = new AnimatedLayersControl();
            //grid.Dock = DockStyle.Fill;
            //BitmapLayer layer1 = new BitmapLayer(grid, obstacles);
            //AnimatedLayer layer2 = new AnimatedLayer(grid, obstacles.GetLength(0), obstacles.GetLength(1));
            //layer2.Objects.Add(new AnimatedRectangle(0, 0, 4, 4, 30));
            //layer2.Objects.Add(new AnimatedRectangle(2, 2, 0, 3, 20));
            ////grid.Layers.Add(layer1);
            ////grid.Layers.Add(layer2);
            //form2.Controls.Add(grid);
            //form2.Load += (sender, e) => grid.AnimationTimer.Start();
            //Application.Run(form2);
            //return;

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
