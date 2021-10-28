using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Policy;
using Unity.Processors;

namespace Unity.Logger.Injection
{
	internal class CustomPropertyProcessor : PropertyProcessor
	{
		static readonly string[] customProperties = { "Template", "Logger" };

		public CustomPropertyProcessor() : base(new EmptyPolicySet())
		{

		}

		public override IEnumerable<object> Select(Type type, IPolicySet registration)
		{
			IEnumerable<object> properties = base.Select(type, registration);

			IEnumerable<object> templateProperties = type.GetProperties().Where(ShouldBeInjected);
			if (templateProperties.Any())
			{
				properties = properties.Union(templateProperties).Distinct();
			}

			return properties;
		}

		private static bool ShouldBeInjected(PropertyInfo propertyInfo)
		{
			return customProperties.Contains(propertyInfo.Name);
		}
	}
}