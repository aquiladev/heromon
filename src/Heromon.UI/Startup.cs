using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heromon
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		public IConfigurationRoot Configuration { get; set; }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseIISPlatformHandler();
			app.UseStaticFiles();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
