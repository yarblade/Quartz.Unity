using System;
using System.Threading;

namespace Quartz.Unity.Jobs
{
    public abstract class InterruptableJob : IInterruptableJob
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        protected InterruptableJob()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Execute(context, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void Interrupt()
        {
            _cancellationTokenSource.Cancel();
        }

        public abstract void Execute(IJobExecutionContext context, CancellationToken token);
    }
}
