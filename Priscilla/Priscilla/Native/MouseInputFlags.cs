namespace Priscilla.Native
{
    using System;

    [Flags]
    public enum MouseInputFlags
        : uint
    {
        Absolute = 0x8000,        //The dx and dy parameters contain normalized absolute coordinates. If not set, those parameters contain relative data
        VirtualDesktop = 0x4000,  //Maps coordinates to the entire desktop. Must be used with MouseInput.Absolute. 
        LeftDown = 0x0002,        //The left button is down.
        LeftUp = 0x0004,          //The left button is up.
        MiddleDown = 0x0020,      //The middle button is down.
        MiddleUp = 0x0040,        //The middle button is up.
        Move = 0x0001,            //Movement occurred.
        RightDown = 0x0008,       //The right button is down.
        RightUp = 0x0010,         //The right button is up.
        Wheel = 0x0800,           //The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
        XDown = 0x0080,           //An X button was pressed.
        XUp = 0x0100,             //An X button was released.
        HWheel = 0x1000,          //The wheel button is tilted.
    }
}