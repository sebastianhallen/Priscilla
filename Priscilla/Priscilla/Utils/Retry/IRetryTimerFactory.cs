namespace Priscilla.Utils.Retry
{
    using System;

    public interface IRetryTimerFactory
    {
        IRetryTimer Create(TimeSpan timeoutLimit);
    }
}