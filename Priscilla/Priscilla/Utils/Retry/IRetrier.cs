namespace Priscilla.Utils.Retry
{
    using System;

    public interface IRetrier
    {
        void DoUntil(Action perform, Func<bool> until, TimeSpan? timeout = null);
        void DontDoUntil(Action perform, Func<bool> whenFulfilled, TimeSpan? timeout = null);

        IDo Do(Action action);                        
    }

    public interface IDo
    {
        IDoFor ForNoLongerThan(TimeSpan fromSeconds);
    }

    public interface IDoFor
    {
        void Until(Func<bool> until);
    }
}