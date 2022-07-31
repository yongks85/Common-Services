using Bootstrap.Test.Mocks;

using DryIoc;
using FluentAssertions;

namespace Bootstrap.Test
{
    public class BootstrapTest
    {
        [Fact]
        public void ResolveWithManualRegistration()
        {
            var container = new Container();
            Setup.Using(container)
                .Register(reg => reg.Register<IMockClass, MockClass>())
                .Start<IMockClass>(mock =>
                {
                    mock.GetType().Should().Be(typeof(MockClass));
                    return Task.CompletedTask;
                });
        }


        [Fact]
        public void ResolveWithAssemblyScanningRegistration()
        {
            var container = new Container();
            Setup.Using(container)
                .ScanAssembly<IMockAssemblyMarker>()
                .Start<IAutoRegType>(mock =>
                {
                    mock.GetType().Should().Be(typeof(AutoA));
                    return Task.CompletedTask;
                });
        }

    }
}