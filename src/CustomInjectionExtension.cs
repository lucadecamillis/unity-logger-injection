using System;
using Microsoft.Extensions.Logging;
using Unity.Builder;
using Unity.Extension;
using Unity.Policy;
using Unity.Resolution;

namespace Unity.Logger.Injection
{
	/// <summary>
	/// Unity extension which automatically inject ILogger on container instantiated objects
	/// </summary>
	public class CustomInjectionExtension : UnityContainerExtension
	{
		readonly ILogger logger;

		public CustomInjectionExtension(ILogger<CustomInjectionExtension> logger)
		{
			if (logger is null)
			{
				throw new ArgumentNullException(nameof(logger));
			}

			this.logger = logger;
		}

		protected override void Initialize()
		{
			// ILogger property resolution
			base.Context.Strategies.Add(
				new CustomInjectionBuilderStrategy(this.logger),
				UnityBuildStage.PreCreation);

			// Default ILogger resolution
			base.Context.Policies.Set(
				typeof(ILogger),
				typeof(ResolveDelegateFactory),
				(ResolveDelegateFactory)ResolveLogger);
		}

		private ResolveDelegate<BuilderContext> ResolveLogger(ref BuilderContext context)
		{
			return ResolveLoggerDelegate;
		}

		private static object ResolveLoggerDelegate(ref BuilderContext context)
		{
			var factory = context.Container.Resolve<ILoggerFactory>();

			Type loggerType = context.DeclaringType ?? context.Type;

			return factory.CreateLogger(loggerType.Name);
		}
	}
}