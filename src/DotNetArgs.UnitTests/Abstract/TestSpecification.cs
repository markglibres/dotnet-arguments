using System;

namespace DotNetArgs.UnitTests.Abstract
{
    public abstract class TestSpecification<TGiven, TResult>
    {
        private TGiven _given;
        private TResult _result;

        protected void When(Func<TGiven, TResult> func)
        {
            _result = func(_given);
        }

        protected void Given(Func<TGiven> func)
        {
            _given = func();
        }

        protected void Then(Action<TGiven, TResult> action)
        {
            action(_given, _result);
        }
    }
}
