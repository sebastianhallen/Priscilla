namespace Priscilla.Native
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public InputType Type;
        public Inputs Data;
    }
}