namespace Priscilla.Utils.Retry
{
    using System;

    public class RetryTimerFactory
        : IRetryTimerFactory
    {
        public IRetryTimer Create(TimeSpan timeoutLimit)
        {
            return new RetryTimer(timeoutLimit);
        }
    }
}