using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents
{
    /// <summary>
    /// Jeśli chcemy policzyć ścieżki przed rozpoczęciem symulacji, to parz komentarze [Wariant 1].
    /// Jeśli chcemy liczyć ścieżki dynamicznie po każdym kroku symulacji, to parz komentarze [Wariant 2].
    /// </summary>
    public partial class SimulationForm : Form 
    {
        CancellationTokenSource SimulationCancellation;
        bool SimulationPaused;
        EventWaitHandle PauseHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        public SimulationForm()
        {
            InitializeComponent();
        }

        private async void DrawSimulation()
        {
            await Task.Run(() =>
            {
                while (SimulationCancellation.IsCancellationRequested == false)
                {
                    if (SimulationPaused)
                    {
                        PauseHandle.Set();
                    }
                    else
                    {
                        SimulationStep();
                        this.InvokeAction(Refresh);
                    }
                }

                SimulationCancellation = null;
                this.InvokeAction(Close);
            });
        }

        protected virtual void SimulationStep()
        {

        }

        protected void DrawObstacles<TEnvironment, TAgent, TState>(Graphics graphics, TEnvironment environment, int scale)
            where TEnvironment : IEnvironment<TEnvironment, TAgent, TState>
            where TAgent : IAgent<TEnvironment, TAgent, TState>
            where TState : AgentState<TEnvironment, TAgent, TState>
        {
            int width = environment.Width;
            int height = environment.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    var field = environment[x, y];

                    if (field.IsObstacle)
                    {
                        graphics.FillRectangle(Brushes.DarkGray, x * scale, y * scale, scale, scale);
                    }
                }
            }
        }

        protected void DrawAgents<TEnvironment, TAgent, TState>(Graphics graphics, TEnvironment environment, int scale)
            where TEnvironment : IEnvironment<TEnvironment, TAgent, TState>
            where TAgent : IAgent<TEnvironment, TAgent, TState>
            where TState : AgentState2<TEnvironment, TAgent, TState>
        {
            foreach (var agent in environment.Agents)
            {
                var agentState = agent.State;
                var x = agentState.Field.X;
                var y = agentState.Field.Y;
                Brush brush = agentState.IsDestroyed ? Brushes.Red : Brushes.Black;
                graphics.FillRectangle(brush, x * scale, y * scale, scale, scale);
            }
        }

        private void StartStopSimulation()
        {
            if (SimulationCancellation == null)
            {
                SimulationCancellation = new CancellationTokenSource();
                DrawSimulation();
            }
            else
            {
                if (SimulationPaused)
                {
                    SimulationPaused = false;
                    PauseHandle.Reset();
                }
                else
                {
                    SimulationPaused = true;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SimulationCancellation != null)
            {
                e.Cancel = true;
                SimulationCancellation.Cancel();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    StartStopSimulation();
                    break;
            }
        }
    }
}
