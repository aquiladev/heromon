using LightInject;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace Heromon.Job
{
	public class Program
	{
		public static IConfigurationRoot Configuration { get; set; }

		public static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();

			Configuration = builder.Build();
			var container = new ServiceContainer();

			JobHostConfiguration config = new JobHostConfiguration
			{
				JobActivator = new JobActivator(container),
				NameResolver = new NameResolver(Configuration)
			};

			var host = new JobHost(config);
			host.RunAndBlock();
		}
	}
}
