namespace Priscilla
{
    using System;

    public static class Logger
    {
        private static Action<string> _debugLoggerField;
        private static Action<string> _errorLoggerField;
        private static Action<string> _infoLoggerField;

        public static Action<string> Error
        {
            internal get
            {
                if (_errorLoggerField == null)
                {
                    return _ => { };
                }
                return _errorLoggerField;
            }
            set { _errorLoggerField = value; }
        }

        public static Action<string> Info
        {
            internal get
            {
                if (_infoLoggerField == null)
                {
                    return _ => { };
                }
                return _infoLoggerField;
            }
            set { _infoLoggerField = value; }
        }

        public static Action<string> Debug
        {
            internal get
            {
                if (_debugLoggerField == null)
                {
                    return _ => { };
                }
                return _debugLoggerField;
            }
            set { _debugLoggerField = value; }
        }
    }
}
