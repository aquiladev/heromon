using LightInject;
using Microsoft.Azure.WebJobs.Host;

namespace Heromon.Job
{
	public class JobActivator : IJobActivator
	{
		private readonly IServiceContainer _container;
		public JobActivator(IServiceContainer container)
		{
			_container = container;
		}

		public T CreateInstance<T>()
		{
			return _container.GetInstance<T>();
		}
	}
}
