using Microsoft.Extensions.Logging;

namespace Unity.Logger.Injection.Tests
{
	public class LoggerInCtor
	{
		private readonly ILogger logger;

		public LoggerInCtor(ILogger<LoggerInCtor> logger)
		{
			this.logger = logger;
		}

		public bool LogInjected()
		{
			return this.logger != null;
		}
	}
}