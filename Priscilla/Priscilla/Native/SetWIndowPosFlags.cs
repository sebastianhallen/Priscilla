namespace Priscilla.Native
{
    public static class SetWIndowPosFlags
    {
        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx
        public const uint SWP_ASYNCWINDOWPOS = 0x4000; //If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        public const uint SWP_DEFERERASE = 0x2000; //Prevents generation of the WM_SYNCPAINT message.
        public const uint SWP_DRAWFRAME = 0x0020; //Draws a frame (defined in the window's class description) around the window.
        public const uint SWP_FRAMECHANGED = 0x0020; //Applies new frame styles set using the SetWindowLong function
        public const uint SWP_HIDEWINDOW = 0x0080; //Hides the window.
        public const uint SWP_NOACTIVATE = 0x0010; //Does not activate the window.
        public const uint SWP_NOCOPYBITS = 0x0100; //Discards the entire contents of the client area.
        public const uint SWP_NOMOVE = 0x0002; //Retains the current position (ignores X and Y parameters).
        public const uint SWP_NOOWNERZORDER = 0x0200; //Does not change the owner window's position in the Z order.
        public const uint SWP_NOREDRAW = 0x0008; //Does not redraw changes. If this flag is set, no repainting of any kind occurs. ....
        public const uint SWP_NOREPOSITION = 0x0200; //Same as the SWP_NOOWNERZORDER flag.
        public const uint SWP_NOSENDCHANGING = 0x0400; //Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        public const uint SWP_NOSIZE = 0x0001; //Retains the current size (ignores the cx and cy parameters).
        public const uint SWP_NOZORDER = 0x0004; //Retains the current Z order (ignores the hWndInsertAfter parameter).
        public const uint SWP_SHOWWINDOW = 0x0040; //Displays the window.



    }
}