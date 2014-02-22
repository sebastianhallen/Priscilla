namespace Priscilla.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public int X;
        public int Y;
        public uint MouseData;
        public MouseInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}