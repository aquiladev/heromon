using System.IO;
using Microsoft.Azure.WebJobs;

namespace Heromon.Job
{
	public class Functions
	{
		//private readonly TextWriter _logWriter;

		//public Functions(TextWriter writer)
		//{
		//	_logWriter = writer;
		//}

		public static void ProcessTimer([TimerTrigger("*/30 * * * * *", RunOnStartup = true)] TimerInfo info, TextWriter log)
		{
			log.WriteLine("Start processing");
			log.WriteLine($"{info.FormatNextOccurrences(1)}");
			log.Flush();
		}
	}
}
