namespace Priscilla.Utils.Retry
{
    using System;

    public interface IRetrier
    {
        void DoUntil(Action perform, Func<bool> until);
        void DontDoUntil(Action perform, Func<bool> whenFulfilled);

        IDo Do(Action action);                        
    }

    public interface IDo
    {
        IDoFor For(TimeSpan fromSeconds);
    }

    public interface IDoFor
    {
        void Until(Func<bool> until);
    }
}