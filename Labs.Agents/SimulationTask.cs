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
        public int Step { get; private set; }
        Action SimulationStepAction;
        Action RefreshAction;
        CancellationTokenSource CancellationSource;
        TaskCompletionSource<object> CompletionSource;
        EventWaitHandle PauseHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        public SimulationTask(Action simulationStepAction, Action refreshAction)
        {
            SimulationStepAction = simulationStepAction;
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
                        Step++;
                        SimulationStepAction();
                        RefreshAction();
                    }
                }

                CancellationSource = null;
                CompletionSource?.SetResult(null);
            });
        }
    }
}
