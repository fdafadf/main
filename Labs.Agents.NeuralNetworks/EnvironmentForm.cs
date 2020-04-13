using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2, Labs.Agents.InteractionResult>;

namespace Labs.Agents.NeuralNetworks
{
    public class EnvironmentForm : Form //Action2EnvironmentForm<Simulation, MarkovEnvironment2<Agent, AgentState>, Agent, AgentState, Interaction>
    {
        public EnvironmentForm(Simulation simulation) : base()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            /*
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
                    }
                }

                px = Simulation.Environment.Width * scale + 25;
                py = AgentNetworkInput.V * 10 + 25;
                float dx = (float)historyItem.Input.Encoded[AgentNetworkInput.DirectionOffset] * 25;
                float dy = (float)historyItem.Input.Encoded[AgentNetworkInput.DirectionOffset + 1] * 25;
                e.Graphics.DrawLine(Pens.Black, px, py, px + dx, py + dy);
            }
            */
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EnvironmentForm
            // 
            this.ClientSize = new System.Drawing.Size(607, 328);
            this.Name = "EnvironmentForm";
            this.ResumeLayout(false);

        }
    }
}
