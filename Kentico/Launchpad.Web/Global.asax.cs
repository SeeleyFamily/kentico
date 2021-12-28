using FormBuilderCustomizations;
using Kentico.Web.Mvc;
using Launchpad.Api;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.DependencyInjection;
using Launchpad.Infrastructure.Kentico.Web.ViewEngines;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace Launchpad.Web
{

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			// Register the API project
			GlobalConfiguration.Configure(WebApiConfig.Register);


			// Enable Dependency Injection -- place any assemblies to be scanned for service registration here
			DependencyInjectionEngine dependencyInjectionEngine = new DependencyInjectionEngine();

			var container = dependencyInjectionEngine.Configure(GlobalConfiguration.Configuration,
				typeof(Custom.Infrastructure.Kentico.Web.AssemblyReference).Assembly, // Register Custom Implementing Types First
				typeof(Custom.Infrastructure.AssemblyReference).Assembly, // Register Custom Implementing Types First
				typeof(Custom.Api.AssemblyReference).Assembly,
				typeof(Launchpad.Web.AssemblyReference).Assembly,
				typeof(Launchpad.Api.AssemblyReference).Assembly,
				typeof(Launchpad.Infrastructure.Kentico.Web.AssemblyReference).Assembly,
				typeof(Launchpad.Infrastructure.AssemblyReference).Assembly
			);

			dependencyInjectionEngine.RegisterViewModelFactory();

			OverrideRegistrations(dependencyInjectionEngine);


			// Enable Launchpad View Engine
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new LaunchpadViewEngine());


			// Enables and configures selected Kentico ASP.NET MVC integration features
			ApplicationConfig.RegisterFeatures(ApplicationBuilder.Current);

			// Configure global filters
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			// Registers routes including system routes for enabled features
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// Perform initial caching strategies
			CacheConfig.StartupCache(DependencyResolver.Current);

			//Form Builder config setup
			FormBuilderStaticMarkupConfiguration.SetGlobalRenderingConfigurations();
			FormBuilderFieldMarkupInjection.RegisterEventHandlers();

			// Do not use kentico logger service in application_start
			// it will throw an error
		}


		protected void Application_Error(object sender, EventArgs e)
		{
			ILoggerService logger = DependencyResolver.Current.GetService<ILoggerService>();
			Exception exception = Server.GetLastError();

			if (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}


			// No logger? Throw the exception
			if (logger == null)
			{
				throw exception;
			}


			// Log the error
			System.Diagnostics.Trace.WriteLine(exception);
			System.Diagnostics.Trace.WriteLine(exception.Message);
			logger.LogError($"An exception occurred in the MVC application:{Environment.NewLine}{Environment.NewLine}", "MVC Error", exception: exception);
		}


		protected void Session_Start()
		{
			if (Application["RanOnce"] == null)
			{
				Application["RanOnce"] = true;
				CacheConfig.SessionCache(DependencyResolver.Current);
			}
		}



		/// <summary>
		/// Override default registrations here.
		/// </summary>
		private void OverrideRegistrations(DependencyInjectionEngine engine)
		{
			// engine.OverrideRegistration<ILayoutProvider, LayoutProvider, IPerScopeService>();
			//< !-- ================ CUSTOM =============== -->
			//< !-- ======================================= -->
			//< !-- Add Custom Registration Below This Line -->
			//< !-- ======================================= -->
			//< !-- ======================================= -->
		}
	}

}
