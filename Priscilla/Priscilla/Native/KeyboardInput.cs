namespace Priscilla.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        public short VirtualKey;
        public short Scan;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}