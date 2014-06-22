namespace Priscilla.Test.Utils.Retries
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;    
    using Priscilla.Utils.Retry;

    [TestFixture]
    public class RetryUntilTests
    {
#pragma warning disable 649
        [UnderTest] private Retrier retrierSut;
        [Fake] private IRetryTimer retryTimer;
        [Fake] private IRetryTimerFactory retryTimerFactory;
#pragma warning restore 649
        private IRetrier retrier;

        [SetUp]
        public void Before()
        {
            Fake.InitializeFixture(this);
            A.CallTo(() => this.retryTimerFactory.Create(A<TimeSpan>._)).Returns(this.retryTimer);

            this.retrier = retrierSut;
        }

        [Test]
        public void Should_not_perform_action_when_retry_condition_is_satisfied()
        {
            var actionPerformed = false;

            this.retrier.DoUntil(() => { actionPerformed = true; }, () => true, TimeSpan.FromSeconds(1));

            Assert.That(actionPerformed, Is.False);
        }

        [Test]
        public void Should_perform_action_when_retry_condition_is_not_satisfied()
        {
            var actionPerformed = false;
            A.CallTo(() => this.retryTimer.TimedOut()).ReturnsNextFromSequence(false, true);

            this.retrier.DoUntil(() => { actionPerformed = true; }, () => false, TimeSpan.FromSeconds(1));

            Assert.That(actionPerformed);
        }

        [Test]
        public void DontDoUntil_should_not_perform_action_when_condition_is_not_satisfied()
        {
            var actionPerformed = false;
            A.CallTo(() => this.retryTimer.TimedOut()).ReturnsNextFromSequence(false, true);

            this.retrier.DontDoUntil(() => { actionPerformed = true; }, () => false, TimeSpan.FromSeconds(1));

            Assert.That(actionPerformed, Is.False);
        }

        [Test]
        public void DontDoUntil_should_perform_action_when_condition_is_satisfied()
        {
            var actionPerformed = false;

            this.retrier.DontDoUntil(() => { actionPerformed = true; }, () => true, TimeSpan.FromSeconds(1));

            Assert.That(actionPerformed, Is.True);
        }
    }
}