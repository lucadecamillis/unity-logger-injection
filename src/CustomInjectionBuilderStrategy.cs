using System.Reflection;
using Microsoft.Extensions.Logging;
using Unity.Builder;
using Unity.Policy;
using Unity.Strategies;

namespace Unity.Logger.Injection
{
	/// <summary>
	/// Implement the build strategy to inject the logger
	/// </summary>
	internal class CustomInjectionBuilderStrategy : BuilderStrategy
	{
		readonly CustomPropertyProcessor propertyProcessor;
		readonly ILogger logger;

		public CustomInjectionBuilderStrategy(ILogger logger)
		{
			this.propertyProcessor = new CustomPropertyProcessor();
			this.logger = logger;
		}

		public override void PreBuildUp(ref BuilderContext context)
		{
			object policy = context.Registration.Get(typeof(ISelect<PropertyInfo>));
			if (policy is null)
			{
				this.logger.LogDebug($"Adding custom injection selector to type '{context.RegistrationType}'");
				context.Registration.Set(typeof(ISelect<PropertyInfo>), this.propertyProcessor);
			}
		}
	}
}