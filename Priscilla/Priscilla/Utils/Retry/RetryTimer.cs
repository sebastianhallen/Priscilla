namespace Priscilla.Utils.Retry
{
    using System;
    using System.Diagnostics;

    public class RetryTimer
        : IRetryTimer
    {
        private readonly TimeSpan timeoutLimit;
        private readonly Stopwatch stopwatch;

        public RetryTimer(TimeSpan timeoutLimit)
        {
            this.timeoutLimit = timeoutLimit;
            this.stopwatch = Stopwatch.StartNew();
        }

        public bool TimedOut()
        {
            return this.stopwatch.Elapsed > this.timeoutLimit;
        }
    }
}