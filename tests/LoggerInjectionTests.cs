using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Unity.Logger.Injection.Tests
{
    public class LoggerInjectionTests
    {
        [Fact]
        public void CanInjectLogger()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<ILogger>(NullLogger.Instance);

            container.AddNewExtension<CustomInjectionExtension>();

            SimpleClass simpleClass = container.Resolve<SimpleClass>();
            Assert.NotNull(simpleClass.Logger);
            Assert.Equal(NullLogger.Instance, simpleClass.Logger);
        }
    }
}