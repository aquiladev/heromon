using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Heromon.Controllers
{
	[Route("api/[controller]")]
	public class WebsitesController : Controller
	{
		const string SubscriptionId = "";
		const string TenantId = "";
		const string ClientId = "";
		const string ClientSecret = "";
		const string ResourceGroup = "";

		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			string token = GetAccessToken();

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
				client.BaseAddress = new Uri("https://management.azure.com/");

				using (var response = await client.GetAsync($"/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroup}/providers/Microsoft.Web/sites/drpbx?api-version=2015-08-01"))
				{
					response.EnsureSuccessStatusCode();

					var json = await response.Content.ReadAsStringAsync();
				}
			}

			return new[] { "value1", "value2" };
		}

		public static string GetAccessToken()
		{
			var authenticationContext = new AuthenticationContext($"https://login.windows.net/{TenantId}");
			var credential = new ClientCredential(ClientId, ClientSecret);
			var result = authenticationContext.AcquireToken("https://management.azure.com/", credential);

			if (result == null)
			{
				throw new InvalidOperationException("Failed to obtain the JWT token");
			}

			return result.AccessToken;
		}
	}
}
