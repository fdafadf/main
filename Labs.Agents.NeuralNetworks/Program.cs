using System;
using System.Linq;
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
            var environmentWidth = 200;
            var environmentHeight = 150;
            var numberOfAgents = 80;
            var numberOfObstacles = 180;
            var environment = new MarkovEnvironment2<Agent, AgentState>(new Random(0), environmentWidth, environmentHeight);
            EnvironmentGenerator.GenerateObstacles(environment, numberOfObstacles, 1, 20);
            var simulation = new Simulation(environment, numberOfAgents);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnvironmentForm(simulation));
        }
    }
}
