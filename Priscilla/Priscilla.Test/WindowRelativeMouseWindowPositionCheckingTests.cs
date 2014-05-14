namespace Priscilla.Test
{
	using System;
	using FakeItEasy;
	using NUnit.Framework;

	[TestFixture]
	internal class WindowRelativeMouseWindowPositionCheckingTests
		: WindowRelativeMouseTests
	{
		[SetUp]
		public void BeforeEach()
		{
			A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).Returns(this.hWnd);
		}

		[TearDown]
		public void AfterEach()
		{
			WindowRelativeMouse.AssumeFixedWindowPosition = false;
			WindowRelativeMouse.AllowTopLeftPosition = true;
		}


		[Test]
		public void Should_cache_window_offset_when_assuming_fixed_window_position()
		{
			WindowRelativeMouse.AssumeFixedWindowPosition = true;

			this.windowRelativeMouse.FindCursor();
			this.windowRelativeMouse.FindCursor();

			var _ = new CursorCoordinate();
			A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(A<IntPtr>._, ref _)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void Should_retry_getting_window_position_until_position_is_not_in_top_left_corner_when_AllowTopLeftPosition_is_false()
		{
			var _ = new CursorCoordinate();

			WindowRelativeMouse.AllowTopLeftPosition = false;
			this.windowRelativeMouse.FindCursor();

			A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(A<IntPtr>._, ref _)).MustHaveHappened(Repeated.Exactly.Twice);
		}
	}
}