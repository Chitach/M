using M.Configuration;
using M.Data;
using M.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace M {
	public class Startup {
		public Startup(IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			if (env.IsDevelopment()) {
				// This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
				builder.AddApplicationInsightsSettings(developerMode: true);
			}
			Configuration = builder.Build();
			AppConfiguration = new AppConfiguration();
			Configuration.Bind(AppConfiguration);
		}

		public IConfigurationRoot Configuration { get; }
		public AppConfiguration AppConfiguration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Add framework services.
			services.AddApplicationInsightsTelemetry(Configuration);

			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<MDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<MDbContext>();

			services.AddMvc();

			services.AddSingleton(s => AppConfiguration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseIdentity();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
