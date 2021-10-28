using System;
using Unity.Policy;

namespace Unity.Logger.Injection
{
	internal class EmptyPolicySet : IPolicySet
	{
		public void Clear(Type policyInterface)
		{

		}

		public object Get(Type policyInterface)
		{
			return null;
		}

		public void Set(Type policyInterface, object policy)
		{
			
		}
	}
}