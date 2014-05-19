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

		[Test]
		public void Should_cache_window_offset_when_assuming_fixed_window_position()
		{
			var settings = new WindowRelativeMouse.Settings()
				{
					AssumeFixedWindowPosition = true
				};
			this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.hWnd, this.innerMouse, this.nativeMethodWrapper, this.retrier, settings);

			this.windowRelativeMouse.FindCursor();
			this.windowRelativeMouse.FindCursor();

			var _ = new CursorCoordinate();
			A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(A<IntPtr>._, ref _)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void Should_cache_window_offset_after_find_threshold_has_been_reached_when_assuming_fixed_window_position_and_using_custom_threshold()
		{
			var settings = new WindowRelativeMouse.Settings()
			{
				AssumeFixedWindowPosition = true,
				AssumeFixedWindowPositionThreshold = 2
			};
			this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.hWnd, this.innerMouse, this.nativeMethodWrapper, this.retrier, settings);

			this.windowRelativeMouse.FindCursor();
			this.windowRelativeMouse.FindCursor();
			this.windowRelativeMouse.FindCursor();

			var _ = new CursorCoordinate();
			A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(A<IntPtr>._, ref _)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Times(2));
		}

		[Test]
		public void Should_retry_getting_window_position_until_position_is_not_in_top_left_corner_when_AllowTopLeftPosition_is_false()
		{
			var _ = new CursorCoordinate();
			var settings = new WindowRelativeMouse.Settings()
			{
				AllowTopLeftPosition = false
			};
			this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.hWnd, this.innerMouse, this.nativeMethodWrapper, this.retrier, settings);

			this.windowRelativeMouse.FindCursor();

			A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(A<IntPtr>._, ref _)).MustHaveHappened(Repeated.Exactly.Twice);
		}
	}
}