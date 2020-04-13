using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class SimulationTask
    {
        public bool IsStarted => CancellationSource != null;
        public bool IsPaused { get; private set; }
        Action SimulationStep;
        Action RefreshAction;
        CancellationTokenSource CancellationSource;
        TaskCompletionSource<object> CompletionSource;
        EventWaitHandle PauseHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        public SimulationTask(Action simulationStep, Action refreshAction)
        {
            SimulationStep = simulationStep;
            RefreshAction = refreshAction;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            if (IsStarted)
            {
                if (IsPaused)
                {
                    IsPaused = false;
                    PauseHandle.Reset();
                }
            }
            else
            {
                RunSimulation();
            }
        }

        public void PauseOrResume()
        {
            if (IsStarted == false || IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public async Task Stop()
        {
            if (CancellationSource != null)
            {
                CancellationSource.Cancel();
                CompletionSource = new TaskCompletionSource<object>();
                PauseHandle.Reset();
                await CompletionSource.Task;
            }
        }

        private async void RunSimulation()
        {
            CancellationSource = new CancellationTokenSource();

            await Task.Run(() =>
            {
                while (CancellationSource.IsCancellationRequested == false)
                {
                    if (IsPaused)
                    {
                        PauseHandle.Set();
                    }
                    else
                    {
                        SimulationStep();
                        RefreshAction();
                    }
                }

                CancellationSource = null;
                CompletionSource?.SetResult(null);
            });
        }
    }
}
