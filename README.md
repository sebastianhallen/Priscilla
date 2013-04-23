Priscilla
========

A small mouse interop library.
```C#
var mouse = new Mouse();

// put the cursor at screen coordinate x:100 y:200
mouse.PositionCursor(new Coordinate(100, 200));

//get to the current location of the cursor
var currentPosition = mouse.FindCursor();

//left button down
mouse.LeftDown();

//left button up
mouse.LeftUp();




//Higher level actions are available as extensions in Priscilla.Extension

//move the cursor 
var offset = new Coordinate(20, 20);
mouse.MoveTo(new Coordinate(200, 200), MovementSpeed.Medium, offset);
mouse.MoveTo(new Coordinate(300, 300), MovementSpeed.Instant);
mouse.MoveTo(new Coordinate(100, 100));

//drag and drop from point x:400; y:100 to x:100; y:150
mouse.DragAndDrop(new Coordinate(400, 100), new Coordinate(100, 150);

//click actions
mouse.LeftClick();
mouse.RightClick();
mouse.MiddleClick();

```
