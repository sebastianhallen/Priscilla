namespace Priscilla.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;

    [TestFixture]
    public class MouseTest
    {
        private Mouse mouse;
        private INativeMethodWrapper nativeMethodWrapper;

        [SetUp]
        public void Before()
        {
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.mouse = new Mouse(this.nativeMethodWrapper);
            A.CallTo(() => this.nativeMethodWrapper.GetSystemMetrics(A<SystemMetric>._)).Returns(65535).NumberOfTimes(2);
        }

        [Test]
        public void PositionCursor_should_move_cursor_with_absolute_movement()
        {
            this.mouse.PositionCursor(new Coordinate(10, 20));

            Func<Input[], bool> hasMatchingFlags = inputs =>
                {
                    var input = inputs.Single().Data.Mouse;
                    return input.Flags == (MouseInputFlags.Move | MouseInputFlags.Absolute);
                };
            A.CallTo(() => this.nativeMethodWrapper.SendInput(A<Input[]>.That.Matches(hasMatchingFlags, "flags to send input did not match"))).MustHaveHappened();
        }

        [Test]
        public void PositionCursor_should_position_cursor_at_correct_coordinates()
        {
            this.mouse.PositionCursor(new Coordinate(10, 20));

            Func<Input[], bool> hasMatchingCoordinates = inputs =>
            {
                var input = inputs.Single().Data.Mouse;
                //intentional +1 after experimenting a bit with how SendInput 
                //handles the transformation to screen normalized coordinates
                return input.X == 11 && input.Y == 21;
            };
            A.CallTo(() => this.nativeMethodWrapper.SendInput(A<Input[]>.That.Matches(hasMatchingCoordinates, "coordinates to send input did not match"))).MustHaveHappened();
        }

        [Test]
        public void MoveCursor_should_move_cursor_with_relative_movement()
        {
            this.mouse.MoveCursor(1, 1);

            Func<Input[], bool> hasMatchingFlags = inputs =>
            {
                var input = inputs.Single().Data.Mouse;
                return input.Flags == (MouseInputFlags.Move);
            };
            A.CallTo(() => this.nativeMethodWrapper.SendInput(A<Input[]>.That.Matches(hasMatchingFlags, "flags to send input did not match"))).MustHaveHappened();
        }

        [Test]
        public void MoveCursor_should_move_cursor_with_correct_distance()
        {
            this.mouse.PositionCursor(new Coordinate(10, 20));

            Func<Input[], bool> hasMatchingCoordinates = inputs =>
            {
                var input = inputs.Single().Data.Mouse;
                //intentional +1 after experimenting a bit with how SendInput 
                //handles the transformation to screen normalized coordinates 
                return input.X == 11 && input.Y == 21;
            };
            A.CallTo(() => this.nativeMethodWrapper.SendInput(A<Input[]>.That.Matches(hasMatchingCoordinates, "coordinates to send input did not match"))).MustHaveHappened();
        }

        [Test]
        public void Should_get_cursor_position_from_native_wrapper()
        {
            CursorCoordinate _;
            A.CallTo(() => this.nativeMethodWrapper.GetCursorPos(out _)).Returns(true);

            this.mouse.FindCursor();
        
            A.CallTo(() => this.nativeMethodWrapper.GetCursorPos(out _)).MustHaveHappened();
        }

        [Test]
        public void Should_press_left_down_with_SendInput()
        {
            this.mouse.LeftDown();

            this.VerifyMouseButtonCall(MouseInputFlags.LeftDown);
        }

        [Test]
        public void Should_press_left_up_with_SendInput()
        {
            this.mouse.LeftUp();

            this.VerifyMouseButtonCall(MouseInputFlags.LeftUp);
        }

        [Test]
        public void LeftDown_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.LeftDown(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.LeftDown,
                1024, 768);
        }

        [Test]
        public void LeftUp_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.LeftUp(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                (MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.LeftUp), 
                1024, 768);
        }

        [Test]
        public void Should_press_right_down_with_SendInput()
        {
            this.mouse.RightDown();

            this.VerifyMouseButtonCall(MouseInputFlags.RightDown);
        }

        [Test]
        public void Should_press_right_up_with_SendInput()
        {
            this.mouse.RightUp();

            this.VerifyMouseButtonCall(MouseInputFlags.RightUp);
        }

        [Test]
        public void RightDown_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.RightDown(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.RightDown,
                1024, 768);
        }

        [Test]
        public void RightUp_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.RightUp(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                (MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.RightUp),
                1024, 768);
        }

        [Test]
        public void Should_press_Middle_down_with_SendInput()
        {
            this.mouse.MiddleDown();

            this.VerifyMouseButtonCall(MouseInputFlags.MiddleDown);
        }

        [Test]
        public void Should_press_Middle_up_with_SendInput()
        {
            this.mouse.MiddleUp();

            this.VerifyMouseButtonCall(MouseInputFlags.MiddleUp);
        }

        [Test]
        public void MiddleDown_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.MiddleDown(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.MiddleDown,
                1024, 768);
        }

        [Test]
        public void MiddleUp_at_specific_position_should_perform_action_at_the_expected_coordinates()
        {
            this.mouse.MiddleUp(new Coordinate(1024, 768));

            this.VerifyMouseButtonCall(
                (MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.MiddleUp),
                1024, 768);
        }

        private void VerifyMouseButtonCall(MouseInputFlags flags, int x = -1, int y = -1)
        {
            Func<Input[], bool> hasMatchingCoordinates = inputs =>
            {
                var input = inputs.Single().Data.Mouse;
                //intentional +1 after experimenting a bit with how SendInput 
                //handles the transformation to screen normalized coordinates 
                return input.X == (x + 1) && input.Y == (y + 1);
            };
            Func<Input[], bool> hasMatchingFlags = inputs =>
            {
                var input = inputs.Single().Data.Mouse;
                return input.Flags == flags;
            };

            Func<Input[], bool> hasMatchingInput = inputs => hasMatchingCoordinates(inputs) && hasMatchingFlags(inputs);

            A.CallTo(() => this.nativeMethodWrapper.SendInput(A<Input[]>.That.Matches(hasMatchingInput,
                                                                           "input to send input did not match")))
             .MustHaveHappened();
        }
    }
}
