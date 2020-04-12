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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnvironmentForm(Simulation(200, 150, 1, 0)));
        }

        static Simulation Simulation(int environmentWidth, int environmentHeight, int numberOfAgents, int numberOfObstacles)
        {
            var environment = new MarkovEnvironment2<Agent, AgentState>(new Random(0), environmentWidth, environmentHeight);
            EnvironmentGenerator.GenerateObstacles(environment, numberOfObstacles, 1, 20);
            return new Simulation(environment, numberOfAgents);
        }
    }
}
