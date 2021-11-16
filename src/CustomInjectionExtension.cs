using System;
using Microsoft.Extensions.Logging;
using Unity.Builder;
using Unity.Extension;

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
			base.Context.Strategies.Add(
				new CustomInjectionBuilderStrategy(this.logger),
				UnityBuildStage.PreCreation);
		}
	}
}