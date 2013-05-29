namespace Priscilla.Utils.Retry
{
    using System;

    public class Retrier
        : IRetrier, IDo, IDoFor
    {
        private readonly IRetryTimerFactory retryTimerFactory;
        private TimeSpan timeoutLimit;
        private Func<bool> until;
        private Action action;

        public Retrier(IRetryTimerFactory retryTimerFactory)
        {
            this.retryTimerFactory = retryTimerFactory;
            this.timeoutLimit = TimeSpan.FromSeconds(5);
            this.until = () => true;
            this.action = () => System.Threading.Thread.Sleep(this.timeoutLimit);
        }

        public void DoUntil(Action action, Func<bool> condition)
        {
            var timer = this.retryTimerFactory.Create(this.timeoutLimit);

            while (!condition() && !timer.TimedOut)
            {
                action();
            }
        }

        public void DontDoUntil(Action perform, Func<bool> whenFulfilled)
        {
            var timer = this.retryTimerFactory.Create(this.timeoutLimit);
            bool fulfilled;
            while (!(fulfilled = whenFulfilled()) && !timer.TimedOut)
            {

            }

            if (fulfilled)
            {
                perform();
            }

        }

        public void Until(Func<bool> until)
        {
            this.until = until;
            this.DoUntil(this.action, this.until);
        }

        public IDoFor For(TimeSpan timeoutLimit)
        {
            this.timeoutLimit = timeoutLimit;
            return this;
        }

        public IDo Do(Action action)
        {
            this.action = action;
            return this;
        }
    }
}
