namespace Priscilla
{
    using System;

    public static class Logger
    {
        private static Action<string> _loggerField;

        public static Action<string> Debug
        {
            internal get
            {
                if (_loggerField == null)
                {
                    return _ => { };
                }
                return _loggerField;
            }
            set { _loggerField = value; }
        }
    }
}
