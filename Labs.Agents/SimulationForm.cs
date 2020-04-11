using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
