using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace Heromon.Controllers
{
	[Route("api/[controller]")]
	public class StatController : Controller
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}
	}
}
