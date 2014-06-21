namespace Priscilla.Utils.Retry
{
    public interface IRetryTimer
    {
        bool TimedOut();
    }
}