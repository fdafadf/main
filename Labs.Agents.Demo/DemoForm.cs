using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.Demo
{
    public partial class DemoForm : SimulationForm
    {
        Painter Painter;
        GoalDestructibleCardinalMovementSimulation Simulation;
        Random random = new Random(0);

        public DemoForm()
        {
            InitializeComponent();
            InitializeMenu(toolStripContainer1);
            InitializeEnvironment();
            environmentControl.Paint += ContentPanel_Paint;
            SimulationWorker.Start();
        }

        protected override void LoadEnvironment(EnvironmentBitmap environmentBitmap)
        {
            var random = new Random(0);
            Simulation = new GoalDestructibleCardinalMovementSimulation(environmentBitmap);
            Painter = new Painter(Simulation.InteractiveSpace);
            this.InvokeAction(RefreshEnvironment);
        }

        int Iterations;

        protected override bool IterateSimulation()
        {
            Iterations++;

            foreach (var agent in Simulation.Agents)
            {
                agent.Interaction.Action = random.Next(CardinalMovement.All);
            }

            Simulation.Iterate();
            this.InvokeAction(RefreshEnvironment);
            return Simulation.Agents.Any(agent => agent.Fitness.IsDestroyed == false);
        }

        protected void RefreshEnvironment()
        {
            iterationStatusLabel.Text = $"Iteration: {Iterations} Reached Goals: {Simulation.Goals.Reached}";
            environmentControl.Refresh();
        }

        private void InitializeEnvironment()
        {
            SpaceGeneratorProperties properties = new SpaceGeneratorProperties()
            {
                Seed = 0,
                Width = 200,
                Height = 150,
                NumberOfAgents = 80,
                NumberOfObstacles = 180,
                ObstacleMinSize = 2,
                ObstacleMaxSize = 15
            };
            var obstacles = SpaceGenerator.GenerateObstacles(properties);
            var agents = SpaceGenerator.GenerateAgents(properties, obstacles);
            EnvironmentBitmap = new EnvironmentBitmap(obstacles, agents);
            LoadEnvironment(EnvironmentBitmap);
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            Painter.PaintObstacles(e.Graphics);
            Painter.PaintAgents(e.Graphics, Simulation.Agents);
            Painter.PaintGoals(e.Graphics, Simulation.Agents);
        }
    }
}
