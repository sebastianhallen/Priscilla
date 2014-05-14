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
            return new SingleTryRetryTimer();
        }

        private class SingleTryRetryTimer
            : IRetryTimer
        {
            private int tries = 0;

            public bool TimedOut 
            {
                get { return tries++ > 0; }
            }
        }
    }
}