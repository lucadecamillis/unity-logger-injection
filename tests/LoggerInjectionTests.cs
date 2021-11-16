using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Unity.Logger.Injection.Tests
{
	public class LoggerInjectionTests
	{
		[Fact]
		public void NoInjection()
		{
			IUnityContainer container = WireUp();

			LoggerAsProperty candidate = container.Resolve<LoggerAsProperty>();
			Assert.Null(candidate.Logger);
		}

		[Fact]
		public void CanInjectLogger_Ctor()
		{
			IUnityContainer container = WireUp();

			LoggerInCtor candidate = container.Resolve<LoggerInCtor>();
			Assert.True(candidate.LogInjected());
		}

		[Fact]
		public void CanInjectLogger_Property()
		{
			IUnityContainer container = WireUp();

			container.AddNewExtension<CustomInjectionExtension>();

			LoggerAsProperty candidate = container.Resolve<LoggerAsProperty>();
			Assert.NotNull(candidate.Logger);
		}

		private IUnityContainer WireUp()
		{
			ILoggerFactory loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
			loggerFactory.AddProvider(new UnitTestLoggerProvider());

			IUnityContainer container = new UnityContainer();

			container.RegisterInstance(loggerFactory);
			container.RegisterSingleton(
				typeof(Microsoft.Extensions.Logging.ILogger<>),
				typeof(Microsoft.Extensions.Logging.Logger<>));

			return container;
		}

		private class UnitTestLoggerProvider : ILoggerProvider
		{
			public ILogger CreateLogger(string categoryName)
			{
				return NullLogger.Instance;
			}

			public void Dispose()
			{

			}
		}
	}
}