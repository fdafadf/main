﻿using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2, Labs.Agents.InteractionResult>;

namespace Labs.Agents.NeuralNetworks
{
    public class EnvironmentForm : Action2EnvironmentForm<Simulation, MarkovEnvironment2<Agent, AgentState>, Agent, AgentState, Interaction>
    {
        public EnvironmentForm(Simulation simulation) : base(simulation)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Simulation.History.Any())
            {
                int scale = 3;
                var environment = Simulation.Environment;
                var agent = Simulation.Environment.Agents.First();
                var historyItem = Simulation.History.Last();
                int i = 0;
                int px = Simulation.Environment.Width * scale;
                int py = 0;

                for (int y = 0; y < AgentNetworkInput.V; y++)
                {
                    for (int x = 0; x < AgentNetworkInput.V; x++)
                    {
                        var field = historyItem.Input.Encoded[AgentNetworkInput.ObstaclesOffset + i++];
                        var pen = field > 0 ? Brushes.Black : Brushes.White;
                        e.Graphics.FillRectangle(pen, px + x * 10, py + y * 10, 9, 9);
                        //var field = environment[px + x, py + y];
                        //Encoded[i++] = field.IsOutside || field.IsObstacle ? 1 : 0;
                    }
                }
            }
        }
    }
}
