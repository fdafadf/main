using System;
using System.Threading;
using System.Threading.Tasks;

namespace Labs.Agents
{
    public class BackgroundWorker
    {
        public event Action Paused;
        public event Action Resumed;
        public bool IsPaused { get; private set; }
        public bool IsCompleted => CompletionSource != null;
        public bool SingleStep;
        Func<bool> BackgroundAction;
        CancellationTokenSource CancellationSource;
        TaskCompletionSource<object> CompletionSource;
        EventWaitHandle PauseHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        public BackgroundWorker(Func<bool> backgroundAction)
        {
            IsPaused = true;
            BackgroundAction = backgroundAction;
        }

        public async void Start()
        {
            if (CancellationSource == null)
            {
                CancellationSource = new CancellationTokenSource();

                await Task.Run(() =>
                {
                    DateTime prev = DateTime.Now;
                    Resumed?.Invoke();

                    while (CancellationSource.IsCancellationRequested == false)
                    {
                        if (IsPaused)
                        {
                            Paused?.Invoke();
                            PauseHandle.WaitOne();
                            Resumed?.Invoke();
                        }
                        else
                        {
                            if (BackgroundAction() == false)
                            {
                                Pause();
                            }
                            else if (SingleStep)
                            {
                                SingleStep = false;
                                Pause();
                            }
                        }
                    }

                    CompletionSource.SetResult(null);
                });
            }
        }

        public async Task Stop()
        {
            if (CompletionSource == null)
            {
                CompletionSource = new TaskCompletionSource<object>();
                CancellationSource.Cancel();
                PauseHandle.Set();
                await CompletionSource.Task;
            }
        }

        public void Pause()
        {
            if (IsPaused == false)
            {
                IsPaused = true;
                PauseHandle.Reset();
            }
        }

        public void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
                PauseHandle.Set();
            }
        }
    }
}
