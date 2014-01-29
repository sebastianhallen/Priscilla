namespace Priscilla.Native
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ApplicationWindowFinder
        : IApplicationWindowFinder
    {
        //public static readonly string Wildcard = "362BAD10-E489-4891-93CF-FF23671EE199}";
        public static readonly string Wildcard = "*";

        private readonly INativeMethodWrapper nativeMethodWrapper;

        public ApplicationWindowFinder()
            : this(new NativeMethodWrapper())
        {
        }

        internal ApplicationWindowFinder(INativeMethodWrapper nativeMethodWrapper)
        {
            this.nativeMethodWrapper = nativeMethodWrapper;
        }

        public IntPtr FindWindow(string windowClass, string windowTitle)
        {
            return this.FindChildWindow(IntPtr.Zero, windowClass, windowTitle);
        }

        public IntPtr FindChildWindow(IntPtr hwndParent, string windowClass, string windowTitle)
        {
            var exactMatch = this.nativeMethodWrapper.FindWindowEx(hwndParent, IntPtr.Zero, windowClass, windowTitle);

            if (!IntPtr.Zero.Equals(exactMatch))
            {
                return exactMatch;
            }

            var targethWnd = IntPtr.Zero;
            var enumProc = new Func<IntPtr, bool>(hWnd =>
            {
                const bool isMatchingWindowReturnValue = false;

                var isMatchingWindowResult = this.IsMatchingWindowClass(hWnd, windowClass) && this.IsMatchingCaption(hWnd, windowTitle);
                
                if (isMatchingWindowResult)
                {
                    targethWnd = hWnd;
                }

                return isMatchingWindowResult 
                    ? isMatchingWindowReturnValue
                    : !isMatchingWindowReturnValue;
            });

            this.nativeMethodWrapper.EnumChildWindows(hwndParent, enumProc);

            
            return targethWnd;
        }

        private bool IsMatchingWindowClass(IntPtr hWnd, string windowClass)
        {
            var result = new StringBuilder(1 << 10);
            this.nativeMethodWrapper.GetClassName(hWnd, result, result.Capacity);

            var currentClass = result.ToString();
            Console.WriteLine(currentClass);
            return IsWildcardMatch(windowClass, currentClass);
        }

        private bool IsMatchingCaption(IntPtr hWnd, string windowTitle)
        {
            var result = new StringBuilder(1 << 10);
            this.nativeMethodWrapper.GetWindowText(hWnd, result, result.Capacity);
            
            var currentCaption = result.ToString();

            return IsWildcardMatch(windowTitle, currentCaption);
        }

        private static bool IsWildcardMatch(string pattern, string currentCaption)
        {
            if (pattern == null)
            {
                return false;
            }

            var patternParts = pattern.Split(new[] {Wildcard}, StringSplitOptions.RemoveEmptyEntries);
            var regexPattern = string.Join(".*", patternParts.Select(Regex.Escape));

            return Regex.IsMatch(currentCaption, regexPattern);
        }
    }
}