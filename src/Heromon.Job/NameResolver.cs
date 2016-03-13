using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace Heromon.Job
{
	public class NameResolver : INameResolver
	{
		private readonly IConfigurationRoot _configuration;

		public NameResolver(IConfigurationRoot configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(string name)
		{
			return _configuration[$"AppSettings:{name}"];
		}
	}
}
