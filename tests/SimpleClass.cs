using Microsoft.Extensions.Logging;

namespace Unity.Logger.Injection.Tests
{
	public class LoggerAsProperty
	{
		public ILogger<LoggerAsProperty> Logger { get; set; }
	}
}