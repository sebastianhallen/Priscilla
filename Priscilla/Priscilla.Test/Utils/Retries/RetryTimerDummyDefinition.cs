namespace Priscilla.Test.Utils.Retries
{
	using System;
	using FakeItEasy;    
    using Priscilla.Utils.Retry;

    public class RetryTimerDummyDefinition
        : DummyDefinition<IRetryTimer>
    {
        protected override IRetryTimer CreateDummy()
        {
			Console.WriteLine("Using single retries");
            return new NRetryTimer(1);
        }
    }

    internal class NRetryTimerFactory
        : IRetryTimerFactory
    {
        private readonly int retries;

        public NRetryTimerFactory(int retries)
        {
            this.retries = retries;
        }

        public IRetryTimer Create(TimeSpan timeoutLimit)
        {
            return new NRetryTimer(this.retries);
        }
    }

    internal class NRetryTimer
        : IRetryTimer
    {
        private int remainingTries;

        public NRetryTimer(int retries)
        {
            this.remainingTries = retries;
        }

        public bool TimedOut()
        {
            return this.remainingTries-- <= 0;
        }
    }
}