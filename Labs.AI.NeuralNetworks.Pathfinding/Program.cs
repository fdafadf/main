using Demos.AI.NeuralNetwork;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pathfinder
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string configurationJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Pathfinder.json");
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(configurationJsonFilePath, false, false);
            var configuration = configurationBuilder.Build();
            var networks = configuration.GetSection("Networks").Get<TrainingNetworkSettings[]>();
            var data = configuration.GetSection("Data").Get<TrainingDataSettings[]>();
            var trainings = configuration.GetSection("Trainings").Get<TrainingSettings[]>();
            var sets = configuration.GetSection("Sets").Get<int[][]>();
            var trainingSets = sets.Select(set => new TrainingSet(string.Join("", set), trainings[set[2]], data[set[1]], networks[set[0]]));
            var trainingResults = Training.Train(trainingSets);
            ChartForm chartForm = new ChartForm();
            chartForm.Add(trainingResults);
            chartForm.ShowDialog();
            var best = trainingResults.OrderBy(r => r.Result.Errors.Last()).First();

            Form1 form = new Form1();
            form.Network = best.Result.Network;
            form.Graphs = best.Result.TrainingGraphs;

            Application.Run(form);
        }
    }
}
